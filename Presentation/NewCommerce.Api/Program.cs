
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using NewCommerce.Api.Configurations.ColumnWriters;
using NewCommerce.Application;
using NewCommerce.Application.Abstractions.Storage;
using NewCommerce.Application.Abstractions.Storage.Local;
using NewCommerce.Application.Validators.Products;
using NewCommerce.Infrastructure;
using NewCommerce.Infrastructure.Filters;
using NewCommerce.Infrastructure.Services;
using NewCommerce.Infrastructure.Services.Storage.Local;
using NewCommerce.Persistence;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Sinks.PostgreSQL;
using System.Diagnostics;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();


//builder.Services.AddStorage<AzureStorage>();

builder.Services.AddStorage<LocalStorage>();


builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
policy.AllowAnyOrigin()
.AllowAnyHeader().
AllowAnyMethod()

));


builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>()).
    AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>()).
    ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin", options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
        };
    });

Logger log = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt")
    .WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("PsotgreSql"), "logs", needAutoCreateTable: true,
    columnOptions: new Dictionary<string, ColumnWriterBase>
    {
        {"message",new RenderedMessageColumnWriter() },
        {"message_template", new MessageTemplateColumnWriter() },
        {"level", new LevelColumnWriter() },
        {"time_stamp" , new TimestampColumnWriter() },
        {"exception", new ExceptionColumnWriter() },
        {"properties", new PropertiesColumnWriter() },
        {"log_event", new LogEventSerializedColumnWriter() },
        {"user_name", new UsernameColumnWriter() }
    })

    .Enrich.FromLogContext().
    MinimumLevel.Information()
    .CreateLogger();

builder.Host.UseSerilog(log);

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseStaticFiles();
app.UseSerilogRequestLogging();
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
    {
        var username = context.User?.Identity?.IsAuthenticated != null || true ? context.User.Identity.Name : null;
       LogContext.PushProperty("username", username);
        await next();
    });

app.MapControllers();

app.Run();
