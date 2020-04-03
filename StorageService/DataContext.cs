using System;
using System.Collections.Generic;
using System.Linq;
using Shared;
using StorageService.Models;

namespace StorageService
{
    public class DataContext
    {
        private AppDbContext _appDbContext;

        public DataContext(AppDbContext dbContext)
        {
            _appDbContext = dbContext;
        }

        public void AddGeuLineHistory(GeoLineResult message)
        {
            _appDbContext.ResultHistories.Add(new ResultHistory
            {
                UserId = message.UserId,
                DistanceResult = message.Distance,
                FromLat = message.FromLat,
                FromLong = message.FromLong,
                ToLat = message.ToLat,
                ToLong = message.ToLong
            });
            _appDbContext.SaveChanges();
        }

        public void AddUser(UserInfo message)
        {
            if(string.IsNullOrWhiteSpace(message.UserId))
                throw new ArgumentNullException(nameof(message.UserId));
            var user = new User
            {
                FirstName = message.FirstName,
                LastName = message.LastName,
                Id = message.UserId,
                PasswordHash = message.PasswordHash,
                Username = message.Username
            };
            _appDbContext.Users.Add(user);

            _appDbContext.SaveChanges();
        }

        internal List<GeoLineResult> GetUserGeoHistories(string userId)
        {
            var result = _appDbContext.ResultHistories.Where(c => c.UserId == userId).Select(c => new GeoLineResult
            {
                Distance = c.DistanceResult,
                FromLat = c.FromLat,
                FromLong = c.FromLong,
                ToLong = c.ToLong,
                ToLat = c.ToLat,
                UserId = c.UserId
            }).ToList();

            return result;
        }

        public GetUserResponse GetUser(string username, string passwordHash)
        {
            var user = _appDbContext.Users.FirstOrDefault(u => (username == null || u.Username == username) &&
                            (passwordHash == null || u.PasswordHash == passwordHash));
            var response = new GetUserResponse
            {
                UserInfo = user == null ? null : new UserInfo
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    PasswordHash = user.PasswordHash,
                    UserId = user.Id
                }
            };

            return response;

        }
    }
}