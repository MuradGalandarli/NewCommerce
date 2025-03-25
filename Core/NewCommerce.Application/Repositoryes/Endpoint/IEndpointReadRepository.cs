using NewCommerce.Application.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Application.Repositoryes.Endpoint
{
    public interface IEndpointReadRepository : IReadRepository<Domain.Entitys.Endpoint>
    {
    }
}
