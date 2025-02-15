using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NewCommerce.Application.Repositoryes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Application.Features.Queries.Product.GetProductImages
{
    public class GetProductImageQueryHandler : IRequestHandler<GetProductImageQueryRequest, List<GetProductImageQueryResponse>>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IConfiguration _configuration;

        public GetProductImageQueryHandler(IProductReadRepository productReadRepository, IConfiguration configuration)
        {
            _productReadRepository = productReadRepository;
            _configuration = configuration;
        }

        public async Task<List<GetProductImageQueryResponse>> Handle(GetProductImageQueryRequest request, CancellationToken cancellationToken)
        {
            
            var product = _productReadRepository.Table.Include(p => p.ProductImageFiles).FirstOrDefault(p => p.Id == Guid.Parse(request.id));


            return product.ProductImageFiles.Select(x => new GetProductImageQueryResponse
            {
                Id = x.Id,
                FileName = x.FileName,
                Path = x.Path
                
            }).ToList();

        }
    }
}
