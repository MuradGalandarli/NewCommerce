
using FluentValidation.AspNetCore;
using NewCommerce.Application.Abstractions.Storage;
using NewCommerce.Application.Abstractions.Storage.Local;
using NewCommerce.Application.Validators.Products;
using NewCommerce.Infrastructure;
using NewCommerce.Infrastructure.Filters;
using NewCommerce.Infrastructure.Services;
using NewCommerce.Infrastructure.Services.Storage.Local;
using NewCommerce.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();



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
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
