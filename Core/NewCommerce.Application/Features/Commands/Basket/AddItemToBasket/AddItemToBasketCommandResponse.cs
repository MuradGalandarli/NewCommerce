using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Application.Features.Commands.Basket.AddItemToBasket
{
    public class AddItemToBasketCommandResponse
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }

    }
}
