using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Application.Abstractions.Services.Authentications
{
    public interface IExternalAuthentication
    {
        Task<DTOs.Token> GoogleLoginAsync(string authToken, int accessTokenLifeTime);
        Task<DTOs.Token> FacebookLoginAsync(string IdToken, int accessTokenLifeTime);
    }
}
