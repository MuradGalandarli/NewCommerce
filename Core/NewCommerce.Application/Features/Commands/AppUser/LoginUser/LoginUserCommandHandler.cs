using MediatR;
using Microsoft.AspNetCore.Identity;
using NewCommerce.Application.Abstractions.Services;
using NewCommerce.Application.Abstractions.Token;
using NewCommerce.Application.DTOs;
using NewCommerce.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly IAuthService _authService;

        public LoginUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            Token token = await _authService.LoginAsync(request.UsernameOrEmail, request.Password,15);
            return new LoginUserSuccessCommandResponse()
            {
                Token = token
            };
        }
    }
}
