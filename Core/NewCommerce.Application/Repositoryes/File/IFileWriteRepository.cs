﻿using NewCommerce.Application.Repository;
using F = NewCommerce.Domain.Entitys.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Application.Repositoryes
{
    public interface IFileWriteRepository:IWriteRepository<F::File>
    {

    }
}
