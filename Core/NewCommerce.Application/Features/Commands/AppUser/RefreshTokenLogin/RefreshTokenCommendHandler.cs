using MediatR;
using NewCommerce.Application.Abstractions.Services;
using NewCommerce.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Application.Features.Commands.AppUser.RefreshTokenLogin
{
    public class RefreshTokenCommendHandler : IRequestHandler<RefreshTokenCommendResquest, RefreshTokenCommendResponse>
    {
        readonly IAuthService _authService;

        public RefreshTokenCommendHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<RefreshTokenCommendResponse> Handle(RefreshTokenCommendResquest request, CancellationToken cancellationToken)
        {
           Token token = await _authService.RefreshTokenLoginAsync(request.RefreshToken);
            return new()
            {
                Token = token
            
            };

        }
    }
}
