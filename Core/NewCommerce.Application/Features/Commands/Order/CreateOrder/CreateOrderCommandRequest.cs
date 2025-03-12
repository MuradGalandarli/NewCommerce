using MediatR;
using NewCommerce.Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Application.Features.Commands.Order.CreateOrder
{
    public class CreateOrderCommandRequest:IRequest<CreateOrderCommandResponse>
    {
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? CustomerId { get; set; }
        public Guid BasketId { get; set; }
     
     
    }
}
