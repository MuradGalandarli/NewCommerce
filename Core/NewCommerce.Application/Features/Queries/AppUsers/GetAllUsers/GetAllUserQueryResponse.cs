using System.Diagnostics.Contracts;

namespace NewCommerce.Application.Features.Queries.AppUsers.GetAllUsers
{
    public class GetAllUserQueryResponse
    {
        public object Users { get; set; }
        public int Count { get; set; }
       

    }
}