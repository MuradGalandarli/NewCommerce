﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Application.Abstractions.Services
{
    public interface IProductService
    {
        Task<byte[]> QrCodeToProductAsync(string productId);
    }
}
