using Microsoft.AspNetCore.SignalR;
using NewCommerce.Application.Abstractions.Hubs;
using NewCommerce.Application.Consts;
using NewCommerce.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.SignalR.HubService
{
    public class ProductHubService : IProductHubService
    {
        readonly IHubContext<ProductHub> _hubContext;

        public ProductHubService(IHubContext<ProductHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task ProductAddedMessageAsync(string mesage)
        {
            await _hubContext.Clients.All.SendAsync(ReceiceFunctionNames.ProductAddedMessage, mesage);
        }
    }
}
 