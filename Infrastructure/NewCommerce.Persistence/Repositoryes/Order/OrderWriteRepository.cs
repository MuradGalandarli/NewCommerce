using NewCommerce.Application.Repository;
using NewCommerce.Application.Repositoryes;
using NewCommerce.Domain.Entitys;
using NewCommerce.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Persistence.Repositoryes
{
    public class OrderWriteRepository:WriteRepository<Order>,IOrderWriteRepository
    {
        public OrderWriteRepository(NewCommerceDb _context):base(_context) { }
       
    }
}
