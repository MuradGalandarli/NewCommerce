using NewCommerce.Domain.Entitys;
using NewCommerce.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Persistence.Repositoryes
{
    public class ProductReadRepository:ReadRepository<Product>
    {
        public ProductReadRepository(NewCommerceDb _context) : base(_context) { }
        
    }
}
