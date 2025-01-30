using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NewCommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IWebHostEnvironment env;
    }
}
