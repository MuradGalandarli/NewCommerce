using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using NewCommerce.Application.Repositoryes;
using NewCommerce.Application.RequestParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Application.Features.Queries.Product.GetAllProduct
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly ILogger<GetAllProductQueryHandler> _logger;
        public GetAllProductQueryHandler(IProductReadRepository _productReadRepository, ILogger<GetAllProductQueryHandler> logger)
        {
            this._productReadRepository = _productReadRepository;
            _logger = logger;
        }
        public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            int totalProductCount = _productReadRepository.GetAll().Count();
            throw new Exception("test");
            var datas = _productReadRepository.GetAll(false).Skip(request.Size * request.Page).Take(request.Size);
            _logger.LogInformation("List edildi");
            return new()
            {
                Products = datas,
                TotalProductCount = totalProductCount


            };
            

        }
    }
}
