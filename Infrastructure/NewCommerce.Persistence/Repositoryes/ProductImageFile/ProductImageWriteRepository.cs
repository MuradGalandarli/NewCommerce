using NewCommerce.Application;
using NewCommerce.Domain.Entitys.Common;
using NewCommerce.Persistence.Context;
using NewCommerce.Persistence.Repositoryes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Persistence
{
    public class ProductImageWriteRepository : WriteRepository<ProductImageFile>,IProductImageWriteRepository
    {
        public ProductImageWriteRepository(NewCommerceDb context) : base(context)
        {
        }
    }
}
