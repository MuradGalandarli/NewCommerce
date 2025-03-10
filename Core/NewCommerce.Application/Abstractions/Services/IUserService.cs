﻿using NewCommerce.Application.DTOs.User;
using NewCommerce.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateAsync(CreateUser model);
        Task UpdateRefreshTOken(string refreshToken,AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate);
    }
}
