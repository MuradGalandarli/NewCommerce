using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using NewCommerce.Application;
using NewCommerce.Application.Abstractions.Storage;
using NewCommerce.Application.Features.Commands.Product.CreateProduct;
using NewCommerce.Application.Features.Commands.Product.DeleteProduct;
using NewCommerce.Application.Features.Commands.Product.DeleteProductImage;
using NewCommerce.Application.Features.Commands.Product.UpdateProduct;
using NewCommerce.Application.Features.Commands.Product.UploadProductImage;
using NewCommerce.Application.Features.Queries.Product.GetAllProduct;
using NewCommerce.Application.Features.Queries.Product.GetByIdProduct;
using NewCommerce.Application.Features.Queries.Product.GetProductImages;
using NewCommerce.Application.Repositoryes;
using NewCommerce.Application.RequestParameters;
using NewCommerce.Application.Services;
using NewCommerce.Application.ViewModels.Products;
using NewCommerce.Domain.Entitys;
using NewCommerce.Domain.Entitys.Common;
using NewCommerce.Persistence.Repositoryes;
using System.Runtime.InteropServices;

namespace NewCommerce.Api.Controllers
{
  //  [Authorize(AuthenticationSchemes = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductWriteRepository _product;
        private readonly IProductReadRepository _productRead;
        private readonly IWebHostEnvironment _webHostEnvironment;


        private readonly IFileReadRepository _fileReadRepository;
        private readonly IFileWriteRepository _fileWriteRepository;
        private readonly IInvoiceFileReadRepository _invoiceFileReadRepository;
        private readonly IInvoiceFileWriteRepository _invoiceFileWriteRepository;
        private readonly IProductImageWriteRepository _productImageFileWriteRepository;
        private readonly IProductImageReadRepository _productImageReadRepository;
        private readonly IStorageService _storageService;
        private readonly IConfiguration _configuration;
        readonly IMediator _mediator;

        public ProductController(IProductWriteRepository product,
            IProductReadRepository productRead,
            IWebHostEnvironment webHostEnvironment,
            IFileReadRepository fileReadRepository,
            IFileWriteRepository fileWriteRepository,
            IInvoiceFileReadRepository invoiceFileReadRepository,
            IInvoiceFileWriteRepository invoiceFileWriteRepository,
            IProductImageWriteRepository productImageFileWriteRepository,
            IProductImageReadRepository productImageReadRepository,
             IStorageService storageService,
             IConfiguration configuration,
            IMediator mediator

            )
        {
            _product = product;
            _productRead = productRead;
            _webHostEnvironment = webHostEnvironment;

            _fileReadRepository = fileReadRepository;
            _fileWriteRepository = fileWriteRepository;
            _invoiceFileReadRepository = invoiceFileReadRepository;
            _invoiceFileWriteRepository = invoiceFileWriteRepository;

            _productImageReadRepository = productImageReadRepository;
            _storageService = storageService;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _configuration = configuration;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProduct([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
            var datas = await _mediator.Send(getAllProductQueryRequest);
            return Ok(datas);
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(CreateProductCommandRequest model)
        {
            CreateProductCommandResponse status = await _mediator.Send(model);
            return Ok(status);
        }
        [HttpGet("GetByIdProduct")]
        public async Task<IActionResult> Get([FromQuery] GetByIdProductQueryRequest getByIdProductQueryRequest)
        {
            GetByIdProductQueryResponse data = await _mediator.Send(getByIdProductQueryRequest);
            return Ok(data);
        }
        [HttpPut("ProductUpdate")]
        public async Task<IActionResult> ProductUpdate(UpdateProductCommandRequest updateProductCommandRequest)
        {
            UpdateProductCommandResponse response = await _mediator.Send(updateProductCommandRequest);
            return Ok();
        }
        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] DeleteProductCommandRequest deleteProductCommandRequest)
        {
            DeleteProductCommandResponse deleteProductCommandResponse = await _mediator.Send(deleteProductCommandRequest);
            return Ok();
        }

        [HttpPost("action/{id}")]
        public async Task<IActionResult> Upload([FromRoute] UploadProductImageCommandRequest uploadProductImageCommandRequest)
        {
            UploadProductImageCommandResponse uploadProductImageCommandResponse = await _mediator.Send(uploadProductImageCommandRequest);
            return Ok();
        }

        [HttpGet("GetProductIdAllImage")]
        public async Task<IActionResult> GetProductImage([FromQuery]GetProductImageQueryRequest getProductImageQueryRequest)
        {
            List<GetProductImageQueryResponse> getProductImageQueryResponse = await _mediator.Send(getProductImageQueryRequest);
            return Ok(getProductImageQueryResponse);
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteProductImage([FromBody] DeleteProductImageCommandRequest deleteProductImageCommandRequest)
        {
            DeleteProductImageCommandResponse deleteProductImageCommandResponse = await _mediator.Send(deleteProductImageCommandRequest);
            return Ok();
        }

    }
}
