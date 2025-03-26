using MediatR;
using NewCommerce.Application.Features.Queries.Order.GetAllOrders;

namespace NewCommerce.Application.Features.Queries.AppUsers.GetAllUsers
{
    public class GetAllUserQueryRequest:IRequest<GetAllUserQueryResponse>
    {
        public int page { get; set; }
        public int size  { get; set; }
    }
}