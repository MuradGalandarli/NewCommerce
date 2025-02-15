using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Application.Features.Commands.Product.UpdateProduct
{
    public class UpdateProductCommandRequest:IRequest<UpdateProductCommandResponse>
    {
        public string id { get; set; }
        public string? Name { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }
    }
}
