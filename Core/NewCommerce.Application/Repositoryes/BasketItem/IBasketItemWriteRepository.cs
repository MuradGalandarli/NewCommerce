using NewCommerce.Application.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Application.Repositoryes.BasketItem
{
    public interface IBasketItemWriteRepository : IWriteRepository<Domain.Entitys.BasketItem>
    {
    }
}
