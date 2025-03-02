using MediatR;
using Microsoft.AspNetCore.Identity;
using NewCommerce.Application.Abstractions.Token;



using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewCommerce.Application.DTOs.Facebook;
using NewCommerce.Application.DTOs;
using System.Text.Json;
using NewCommerce.Application.Abstractions.Services;

namespace NewCommerce.Application.Features.Commands.AppUser.FacebookLogin
{
    public class FacebookLoginCommandHandler : IRequestHandler<FacebookLoginCommandRequest, FacebookLoginCommandResponse>
    {
        readonly IAuthService _authService;

        public FacebookLoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<FacebookLoginCommandResponse> Handle(FacebookLoginCommandRequest request, CancellationToken cancellationToken)
        {
           Token token = await _authService.FacebookLoginAsync(request.AuthToken,5);
            return new()
            {
                Token = token
            };
        }
    }
}