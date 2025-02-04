using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewCommerce.Application.Repositoryes;

namespace NewCommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductWriteRepository _product;
        public ProductController(IProductWriteRepository product)
        {
            _product = product;
        }

        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
           var data =  _product.Update(new() { Id = Guid.Parse("4317d7c6-618a-43e5-b578-8fde369db44c"), Name = "Test" , Price = 103,Stock = 100});
           await _product.RemoveAsync("4317d7c6-618a-43e5-b578-8fde369db44c");
           await _product.SaveAsync();
            return Ok(data);
        }
    }
}
