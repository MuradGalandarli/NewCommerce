using NewCommerce.Application.Abstractions.Services;
using NewCommerce.Application.Repositoryes;
using NewCommerce.Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NewCommerce.Persistence.Services
{
    public class ProductService : IProductService
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IQRCodeService _qrCodeService;
        public ProductService(IProductReadRepository productReadRepository, IQRCodeService qrCodeService)
        {
            _productReadRepository = productReadRepository;
            _qrCodeService = qrCodeService;
        }

        public async Task<byte[]> QrCodeToProductAsync(string productId)
        {
            Product product = await _productReadRepository.GetById(productId);
            if (product == null)
                throw new Exception("Product not found");

            var plainObject = new
            {
                product.Id,
                product.Name,
                product.Price,
                product.Stock,
                product.CreateDate
            };
            string plainText = JsonSerializer.Serialize(plainObject);

            return _qrCodeService.GenerateQRCode(plainText);
        }
    }
    }
