using NewCommerce.Domain.Entitys.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Domain.Entitys
{
    public class Order:BaseEntity
    {
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? OrderCode { get; set; }

        //public string? CustomerId { get; set; }
        //public Guid BasketId { get; set; }
        //public Customer Customer { get; set; }
        //public ICollection<Product> Products { get; set; }  

        public Basket Basket { get; set; }
        public CompletedOrder CompletedOrder { get; set; }
    }
}
