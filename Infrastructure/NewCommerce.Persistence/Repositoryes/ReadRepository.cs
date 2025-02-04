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
        
        public IQueryable<T> GetAll(bool tracking)
        {
            var query = Table.AsNoTracking();
            if(!tracking)
            {
               query = query.AsNoTracking();
            }
            return query;
        }

        public async Task<T> GetById(string Id, bool tracking = true)
        {
           var query = Table.AsQueryable();
            if (!tracking)
            { 
              query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync(x => x.Id == Guid.Parse(Id));

        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if(!tracking)
            {
                query = query.AsNoTracking();
            }
           
            return await query.FirstOrDefaultAsync(method);
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
        { 
            var query = Table.AsQueryable();
            if (!tracking)
            {
                query = query.AsNoTracking();
            }
                        
            return query.Where(method);
        }
    }
}
