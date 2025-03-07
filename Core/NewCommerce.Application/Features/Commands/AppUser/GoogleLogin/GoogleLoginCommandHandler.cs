using MediatR;
using Microsoft.AspNetCore.Identity;
using NewCommerce.Application.Abstractions.Services.Authentications;
using NewCommerce.Application.Abstractions.Token;
using NewCommerce.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Application.Features.Commands.AppUser.GoogleLogin
{
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
    {
        readonly IExternalAuthentication _externalAuthentication;

        public GoogleLoginCommandHandler(IExternalAuthentication externalAuthentication)
        {
            _externalAuthentication = externalAuthentication;
        }

        public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
        {
            Token token = await _externalAuthentication.GoogleLoginAsync(request.IdToken, 900);
            return new()
            {
                Token = token
            };

        }
    }
}
