using MediatR;
using NewCommerce.Domain.Entitys.Common;
using NewCommerce.Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using NewCommerce.Application.Repositoryes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NewCommerce.Application.Features.Commands.ProductImageFile.DeleteProductImage
{
    public class DeleteProductImageCommandHandler : IRequestHandler<DeleteProductImageCommandRequest, DeleteProductImageCommandResponse>
    {
        readonly IProductWriteRepository _productWriteRepository;
        readonly IProductReadRepository _productReadRepository;
        readonly IProductImageWriteRepository _productImageWriteRepository;

        public DeleteProductImageCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IProductImageWriteRepository productImageWriteRepository = null)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _productImageWriteRepository = productImageWriteRepository;
        }

        public async Task<DeleteProductImageCommandResponse> Handle(DeleteProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            var product = _productReadRepository.Table.Include(p => p.ProductImageFiles).FirstOrDefault(p => p.Id == Guid.Parse(request.productId));
            Domain.Entitys.Common.ProductImageFile? productImageFile = product.ProductImageFiles.FirstOrDefault(x => x.Id == Guid.Parse(request.imageId));
            if (productImageFile != null)
                product.ProductImageFiles.Remove(productImageFile);
            await _productImageWriteRepository.SaveAsync();
            return new();
        }
    }
}
