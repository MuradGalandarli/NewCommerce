using NewCommerce.Domain.Entitys.Common;
using NewCommerce.Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Application.Features.Queries.Product.GetByIdProduct
{
    public class GetByIdProductQueryResponse
    {
        public string? Name { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }
       
    }
}
