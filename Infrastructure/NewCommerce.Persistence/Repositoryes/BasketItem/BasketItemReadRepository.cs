using NewCommerce.Application.Repositoryes.BasketItem;
using NewCommerce.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Persistence.Repositoryes.BasketItem
{
    public class BasketItemReadRepository : ReadRepository<Domain.Entitys.BasketItem>, IBasketItemReadRepository
    {
        public BasketItemReadRepository(NewCommerceDb context) : base(context)
        {
        }
    }
}
