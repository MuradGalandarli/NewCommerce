﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewCommerce.Application.Features.Commands.Basket.AddItemToBasket;
using NewCommerce.Application.Features.Commands.Basket.RemoveBasketItem;
using NewCommerce.Application.Features.Commands.Basket.UpdateQuantity;
using NewCommerce.Application.Features.Queries.Basket.GetBasketItems;

namespace NewCommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
            readonly IMediator _mediator;

            public BasketsController(IMediator mediator)
            {
                _mediator = mediator;
            }

            [HttpGet] 
            public async Task<IActionResult> GetBasketItems([FromQuery] GetBasketItemsQueryRequest getBasketItemsQueryRequest)
            {
                List<GetBasketItemsQueryReponse> response = await _mediator.Send(getBasketItemsQueryRequest);
                return Ok(response);
            }

            [HttpPost]
            public async Task<IActionResult> AddItemToBasket(AddItemToBasketCommandRequest addItemToBasketCommandRequest)
            {
                AddItemToBasketCommandResponse response = await _mediator.Send(addItemToBasketCommandRequest);
                return Ok(response);
            }

            [HttpPut]
            public async Task<IActionResult> UpdateQuantity(UpdateQuantityCommandRequest updateQuantityCommandRequest)
            {
                UpdateQuantityCommandResponse response = await _mediator.Send(updateQuantityCommandRequest);
                return Ok(response);
            }

            [HttpDelete("{BasketItemId}")]
            public async Task<IActionResult> RemoveBasketItem([FromRoute] RemoveBasketItemCommandRequest removeBasketItemCommandRequest)
            {
                RemoveBasketItemCommandResponse response = await _mediator.Send(removeBasketItemCommandRequest);
                return Ok(response);
            }
        }
    }

