using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NewCommerce.Application.Repository;
using NewCommerce.Domain.Entitys.Common;
using NewCommerce.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Persistence.Repositoryes
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly NewCommerceDb _context;
        public WriteRepository(NewCommerceDb context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task<bool> AddAsync(T model)
        {
            EntityEntry<T> entity = await Table.AddAsync(model);
            return entity.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<T> datas)
        {
            await Table.AddRangeAsync(datas);
            return true;
        }

        public bool Remove(T model)
        {
            EntityEntry<T> entity = Table.Remove(model);
            return entity.State == EntityState.Deleted;
        }

        public async Task<bool> RemoveAsync(string id)
        {
            var data = await Table.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
            Remove(data);
            return true;
        }

        public bool RemoveRange(T datas)
        {
            Table.RemoveRange(datas);
            return true;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public bool Update(T model)
        {
           EntityEntry<T> data =  Table.Update(model);
           return data.State == EntityState.Modified;   
        }
    }
}
