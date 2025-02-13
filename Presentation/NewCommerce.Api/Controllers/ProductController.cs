using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NewCommerce.Application;
using NewCommerce.Application.Abstractions.Storage;
using NewCommerce.Application.Repositoryes;
using NewCommerce.Application.RequestParameters;
using NewCommerce.Application.Services;
using NewCommerce.Application.ViewModels.Products;
using NewCommerce.Domain.Entitys.Common;

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
        private readonly IProductImageWriteRepository _productWriteRepository;
        private readonly IProductImageReadRepository _productReadRepository;
        private readonly IStorageService _storageService;
        private readonly IProductImageWriteRepository _productImageFileWriteRepository;

        public ProductController(IProductWriteRepository product, 
            IProductReadRepository productRead,
            IWebHostEnvironment webHostEnvironment,
            IFileReadRepository fileReadRepository, 
            IFileWriteRepository fileWriteRepository, 
            IInvoiceFileReadRepository invoiceFileReadRepository,
            IInvoiceFileWriteRepository invoiceFileWriteRepository, 
            IProductImageWriteRepository productWriteRepository,
            IProductImageReadRepository productReadRepository,
             IStorageService storageService,
             IProductImageWriteRepository productImageFileWriteRepository
             
            )
        {
            _product = product;
            _productRead = productRead;
            _webHostEnvironment = webHostEnvironment;
           
            _fileReadRepository = fileReadRepository;
            _fileWriteRepository = fileWriteRepository;
            _invoiceFileReadRepository = invoiceFileReadRepository;
            _invoiceFileWriteRepository = invoiceFileWriteRepository;
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _storageService = storageService;
            _productImageFileWriteRepository = productImageFileWriteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProduct([FromQuery] Pagination pagination)
        {
            //  var datass = _productRead.GetAll().ToList();
            var datas = _productRead.GetAll(false).Skip(pagination.Size * pagination.Page).Take(pagination.Size);
            return Ok(datas);
        }

        [HttpPost("ada")]
        public async Task<IActionResult> AddProduct(VM_Create_Products model)
        {
            if (ModelState.IsValid)
            {

            }
            if (!ModelState.IsValid)
            {

            }

            // var data = await _product.AddAsync(new() { Id = Guid.Parse("4317d7c6-618a-43e5-b578-8fde369db44c"), Name = "Test" , Price = 103,Stock = 100});

            var data = await _product.AddAsync(new() { Name = model.Name, Price = model.Price, Stock = model.stock });
            //await _product.RemoveAsync("4317d7c6-618a-43e5-b578-8fde369db44c");
            await _product.SaveAsync();
            return Ok(data);
        }

        [HttpPost("action")]
        public async Task<IActionResult> Upload( IFormFile formFile)
         {

                var datas = await _storageService.UploadAsync("files", Request.Form.Files);

           // var datas = await _storageService.UploadAsync("resource/files", Request.Form.Files);
          
            await _productImageFileWriteRepository.AddRangeAsync(datas.Select(d => new ProductImageFile()
            {
                FileName = d.fileName,
                Path = d.pathOrContainerName,
                Storage = _storageService.StorageName
            }).ToList());
            await _productImageFileWriteRepository.SaveAsync();

          

            return Ok();
        }

       
    }
}
