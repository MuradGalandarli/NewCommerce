﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Application.Features.Commands.Product.CreateProduct
{
    public class CreateProductCommandRequest : IRequest<CreateProductCommandResponse>
    {
        public string Name { get; set; }
        public int stock { get; set; }
        public float Price { get; set; }
    }
}
