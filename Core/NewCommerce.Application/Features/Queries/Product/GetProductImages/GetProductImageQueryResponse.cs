﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Application.Features.Queries.Product.GetProductImages
{
    public class GetProductImageQueryResponse
    {
        public string FileName { get; set; }
        public string Path { get; set; }
        public Guid Id { get; set; }
    }
}
