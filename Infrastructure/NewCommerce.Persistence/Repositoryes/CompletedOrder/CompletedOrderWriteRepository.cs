using NewCommerce.Application.Repositoryes.CompletedOrder;
using NewCommerce.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Persistence.Repositoryes.CompletedOrder
{
    public class CompletedOrderWriteRepository : WriteRepository<Domain.Entitys.CompletedOrder>, ICompletedOrderWriteRepository
    {
        public CompletedOrderWriteRepository(NewCommerceDb context) : base(context)
        {
        }
    }
}
