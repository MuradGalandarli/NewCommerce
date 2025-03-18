using MediatR;

namespace NewCommerce.Application.Features.Commands.Order.CompletedOrder
{
    public class CompletedOrderCommandRequest : IRequest<CompletedOrderCommandResponse>
    {
        public string id { get; set; }
    }
}