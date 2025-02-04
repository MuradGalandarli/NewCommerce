using Microsoft.Extensions.Logging;
using NewCommerce.Domain.Entitys.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Application.Repositoryes
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll(bool tracking = true);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> method,bool tracking = true);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> method,bool tracking = true);
        Task<T> GetById(string Id,bool tracking = true);

    }
}
