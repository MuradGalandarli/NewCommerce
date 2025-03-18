using MediatR;
using NewCommerce.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Application.Features.Commands.Order.CompletedOrder
{
  
    public class CompletedOrderCommandHandler : IRequestHandler<CompletedOrderCommandRequest, CompletedOrderCommandResponse>
    {
        readonly IOrderService _orderService;

        public CompletedOrderCommandHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<CompletedOrderCommandResponse> Handle(CompletedOrderCommandRequest request, CancellationToken cancellationToken)
        {
           await _orderService.CompletedOrderAsync(request.id);

            return new();
        }
    }
}
