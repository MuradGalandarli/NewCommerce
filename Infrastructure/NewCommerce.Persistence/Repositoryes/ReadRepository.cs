using Microsoft.EntityFrameworkCore;
using NewCommerce.Application.Repository;
using NewCommerce.Application.Repositoryes;
using NewCommerce.Domain.Entitys.Common;
using NewCommerce.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Persistence.Repositoryes
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        public NewCommerceDb _context;
        public ReadRepository(NewCommerceDb context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll()
        {
            return Table;
        }

        public async Task<T> GetById(string Id)
        {
         return  await Table.FirstOrDefaultAsync(x => x.Id == Guid.Parse(Id));
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method)
        {
            return await Table.FirstOrDefaultAsync(method);
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method)
        {
          return Table.Where(method);
        }
    }
}
