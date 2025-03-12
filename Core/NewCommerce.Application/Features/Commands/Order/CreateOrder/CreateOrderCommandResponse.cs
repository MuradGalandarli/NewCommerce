using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Application.Features.Commands.Order.CreateOrder
{
    public class CreateOrderCommandResponse
    {
        public string Description { get; set; }
        public string Address { get; set; }
    }
}
