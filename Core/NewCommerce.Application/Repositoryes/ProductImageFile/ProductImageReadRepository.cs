﻿using NewCommerce.Application.Repositoryes;
using NewCommerce.Domain.Entitys.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Application
{
    public interface  IProductImageReadRepository:IReadRepository<ProductImageFile>
    {
    }
}
