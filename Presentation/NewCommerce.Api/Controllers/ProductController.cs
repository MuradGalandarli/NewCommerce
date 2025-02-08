using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NewCommerce.Application.Repositoryes;
using NewCommerce.Application.RequestParameters;
using NewCommerce.Application.Services;
using NewCommerce.Application.ViewModels.Products;

namespace NewCommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductWriteRepository _product;
        private readonly IProductReadRepository _productRead;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileService _fileService;
        public ProductController(IProductWriteRepository product,
            IProductReadRepository productRead,
            IWebHostEnvironment webHostEnvironment,
             IFileService fileService)
        {
            _product = product;
            _productRead = productRead;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
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
            await _fileService.UploadAsync("resource/product-images", Request.Form.Files);

            return Ok();
        }



    }
}
