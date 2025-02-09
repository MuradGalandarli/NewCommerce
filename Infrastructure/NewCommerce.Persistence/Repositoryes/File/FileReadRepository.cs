using NewCommerce.Application.Repositoryes;
using NewCommerce.Persistence.Context;
using NewCommerce.Persistence.Repositoryes.InvoiceFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Persistence.Repositoryes
{
    public  class FileReadRepository:ReadRepository<NewCommerce.Domain.Entitys.Common.File>,IFileReadRepository
    {
        public FileReadRepository(NewCommerceDb context):base(context)
        {
            
        }
    }
}
