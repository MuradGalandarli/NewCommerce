using Microsoft.Extensions.DependencyInjection;
using NewCommerce.Application.Services;
using NewCommerce.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices( this IServiceCollection services)
        {
            services.AddScoped<IFileService, FileService>();
        
        }
    }
}
