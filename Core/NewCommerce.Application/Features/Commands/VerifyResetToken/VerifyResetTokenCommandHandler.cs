using MediatR;
using NewCommerce.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Application.Features.Commands.VerifyResetToken
{
    public class VerifyResetTokenCommandHandler : IRequestHandler<VerifyResetTokenRequest, VerifyResetTokenReponse>
    {
        readonly IAuthService _authService;

        public VerifyResetTokenCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<VerifyResetTokenReponse> Handle(VerifyResetTokenRequest request, CancellationToken cancellationToken)
        {
          
            bool state =  await _authService.VerifyResetTokenAsync(request.ResetToken,request.UserId);
            return new()
            {
               State = state,
            };
        }
    }
}
