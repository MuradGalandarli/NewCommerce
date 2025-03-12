using Microsoft.Extensions.DependencyInjection;
using NewCommerce.Application.Abstractions.Hubs;
using NewCommerce.SignalR.HubService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.SignalR
{
    public static class ServiceRegistration
    {
        public static void AddSignalService(this IServiceCollection service) {

            service.AddScoped<IProductHubService, ProductHubService>();
            service.AddScoped<IOrderHubService, OrderHubService>();
            service.AddSignalR();
        }
    }
}
