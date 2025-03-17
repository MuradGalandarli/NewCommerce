using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using NewCommerce.Application.Abstractions.Services;
using NewCommerce.Application.Abstractions.Token;
using NewCommerce.Application.DTOs;
using NewCommerce.Application.DTOs.Facebook;
using NewCommerce.Application.Exceptions;
using NewCommerce.Application.Features.Commands.AppUser.LoginUser;
using NewCommerce.Application.Helpers;
using NewCommerce.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NewCommerce.Persistence.Services
{
    public class AuthService : IAuthService
    {
        readonly HttpClient _httpClient;
        readonly Microsoft.AspNetCore.Identity.UserManager<AppUser> _userManager;
        readonly ITokenHandler _tokenHandler;
        readonly IConfiguration _configuration;
        readonly SignInManager<AppUser> _signInManager;
        readonly IUserService _userService;
        readonly IMailService _mailService;

        public AuthService(IHttpClientFactory httpClientFactory, Microsoft.AspNetCore.Identity.UserManager<AppUser> userManager, ITokenHandler tokenHandler, IConfiguration configuration = null, SignInManager<AppUser> signInManager = null, IUserService userService = null, IMailService mailService = null)
        {
            _httpClient = httpClientFactory.CreateClient();
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _configuration = configuration;
            _signInManager = signInManager;
            _userService = userService;
            _mailService = mailService;
        }

        private async Task<Token> CreateUserExternalAsync(AppUser user, string email,string name, Microsoft.AspNetCore.Identity.UserLoginInfo info,int accessTokenLifeTime)
        {
            bool result = user != null;
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    user = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = email,
                        UserName = email,
                        NameSurname = name
                    };
                    var identityResult = await _userManager.CreateAsync(user);
                    result = identityResult.Succeeded;
                }
            }

            if (result)
                await _userManager.AddLoginAsync(user, info);
            else
                throw new Exception("Invalid external authentication.");

            Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime,user);
            
           await _userService.UpdateRefreshTokenAsync(token.RefreshToken,user,token.Expiration,10);
            return token;
        }

        public async Task<Token> FacebookLoginAsync(string IdToken , int accessTokenLifeTime) 
        {

            string accessTokenResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={_configuration["ExternalTokenSettings:Facebook:Clint_Id"]}&client_secret={_configuration["ExternalTokenSettings:Facebook:Clint_Secret"]}&grant_type=client_credentials");

            FacebookAccessTokenResponse? facebookAccessTokenResponse = JsonSerializer.Deserialize<FacebookAccessTokenResponse>(accessTokenResponse);
            string userAccessTokenValidation = await _httpClient.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={IdToken}&access_token={facebookAccessTokenResponse.AccessToken}");

            FacebookUserAccessTokenValidation? validation = JsonSerializer.Deserialize<FacebookUserAccessTokenValidation>(userAccessTokenValidation);
            if (validation?.Data.IsValid != null)
            {
                string userInfoResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/me?fields=email,name&access_token={IdToken}");

                FacebookUserInfoResponse? userInfo = JsonSerializer.Deserialize<FacebookUserInfoResponse>(userInfoResponse);

                var info = new Microsoft.AspNetCore.Identity.UserLoginInfo("FACEBOOK", validation.Data.UserId, "FACEBOOK");
                AppUser? user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

                bool result = user != null;
                if (user == null)
                {
                    return await CreateUserExternalAsync(user, userInfo.Email, userInfo.Name, info, accessTokenLifeTime);
                }

            }
            throw new Exception("Invalid external authentication.");
        }
        

        public async Task<Token> GoogleLoginAsync(string authToken, int accessTokenLifeTime)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { _configuration["Google:Client_Id"] }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(authToken, settings);

            var info = new Microsoft.AspNetCore.Identity.UserLoginInfo("Google", payload.Subject, "Google");
            AppUser? user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
           return  await CreateUserExternalAsync(user, payload.Email, payload.Name, info, accessTokenLifeTime);

        }

        public async Task<Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime)
        {
            AppUser user = await _userManager.FindByNameAsync(usernameOrEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(usernameOrEmail);

            if (user == null)
                throw new NotFoundUserException();

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user,password, false);
            if (result.Succeeded)
            {
                Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime,user);
                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 10);
                return token;
              
            }

            throw new AuthenticationErrorException();
        }

        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
          AppUser? user = _userManager.Users.FirstOrDefault(x => x.RefreshToken == refreshToken);
            if (user != null && user?.RefreshTokenEndDate < DateTime.UtcNow)
            {
                Token token = _tokenHandler.CreateAccessToken(15,user);
                await _userService.UpdateRefreshTokenAsync(refreshToken, user, token.Expiration, 300);
                await _userManager.UpdateAsync(user);
                return token;
            }
            else
                throw new AuthenticationErrorException();

        }

        public async Task PaswordResetAsync(string email)
        {
          AppUser user = _userManager.Users.FirstOrDefault(x => x.Email == email);
            if (user != null)
            {
                string refreshToken = await _userManager.GeneratePasswordResetTokenAsync(user);

                //byte[] tokenByte = Encoding.UTF8.GetBytes(refreshToken);    
                //refreshToken = WebEncoders.Base64UrlEncode(tokenByte);

                refreshToken = refreshToken.UrlEncode();
                await _mailService.SendPasswordResetMailAsync(email,user.Id,refreshToken);
            }

        }

        public async Task<bool> VerifyResetTokenAsync(string resetToken, string userId)
        {
         AppUser? user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                //byte[] byteToken = WebEncoders.Base64UrlDecode(resetToken); 
                //resetToken = Encoding.UTF8.GetString(byteToken);

                resetToken = resetToken.UrlDecode();
               
              return await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", resetToken);
            }
            return false;
        }
    }
}
