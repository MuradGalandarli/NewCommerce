using MediatR;
using NewCommerce.Application.Repositoryes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P = NewCommerce.Domain.Entitys;

namespace NewCommerce.Application.Features.Commands.Product.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        readonly IProductWriteRepository _productWriteRepository;
        readonly IProductReadRepository _productReadRepository;

        public UpdateProductCommandHandler(IProductWriteRepository productWriteRepository,
            IProductReadRepository productReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
          P.Product product = await _productReadRepository.GetById(request.id);
            product.Name = request.Name;
            product.Price = request.Price;
            product.Stock = request.Stock;
           await _productWriteRepository.SaveAsync();
            return new();
        }
    }
}
