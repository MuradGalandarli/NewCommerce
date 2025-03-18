using NewCommerce.Application.Repositoryes.CompletedOrder;
using NewCommerce.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Persistence.Repositoryes.CompletedOrder
{
    public class CompletedOrderReadRepository : ReadRepository<Domain.Entitys.CompletedOrder>, ICompletedOrderReadRepository
    {
        public CompletedOrderReadRepository(NewCommerceDb context) : base(context)
        {
        }
    }
}
