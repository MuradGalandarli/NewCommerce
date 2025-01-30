using Microsoft.EntityFrameworkCore;
using NewCommerce.Domain.Entitys;


namespace NewCommerce.Persistence.Context
{
    public class NewCommerceDb : DbContext
    {
        public NewCommerceDb(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
