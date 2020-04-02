using System;
using System.Collections.Generic;
using System.Linq;
using Models;

namespace AuthService.Services
{
    //this class will be used singletone so, it must be threadsafe throughout the project
    public class UsersRepository
    {
        private object _Lock = new object();
        private List<User> _Users = new List<User>(){
            new User{
                FirstName = "test",
                LastName = "test",
                Username = "test@gmail.com",
                PasswordHash = CryptoAlgorithms.SHA256("test")
            }
        };

        public User Add(User user)
        {
            lock (_Lock)
            {
                _Users.Add(user);
            }

            return user;
        }

        public User Find(Func<User, bool> predicate)
        {
            lock (_Lock)
            {
                return _Users.Where(a => predicate(a)).FirstOrDefault();
            }
        }

        public ICollection<User> All()
        {
            lock (_Lock)
            {
                return this._Users.ToList();
            }
        }
    }
}