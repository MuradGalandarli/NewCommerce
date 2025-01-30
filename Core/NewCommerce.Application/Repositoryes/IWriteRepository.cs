using NewCommerce.Application.Repositoryes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Application.Repository
{
    public interface IWriteRepository<T> : IRepository<T> where T : class
    {
        Task<bool> AddAsync(T model);
        Task<bool> AddRangeAsync(List<T> datas);
        bool Remove(T model);
        bool RemoveRange(T datas);
        Task<bool> RemoveAsync(string id);
        bool Update(T model);
        Task<int> SaveAsync();

    }
}
