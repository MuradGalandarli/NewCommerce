using Microsoft.AspNetCore.SignalR;
using NewCommerce.Application.Abstractions.Hubs;
using NewCommerce.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.SignalR.HubService
{
    public class OrderHubService : IOrderHubService
    {
        IHubContext<OrderHub> _hubContext;

        public OrderHubService(IHubContext<OrderHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task OrderAddedMessageAsync(string message)
        {
           await _hubContext.Clients.All.SendAsync(ReceiceFunctionNames.OrderAddedMessage,message);
        }
    }
}
