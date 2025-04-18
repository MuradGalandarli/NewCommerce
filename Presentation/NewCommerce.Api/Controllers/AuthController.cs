﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewCommerce.Application.Abstractions.Services;
using NewCommerce.Application.Features.Commands.AppUser.FacebookLogin;
using NewCommerce.Application.Features.Commands.AppUser.GoogleLogin;
using NewCommerce.Application.Features.Commands.AppUser.LoginUser;
using NewCommerce.Application.Features.Commands.AppUser.PasswordReset;
using NewCommerce.Application.Features.Commands.AppUser.RefreshTokenLogin;
using NewCommerce.Application.Features.Commands.VerifyResetToken;

namespace NewCommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        readonly IMailService _mailService;
        public AuthController(IMediator mediator, IMailService mailService)
        {
            _mediator = mediator;
            _mailService = mailService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
        {
            LoginUserCommandResponse response = await _mediator.Send(loginUserCommandRequest);
            return Ok(response);

        }

        [HttpGet("[action]")]
        public async Task<IActionResult> RefreshTokenLogin([FromQuery] RefreshTokenCommendResquest refreshTokenCommendResquest)
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
        [HttpGet]
        public async Task<IActionResult> ExampleMailTest()
        {
            await _mailService.SendMailAsync("mqelenderli25@gmail.com", "Test Mail", "<strong>Bu bir örnek maildir.</strong>");
            return Ok();
        }
        [HttpPost("password-reset")]
        public async Task<IActionResult> PasswordReset([FromBody] PasswordResetCommandRequest passwordResetCommandRequest)
        {
            await _mediator.Send(passwordResetCommandRequest);
            return Ok();
        }
        [HttpPost("verify-reset-token")]
        public async Task<IActionResult> VerifyResetToken([FromBody] VerifyResetTokenRequest verifyResetTokenRequest)
        {
            VerifyResetTokenReponse verifyResetTokenReponse = await _mediator.Send(verifyResetTokenRequest);
            return Ok(verifyResetTokenReponse);
        }


    }
}
