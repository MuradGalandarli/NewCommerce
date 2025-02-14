using MediatR;
using NewCommerce.Application.RequestParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Application.Features.Queries.GetAllProduct
{
    public class GetAllProductQueryRequest:IRequest<GetAllProductQueryResponse>
    {
        //public Pagination pagination { get; set; }
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
    }
}
