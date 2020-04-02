using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using AuthService.Helpers;
using AuthService.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using AuthService.Models;

namespace WebApi.Services
{
    public interface IUserService
    {
        string Authenticate(string username, string password);
        User Register(string firstName, string lastName, string username, string password);
        IEnumerable<User> GetAll();
    }

    public class UserService : IUserService
    {

        // users hardcoded for simplicity, store in a db with hashed passwords in production applications

        
        private readonly AppSettings _appSettings;
        private readonly AppDbContext _dbContext;

        public UserService(IOptions<AppSettings> appSettings, AppDbContext dbContext)
        {
            _appSettings = appSettings.Value;
            _dbContext = dbContext;
        }


        public User Register(string firstName, string lastName, string username, string password)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Username == username);
            if (user != null)
                throw new Exception("Provided email address is taken");

            user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Username = username,
                PasswordHash = CryptoAlgorithms.SHA256(password)
            };

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return user;


        }
        public string Authenticate(string username, string password)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Username == username && x.PasswordHash == CryptoAlgorithms.SHA256(password));

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public IEnumerable<User> GetAll()
        {
            return _dbContext.Users.ToList();
        }

    }
}