using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewCommerce.Application.Features.Commands.AppUser.FacebookLogin;
using NewCommerce.Application.Features.Commands.AppUser.GoogleLogin;
using NewCommerce.Application.Features.Commands.AppUser.LoginUser;
using NewCommerce.Application.Features.Commands.AppUser.RefreshTokenLogin;

namespace NewCommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
        {
            LoginUserCommandResponse response = await _mediator.Send(loginUserCommandRequest);
            return Ok(response);

        }

        [HttpGet("[action]")]
        public async Task<IActionResult> RefreshTokenLogin([FromQuery]RefreshTokenCommendResquest refreshTokenCommendResquest)
        {
            RefreshTokenCommendResponse response = await _mediator.Send(refreshTokenCommendResquest);
            return Ok(response);

        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginCommandRequest googleLoginCommandRequest)
        {
            GoogleLoginCommandResponse response = await _mediator.Send(googleLoginCommandRequest);
            return Ok(response);
        }
        [HttpPost("facebook-login")]
        public async Task<IActionResult> FacebookLogin(FacebookLoginCommandRequest facebookLoginCommandRequest)
        {
            FacebookLoginCommandResponse response = await _mediator.Send(facebookLoginCommandRequest);
            return Ok(response);
        }
    }
}
