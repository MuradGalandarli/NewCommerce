using NewCommerce.Application.Repositoryes.Basket;
using NewCommerce.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Persistence.Repositoryes.Basket
{
    public class BasketWriteRepository:WriteRepository<Domain.Entitys.Basket>,IBasketWriteRepository
    {
        public BasketWriteRepository(NewCommerceDb newCommerceDb):base(newCommerceDb) 
        {
            
        }
    }
}
