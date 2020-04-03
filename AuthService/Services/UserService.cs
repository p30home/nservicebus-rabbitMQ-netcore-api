using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using AuthService.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using AuthService.Services;
using Shared;
using System.Threading.Tasks;

namespace WebApi.Services
{
    public interface IUserService
    {
        Task<string> Authenticate(string username, string password);
        Task<GetUserResponse> Register(string firstName, string lastName, string username, string password);
    }

    public class UserService : IUserService
    {

        // users hardcoded for simplicity, store in a db with hashed passwords in production applications


        private readonly AppSettings _appSettings;
        private readonly BusService _busService;

        public UserService(IOptions<AppSettings> appSettings, BusService busService)
        {
            _appSettings = appSettings.Value;
            _busService = busService;
        }


        public async Task<GetUserResponse> Register(string firstName, string lastName, string username, string password)
        {
            var user = await _busService.SendGetUserRequest(new Shared.GetUserRequest
            {
                Username = username
            });

            if (user.UserInfo != null)
                throw new AuthService.Exceptions.RegisterException("Provided email address is taken");

            var newUser = new UserInfo
            {
                UserId = Guid.NewGuid().ToString(),
                FirstName = firstName,
                LastName = lastName,
                Username = username,
                PasswordHash = CryptoAlgorithms.SHA256(password)
            };

            user = await _busService.SendAddUserRequest(newUser);
            return user;


        }
        public async Task<string> Authenticate(string username, string password)
        {

            var user = await _busService.SendGetUserRequest(new GetUserRequest
            {
                Username = username,
                PasswordHash = CryptoAlgorithms.SHA256(password)
            });

            // return null if user not found
            if (user == null || user.UserInfo == null)
                throw new AuthService.Exceptions.LoginException("Username or password is incorrect");

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserInfo.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

    }
}