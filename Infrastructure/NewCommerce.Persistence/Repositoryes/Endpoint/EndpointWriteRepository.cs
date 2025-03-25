using NewCommerce.Application.Repositoryes.Endpoint;
using NewCommerce.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Persistence.Repositoryes.Endpoint
{
    public class EndpointWriteRepository : WriteRepository<Domain.Entitys.Endpoint>, IEndpointWriteRepository
    {
        public EndpointWriteRepository(NewCommerceDb context) : base(context)
        {
        }
    }
}
