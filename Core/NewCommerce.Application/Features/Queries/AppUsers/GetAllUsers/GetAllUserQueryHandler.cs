using MediatR;
using NewCommerce.Application.Abstractions.Services;
using NewCommerce.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Application.Features.Queries.AppUsers.GetAllUsers
{
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQueryRequest, GetAllUserQueryResponse>
    {
        readonly IUserService _userService;

        public GetAllUserQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<GetAllUserQueryResponse> Handle(GetAllUserQueryRequest request, CancellationToken cancellationToken)
        {
            List<ListUser> users = await _userService.GetAllUsersAsync(request.page, request.size);
            int count = _userService.TotalUsersCount;
            return new()
            {
                Users = users,
                Count = count

            };

        }
    }
}
