﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NewCommerce.Application.Abstractions.Token;
using NewCommerce.Application.DTOs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Infrastructure.Services
{
    public class TokenHandler: ITokenHandler
    {
        readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Token CreateAccessToken(int secund)
        {
            Token token = new();

          
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

           
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

           
            token.Expiration = DateTime.UtcNow.AddSeconds(secund);
            JwtSecurityToken securityToken = new(
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"],
                expires: token.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials
                );

            JwtSecurityTokenHandler tokenHandler = new();
            token.AccessToken = tokenHandler.WriteToken(securityToken);

            token.RefreshToken = CreateRefreshToken();
          
            return token;
        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
           using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}

