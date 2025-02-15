using MediatR;
using NewCommerce.Application.Repositoryes;
using NewCommerce.Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace NewCommerce.Application.Features.Commands.Product.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        readonly IProductWriteRepository _productWriteRepository;

        public CreateProductCommandHandler(IProductWriteRepository productWriteRepository)
        {
            _productWriteRepository = productWriteRepository;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var data = await _productWriteRepository.AddAsync(new() { Name = request.Name, Price = request.Price, Stock = request.stock });
            //await _product.RemoveAsync("4317d7c6-618a-43e5-b578-8fde369db44c");
            await _productWriteRepository.SaveAsync();

            return new();

        }
    }
}
