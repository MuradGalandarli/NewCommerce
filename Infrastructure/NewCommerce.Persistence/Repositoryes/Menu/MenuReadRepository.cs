using NewCommerce.Application.Repositoryes.Endpoint;
using NewCommerce.Application.Repositoryes.Menu;
using NewCommerce.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Persistence.Repositoryes.Menu
{
    public class MenuReadRepository : ReadRepository<Domain.Entitys.Menu>, IMenuReadRepository
    {
        public MenuReadRepository(NewCommerceDb context) : base(context)
        {
        }
    }
}
