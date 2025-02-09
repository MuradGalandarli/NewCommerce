using NewCommerce.Application.Repositoryes;
using NewCommerce.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Persistence.Repositoryes.InvoiceFile
{
    public class InvoiceFileReadRepository:ReadRepository<NewCommerce.Domain.Entitys.Common.InvoiceFile>,IInvoiceFileReadRepository
    {
        public InvoiceFileReadRepository(NewCommerceDb context ):base(context) { }
        

    }
}
