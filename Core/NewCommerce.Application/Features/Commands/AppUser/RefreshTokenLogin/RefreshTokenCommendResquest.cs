using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Application.Features.Commands.AppUser.RefreshTokenLogin
{
    public class RefreshTokenCommendResquest:IRequest<RefreshTokenCommendResponse>
    {
       public string RefreshToken { get; set; }
    }
}
