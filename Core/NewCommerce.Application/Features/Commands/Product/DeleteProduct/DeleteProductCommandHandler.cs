using MediatR;
using NewCommerce.Application.Repositoryes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Application.Features.Commands.Product.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, DeleteProductCommandResponse>
    {
        readonly IProductWriteRepository _productWriteRepository;
      
        public DeleteProductCommandHandler(IProductWriteRepository productWriteRepository)
        {
            _productWriteRepository = productWriteRepository;
            
        }



        public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
           
           await _productWriteRepository.RemoveAsync(request.id);
           await _productWriteRepository.SaveAsync();
           return new();
        }
    }
}
