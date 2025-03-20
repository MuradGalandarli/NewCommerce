using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewCommerce.Application.Abstractions.Configurations;

namespace NewCommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicatonServicesController : ControllerBase
    {
        IApplicationService _applicationService;
        

        public ApplicatonServicesController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet("GetAuthorizeDefinitionEndpoints")]
        public async Task<IActionResult> GetAuthorizeDefinitionEndponts()
        {
            var data = _applicationService.GetAuthorizeDefinitionEndpoints(typeof(Program));
            return Ok(data);
        }
    }
}
