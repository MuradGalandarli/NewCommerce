using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewCommerce.Application.Abstractions.Configurations;
using NewCommerce.Application.CustomAttributes;
using NewCommerce.Application.Enums;

namespace NewCommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Admin")]
    [ApiController]
    public class ApplicatonServicesController : ControllerBase
    {
        IApplicationService _applicationService;
        

        public ApplicatonServicesController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Authorize Definition Endpoints", Menu = "Application Services")]
        [HttpGet("GetAuthorizeDefinitionEndpoints")]
        public async Task<IActionResult> GetAuthorizeDefinitionEndponts()
        {
            var data = _applicationService.GetAuthorizeDefinitionEndpoints(typeof(Program));
            return Ok(data);
        }
    }
}
