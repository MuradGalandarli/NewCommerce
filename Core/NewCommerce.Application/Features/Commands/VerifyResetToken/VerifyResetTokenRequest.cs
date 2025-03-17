using MediatR;

namespace NewCommerce.Application.Features.Commands.VerifyResetToken
{
    public class VerifyResetTokenRequest:IRequest<VerifyResetTokenReponse>
    {
        public string ResetToken { get; set; }
        public string UserId { get; set; }
    }
}