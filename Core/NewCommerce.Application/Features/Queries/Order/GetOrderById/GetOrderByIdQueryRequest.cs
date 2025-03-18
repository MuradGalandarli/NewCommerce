using MediatR;

namespace NewCommerce.Application.Features.Queries.Order.GetOrderById
{
    public class GetOrderByIdQueryRequest:IRequest<GetOrderByIdQueryResponse>
    {
        public string id { get; set; }
        public bool Completed { get; set; }
    }
}