using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Application.Features.Commands.Product.DeleteProductImage
{
    public class DeleteProductImageCommandRequest:IRequest<DeleteProductImageCommandResponse>
    {
            public string productId { get; set; }
            public string imageId { get; set; }
    }
}
