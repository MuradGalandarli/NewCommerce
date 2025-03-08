using MediatR;
using NewCommerce.Application.Abstractions.Storage;
using NewCommerce.Application.Repositoryes;
using NewCommerce.Domain.Entitys.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P = NewCommerce.Domain.Entitys;

namespace NewCommerce.Application.Features.Commands.ProductImageFile.UploadProductImage
{
    public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommandRequest, UploadProductImageCommandResponse>
    {
        readonly IStorageService _storageService;
        readonly IProductImageWriteRepository _productImageWriteRepository;
        readonly IProductImageReadRepository _productImageReadRepository;
        readonly IProductReadRepository _productReadRepository;


        public UploadProductImageCommandHandler(IStorageService storageService, IProductReadRepository productReadRepository, IProductImageReadRepository productImageReadRepository, IProductImageWriteRepository productImageWriteRepository)
        {
            _storageService = storageService;
            _productReadRepository = productReadRepository;
            _productImageReadRepository = productImageReadRepository;
            _productImageWriteRepository = productImageWriteRepository;
        }

        public async Task<UploadProductImageCommandResponse> Handle(UploadProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            var datas = await _storageService.UploadAsync("files", request.formFile);

            {
                P.Product product = await _productReadRepository.GetById(request.id);

                await _productImageWriteRepository.AddRangeAsync(datas.Select(d => new Domain.Entitys.Common.ProductImageFile()
                {

                    FileName = d.fileName,
                    Path = d.pathOrContainerName,
                    Storage = _storageService.StorageName,
                    Products = new List<P.Product>() { product }

                }).ToList());
                await _productImageWriteRepository.SaveAsync();
            }
            return new();
        }



    }
}
