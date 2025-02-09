using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewCommerce.Application.Repositoryes;
using NewCommerce.Persistence.Context;

namespace NewCommerce.Persistence.Repositoryes
{
    public class FileWriteRepository : WriteRepository<NewCommerce.Domain.Entitys.Common.File>,IFileWriteRepository
    {
        public FileWriteRepository(NewCommerceDb context) : base(context)
        { }

    }
}
