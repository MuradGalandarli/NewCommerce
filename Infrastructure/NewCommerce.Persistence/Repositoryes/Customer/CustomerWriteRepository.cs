using NewCommerce.Application.Repository;
using NewCommerce.Application.Repositoryes;
using NewCommerce.Domain.Entitys;
using NewCommerce.Persistence.Context;
using NewCommerce.Persistence.Repositoryes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Persistence.Repositoryes
{
    public class CustomerWriteRepository : WriteRepository<Customer>,ICustomerWriteRepository
    {
        public CustomerWriteRepository(NewCommerceDb context) : base(context)
        {
        }
    }
}
