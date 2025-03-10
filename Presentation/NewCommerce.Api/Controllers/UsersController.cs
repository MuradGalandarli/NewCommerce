﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewCommerce.Application.Features.Commands.AppUser.CreateUser;
using NewCommerce.Application.Features.Commands.AppUser.FacebookLogin;
using NewCommerce.Application.Features.Commands.AppUser.GoogleLogin;
using NewCommerce.Application.Features.Commands.AppUser.LoginUser;

namespace NewCommerce.Api.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
        {
            CreateUserCommandResponse response = await _mediator.Send(createUserCommandRequest);
            return Ok(response);
        }

    }
}
