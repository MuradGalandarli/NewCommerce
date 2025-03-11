using MediatR;
using NewCommerce.Application.Abstractions.Services;
using NewCommerce.Application.ViewModels.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Application.Features.Commands.Basket.AddItemToBasket
{
    public class AddItemToBasketCommandHandler : IRequestHandler<AddItemToBasketCommandRequest, AddItemToBasketCommandResponse>
    {
        readonly IBasketService _basketService;

        public AddItemToBasketCommandHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<AddItemToBasketCommandResponse> Handle(AddItemToBasketCommandRequest request, CancellationToken cancellationToken)
        {
            VM_Create_BasketItem basketItem = new() 
            {
                ProductId = request.ProductId,
                Quantity = request.Quantity,
            };

          await _basketService.AddItemToBasketAsync(basketItem);

            return new();
        }
    }
}
