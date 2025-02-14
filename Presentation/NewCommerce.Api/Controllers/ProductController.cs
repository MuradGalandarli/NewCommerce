using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using NewCommerce.Application;
using NewCommerce.Application.Abstractions.Storage;
using NewCommerce.Application.Features.Commands.CreateProduct;
using NewCommerce.Application.Features.Queries.GetAllProduct;
using NewCommerce.Application.Repositoryes;
using NewCommerce.Application.RequestParameters;
using NewCommerce.Application.Services;
using NewCommerce.Application.ViewModels.Products;
using NewCommerce.Domain.Entitys;
using NewCommerce.Domain.Entitys.Common;
using NewCommerce.Persistence.Repositoryes;

namespace NewCommerce.Api.Controllers
{
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
        public async Task<IActionResult> GetAllProduct([FromQuery]GetAllProductQueryRequest getAllProductQueryRequest )
        {

            //  var datass = _productRead.GetAll().ToList();
            var datas = await _mediator.Send(getAllProductQueryRequest);
            return Ok(datas);
        }

        [HttpPost("ada")]
        public async Task<IActionResult> AddProduct(CreateProductCommandRequest model)
        {
          CreateProductCommandResponse status = await _mediator.Send(model);
            return Ok(status);  
        }

        [HttpPost("action")]
        public async Task<IActionResult> Upload(IFormFile formFile, string id)
        {

            var datas = await _storageService.UploadAsync("files", Request.Form.Files);

            // var datas = await _storageService.UploadAsync("resource/files", Request.Form.Files);

            Product product = await _productRead.GetById(id);

            await _productImageFileWriteRepository.AddRangeAsync(datas.Select(d => new ProductImageFile()
            {

                FileName = d.fileName,
                Path = d.pathOrContainerName,
                Storage = _storageService.StorageName,
                Products = new List<Product>() { product }

            }).ToList());
            await _productImageFileWriteRepository.SaveAsync();

            return Ok();
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductImage(string id)
        {
            var a = _productRead.Table.Include(p => p.ProductImageFiles).FirstOrDefault(p => p.Id == Guid.Parse(id));
            var product = _productRead.Table.Include(p => p.ProductImageFiles).FirstOrDefault(p => p.Id == Guid.Parse(id));


            return Ok(product.ProductImageFiles.Select(x => new
            {
                Path = $"{_configuration["BaseStorageUrl"]}/{x.Path}",
                x.FileName
            }));

        }
        [HttpDelete("[action]/{productId}/{imageId}")]
        public async Task<IActionResult> DeleteProducyImage(string productId, string imageId)
        {
            var product = _productRead.Table.Include(p => p.ProductImageFiles).FirstOrDefault(p => p.Id == Guid.Parse(productId));
            ProductImageFile productImageFile = product.ProductImageFiles.FirstOrDefault(x => x.Id == Guid.Parse(imageId));
            _productImageFileWriteRepository.Remove(productImageFile);
            _productImageFileWriteRepository.SaveAsync();
            return Ok();

        }

    }
}
