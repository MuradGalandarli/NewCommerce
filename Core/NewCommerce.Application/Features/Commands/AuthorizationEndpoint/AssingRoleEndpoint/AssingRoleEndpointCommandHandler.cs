using MediatR;
using Microsoft.AspNetCore.Authorization;
using NewCommerce.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Application.Features.Commands.AuthorizationEndpoint.AssingRoleEndpoint
{
    public class AssingRoleEndpointCommandHandler : IRequestHandler<AssignRoleEndpointCommandRequest, AssignRoleEndpointCommandResponse>
    {
        readonly IAuthorizationEndpointService _authorizationEndpointService;

        public AssingRoleEndpointCommandHandler(IAuthorizationEndpointService authorizationEndpointService)
        {
            _authorizationEndpointService = authorizationEndpointService;
        }

        public async Task<AssignRoleEndpointCommandResponse> Handle(AssignRoleEndpointCommandRequest request, CancellationToken cancellationToken)
        {
          await  _authorizationEndpointService.AssingRoleEndpointAsync(request.Roles, request.Menu,request.Code, request.Type);
            return new()
            {

            };
        }
    }
}
