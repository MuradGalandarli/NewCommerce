using Google.Apis.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Net;
using System.Net.Mime;
using System.Text.Json;

namespace NewCommerce.Api.Exrensions
{
    public static class ConfigureExceptionHandlerExtension
    {
        public static void ConfigureExcetionHandler<T>(this WebApplication application, ILogger<T> _logger)
        {
            application.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = MediaTypeNames.Application.Json;
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if(contextFeature != null)
                    {
                        _logger.LogError(contextFeature.Error.Message);
                      await context.Response.WriteAsync(JsonSerializer.Serialize(new
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message,
                            Title = "Xeta alindi"

                        }));
                    }

                }); 
            });
        }
    }
}
