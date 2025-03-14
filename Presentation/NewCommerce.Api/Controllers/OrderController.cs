using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewCommerce.Application.Features.Commands.Order.CreateOrder;
using NewCommerce.Application.Features.Queries.Order;

namespace NewCommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]

    public class OrderController : ControllerBase
    {
   
            readonly IMediator _mediator;
            public OrderController(IMediator mediator)
            {
                _mediator = mediator;
            }

            [HttpPost]
            public async Task<ActionResult> CreateOrder(CreateOrderCommandRequest createOrderCommandRequest)
            {
                CreateOrderCommandResponse response = await _mediator.Send(createOrderCommandRequest);
                return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllOrders([FromQuery] GetAllOrdersQueryRequest getAllOrdersQueryRequest)
        {
            GetAllOrdersQueryResponse response = await _mediator.Send(getAllOrdersQueryRequest);
            return Ok(response);
        }


    }
}
