using MediatR;
using Microsoft.AspNetCore.Components;
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
        public GetAllProductQueryHandler(IProductReadRepository _productReadRepository)
        {
            this._productReadRepository = _productReadRepository;
        }
        public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            int totalCount = _productReadRepository.GetAll().Count();
            var datas = _productReadRepository.GetAll(false).Skip(request.Size * request.Page).Take(request.Size);
            return new()
            {
                Products = datas,
                TotalCount = totalCount

            };

        }
    }
}
