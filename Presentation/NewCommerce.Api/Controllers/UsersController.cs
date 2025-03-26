using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewCommerce.Application.Consts;
using NewCommerce.Application.CustomAttributes;
using NewCommerce.Application.DTOs.Configuration;
using NewCommerce.Application.Enums;
using NewCommerce.Application.Features.Commands.AppUser.AssignRoleToUser;
using NewCommerce.Application.Features.Commands.AppUser.CreateUser;
using NewCommerce.Application.Features.Commands.AppUser.FacebookLogin;
using NewCommerce.Application.Features.Commands.AppUser.GoogleLogin;
using NewCommerce.Application.Features.Commands.AppUser.LoginUser;
using NewCommerce.Application.Features.Commands.AppUser.UpdatePassword;
using NewCommerce.Application.Features.Queries.AppUsers.GetAllUsers;
using NewCommerce.Application.Features.Queries.AppUsers.GetRolesToUser;

namespace NewCommerce.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
        {
            CreateUserCommandResponse response = await _mediator.Send(createUserCommandRequest);
            return Ok(response);
        }
        [HttpPost("update-password-command")]
        public async Task<IActionResult> UpdatePasswordCommand(UpdatePasswordCommandRequest updatePasswordCommandRequest)
        {
            UpdatePasswordCommandResponse response = await _mediator.Send(updatePasswordCommandRequest);
            return Ok(response);
        }

        [HttpGet]
        /*  [Authorize(AuthenticationSchemes = "Admin")]
          [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get All Users", Menu = "Users")]*/
        public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUserQueryRequest getAllUsersQueryRequest)
        {
            GetAllUserQueryResponse response = await _mediator.Send(getAllUsersQueryRequest);
            return Ok(response);
        }

       [HttpGet("get-roles-to-user/{UserId}")]
        /*   [Authorize(AuthenticationSchemes = "Admin")]*/
        //[AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Roles To Users", Menu = "Users")]
        public async Task<IActionResult> GetRolesToUser([FromRoute] GetRolesToUserQueryRequest getRolesToUserQueryRequest)
        {
            GetRolesToUserQueryResponse response = await _mediator.Send(getRolesToUserQueryRequest);
            return Ok(response);
        }

        [HttpPost("assign-role-to-user")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Assign Role To User", Menu = "Users")]
        public async Task<IActionResult> AssignRoleToUser(AssignRoleToUserCommandRequest assignRoleToUserCommandRequest)
        {
            AssignRoleToUserCommandResponse response = await _mediator.Send(assignRoleToUserCommandRequest);
            return Ok(response);
        }

    }
}
