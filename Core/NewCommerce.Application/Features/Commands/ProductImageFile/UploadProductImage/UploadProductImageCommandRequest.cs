using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Application.Features.Commands.ProductImageFile.UploadProductImage
{
    public class UploadProductImageCommandRequest : IRequest<UploadProductImageCommandResponse>
    {
        //IFormFile formFile, string id
        public string id { get; set; }
        public IFormFileCollection formFile { get; set; }
    }
}
