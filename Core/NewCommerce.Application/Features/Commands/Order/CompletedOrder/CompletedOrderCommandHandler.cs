using MediatR;
using NewCommerce.Application.Abstractions.Services;
using NewCommerce.Application.DTOs.Order;
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
        readonly IMailService _mailService;

        public CompletedOrderCommandHandler(IOrderService orderService, IMailService mailService)
        {
            _orderService = orderService;
            _mailService = mailService;
        }

        public async Task<CompletedOrderCommandResponse> Handle(CompletedOrderCommandRequest request, CancellationToken cancellationToken)
        {
          (bool succeeded, CompletedOrderDto dto) result = await _orderService.CompletedOrderAsync(request.id);
            if (result.succeeded)
            {
                await _mailService.CompletedOrderMailAsync(result.dto.To,result.dto.OrderCode,result.dto.OrderDate,result.dto.UserName);
            }
            return new();
        }
    }
}
