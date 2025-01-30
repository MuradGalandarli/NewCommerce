using NewCommerce.Domain.Entitys;
using NewCommerce.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Persistence.Repositoryes
{
    public class ProductWriteRepository : WriteRepository<Product>
    {
        public ProductWriteRepository(NewCommerceDb context) : base(context)
        {
        }
    }
}
