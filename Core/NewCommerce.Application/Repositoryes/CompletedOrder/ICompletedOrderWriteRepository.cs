using NewCommerce.Application.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Application.Repositoryes.CompletedOrder
{
    public interface ICompletedOrderWriteRepository:IWriteRepository<Domain.Entitys.CompletedOrder>
    {
    }
}
