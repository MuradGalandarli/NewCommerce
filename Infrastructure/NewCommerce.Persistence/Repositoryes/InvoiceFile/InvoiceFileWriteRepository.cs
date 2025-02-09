using NewCommerce.Application.Repositoryes;
using NewCommerce.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Persistence.Repositoryes
{
    public class InvoiceFileWriteRepository:WriteRepository<NewCommerce.Domain.Entitys.Common.InvoiceFile>,IInvoiceFileWriteRepository
    {
        public InvoiceFileWriteRepository(NewCommerceDb context):base(context) 
        {
            
        }
    }
}
