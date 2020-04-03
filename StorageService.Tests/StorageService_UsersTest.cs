using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StorageService.Models;
using Xunit;

namespace StorageService.Tests
{
    public class StorageService_UsersTest
    {
        private AppDbContext _dbContext;
        private DataContext _dataContext;

        public StorageService_UsersTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseInMemoryDatabase("in-memory-test");
            _dbContext = new AppDbContext(optionsBuilder.Options);
            _dataContext = new DataContext(_dbContext);
        }

        [Theory]
        [InlineData("5464sdfssd-sdf/4sdfsdf-sdf", "p30home@gmail.com", "asdfhaksfdhasfklasdfasfd", "Mojtaba", "Goodarzi")]
        [InlineData("sdfsdf24234sdf", "test@gmail.com", "asdfasdferwt3", "test", "test")]
        public void StorageService_AddNewUser(string userId, string username, string password, string firstName, string lastName)
        {
            _dataContext.AddUser(new Shared.UserInfo
            {
                FirstName = firstName,
                LastName = lastName,
                PasswordHash = password,
                UserId = userId,
                Username = username
            });

            Assert.True(
                _dbContext.Users.Any(u => u.Id == userId && u.Username == username && u.PasswordHash == password && u.FirstName == firstName &&
                    u.LastName == lastName)
            );
        }

        [Theory]
        [InlineData("", "p30home@gmail.com", "asdlfjkasdfjasdf", "mojtaba", "goodarzi")] //missing id
        [InlineData(null, "p30home@gmail.com", "asdlfjkasdfjasdf", "mojtaba", "goodarzi")] //missing id
        [InlineData("asdfakhf", "", "asfdkhaslfdk", "mojtaba", "goodarzi")] //missing username
        [InlineData("asdfakhf", null, "asfdkhaslfdk", "mojtaba", "goodarzi")] //missing username
        [InlineData("asajfsdl", "p30home@gmail.com", null, "mojtaba", "goodariz")] // missing password
        [InlineData("asajfsdl", "p30home@gmail.com", "", "mojtaba", "goodariz")] // missing password
        public void StorageService_AddNewUser_MissingRequireds(string userId, string username, string password, string firstName, string lastName)
        {
            Assert.ThrowsAny<Exception>(() => _dataContext.AddUser(new Shared.UserInfo
            {
                FirstName = firstName,
                LastName = lastName,
                PasswordHash = password,
                UserId = userId,
                Username = username
            }));
        }

        [Theory]
        [InlineData()]
        public void StorageService_AddNewUser_DuplicateIdCheck()
        {
            _dataContext.AddUser(new Shared.UserInfo
            {
                FirstName = "firstName",
                LastName = "lastName",
                PasswordHash = "password",
                UserId = "userId",
                Username = "username0"
            });


            Assert.ThrowsAny<Exception>(() => _dataContext.AddUser(new Shared.UserInfo //duplicate userId
            {
                FirstName = "firstName",
                LastName = "lastName",
                PasswordHash = "password",
                UserId = "userId",
                Username = "username"
            }));
        }

        [Theory]
        [InlineData()]
        public void StorageService_AddNewUser_DuplicateUsernameCheck()
        {
            _dataContext.AddUser(new Shared.UserInfo
            {
                FirstName = "firstName",
                LastName = "lastName",
                PasswordHash = "password",
                UserId = "userId0",
                Username = "username"
            });


            Assert.ThrowsAny<Exception>(() => _dataContext.AddUser(new Shared.UserInfo //duplicate userId
            {
                FirstName = "firstName",
                LastName = "lastName",
                PasswordHash = "password",
                UserId = "userId",
                Username = "username"
            }));
        }
    }
}