using NewCommerce.Application.Repositoryes.Endpoint;
using NewCommerce.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Persistence.Repositoryes.Endpoint
{
    public class EndpointReadRepository : ReadRepository<Domain.Entitys.Endpoint>, IEndpointReadRepository
    {
        public EndpointReadRepository(NewCommerceDb context) : base(context)
        {
        }
    }
}
