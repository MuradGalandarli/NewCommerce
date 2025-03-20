using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewCommerce.Application.Consts;
using NewCommerce.Application.CustomAttributes;
using NewCommerce.Application.Enums;
using NewCommerce.Application.Features.Commands.Order.CompletedOrder;
using NewCommerce.Application.Features.Commands.Order.CreateOrder;
using NewCommerce.Application.Features.Queries.Order.GetAllOrders;
using NewCommerce.Application.Features.Queries.Order.GetOrderById;

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
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Orders, ActionType = ActionType.Writing, Definition = "Create Order")]
        public async Task<ActionResult> CreateOrder(CreateOrderCommandRequest createOrderCommandRequest)
        {
            CreateOrderCommandResponse response = await _mediator.Send(createOrderCommandRequest);
            return Ok(response);
        }

        [HttpGet]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Orders, ActionType = ActionType.Reading, Definition = "Get All Orders")]
        public async Task<ActionResult> GetAllOrders([FromQuery] GetAllOrdersQueryRequest getAllOrdersQueryRequest)
        {
            GetAllOrdersQueryResponse response = await _mediator.Send(getAllOrdersQueryRequest);
            return Ok(response);
        }
        [HttpGet("{id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Orders, ActionType = ActionType.Reading, Definition = "Get Order By Id ")]
        public async Task<ActionResult> GetOrderById([FromRoute] GetOrderByIdQueryRequest getOrderByIdQueryRequest)
        {
            GetOrderByIdQueryResponse getOrderByIdQueryResponse = await _mediator.Send(getOrderByIdQueryRequest);
            return Ok(getOrderByIdQueryResponse);   
        }

        [HttpGet("completed-order")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Orders, ActionType = ActionType.Updating, Definition = "Complete Order")]
        public async Task<IActionResult>CompletedOrder([FromRoute]CompletedOrderCommandRequest completedOrderCommandRequest)
        {
            CompletedOrderCommandResponse completedOrderCommandResponse = await _mediator.Send(completedOrderCommandRequest);
            return Ok(completedOrderCommandResponse);
        }

    }
}
