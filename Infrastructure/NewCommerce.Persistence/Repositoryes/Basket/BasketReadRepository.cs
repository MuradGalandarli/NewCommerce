using NewCommerce.Application.Repositoryes.Basket;
using NewCommerce.Application.Repositoryes.BasketItem;
using NewCommerce.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Persistence.Repositoryes
{
    public class BasketReadRepository : ReadRepository<Domain.Entitys.Basket>, IBasketReadRepository
    {
        public BasketReadRepository(NewCommerceDb context) : base(context)
        {
        }
    }
}
