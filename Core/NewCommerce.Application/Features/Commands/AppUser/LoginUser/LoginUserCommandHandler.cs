using MediatR;
using Microsoft.AspNetCore.Identity;
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
        
            readonly UserManager<Domain.Identity.AppUser> _userManager;
            readonly SignInManager<Domain.Identity.AppUser> _signInManager;

        public LoginUserCommandHandler(UserManager<Domain.Identity.AppUser> userManager, SignInManager<Domain.Identity.AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Identity.AppUser user = await _userManager.FindByNameAsync(request.UsernameOrEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(request.UsernameOrEmail);

            if (user == null)
                throw new NotFoundUserException("Kullanıcı veya şifre hatalı...");

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (result.Succeeded) //Authentication başarılı!
            {
                //.... Yetkileri belirlememiz gerekiyor!
            }

            return new();

        }
    }
}
