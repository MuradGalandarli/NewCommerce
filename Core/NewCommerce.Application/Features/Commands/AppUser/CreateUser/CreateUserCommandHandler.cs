using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        readonly Microsoft.AspNetCore.Identity.UserManager<Domain.Identity.AppUser> _userManager;
        public CreateUserCommandHandler(Microsoft.AspNetCore.Identity.UserManager<Domain.Identity.AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            Microsoft.AspNetCore.Identity.IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = request.Username,
                Email = request.Email,
                NameSurname = request.NameSurname,
            }, request.Password);

            CreateUserCommandResponse response = new() { Succeeded = result.Succeeded };

            if (result.Succeeded)
                response.Message = "Kullanıcı başarıyla oluşturulmuştur.";
            else
                foreach (var error in result.Errors)
                    response.Message += $"{error.Code} - {error.Description}\n";

            return response;
        }
    }


}
