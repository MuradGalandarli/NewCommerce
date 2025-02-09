using NewCommerce.Application;
using NewCommerce.Application.Repositoryes;
using NewCommerce.Domain.Entitys.Common;
using NewCommerce.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Persistence.Repositoryes
    
{
    public class ProductImageReadRepository : ReadRepository<ProductImageFile>, IProductImageReadRepository
    {
        public ProductImageReadRepository(NewCommerceDb context) : base(context)
        {
        }
    }
}
