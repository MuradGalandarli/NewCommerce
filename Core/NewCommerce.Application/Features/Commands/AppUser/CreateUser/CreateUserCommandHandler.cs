using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using NewCommerce.Application.Abstractions.Services;
using NewCommerce.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        readonly IUserService _userService;

        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
  CreateUserResponse createUserResponse = await _userService.CreateAsync(
                new()
                {
                    Email = request.Email,
                    NameSurname = request.NameSurname,
                    Password = request.Password,
                    PasswordConfirm = request.PasswordConfirm,
                    Username = request.Username,    
                    
                });

            return new()
            {
                Message = createUserResponse.Message,
                Succeeded = createUserResponse.Succeeded,

            };

        }
    }


}
