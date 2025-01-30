using NewCommerce.Application.Repository;
using NewCommerce.Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Application.Repositoryes
{
    public interface ICustomerWriteRepository : IWriteRepository<Customer>
    {
    }
}
