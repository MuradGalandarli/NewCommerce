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
    public class MenuWriteRepository : WriteRepository<Domain.Entitys.Menu>, IMenuWriteRepository
    {
        public MenuWriteRepository(NewCommerceDb context) : base(context)
        {
        }
    }
}
