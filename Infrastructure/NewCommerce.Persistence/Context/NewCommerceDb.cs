﻿using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewCommerce.Domain.Entitys;
using NewCommerce.Domain.Entitys.Common;
using NewCommerce.Domain.Identity;


namespace NewCommerce.Persistence.Context
{
    public class NewCommerceDb : IdentityDbContext<AppUser,AppRole,string>
    {
        public NewCommerceDb(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Domain.Entitys.Common.File>Files { get; set; }
        public DbSet<ProductImageFile> ProductImageFiles { get; set; }
        public DbSet<InvoiceFile> InvoiceFiles { get; set; }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntity>();
            foreach (var data in datas)
            {
                {
                    switch (data.State)
                    {
                        case EntityState.Added:
                            {
                                data.Entity.CreateDate = DateTime.UtcNow;
                                break;
                            }

                        case EntityState.Modified:
                            {
                                data.Entity.UpdateDate = DateTime.UtcNow;
                                break;
                            }
                    }
                }

            }

                return await base.SaveChangesAsync(cancellationToken);
            

        }

    }
}
