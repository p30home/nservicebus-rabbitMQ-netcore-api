using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Shared;
using StorageService.Models;

namespace StorageService
{
    public class AddUserHandler : IHandleMessages<UserInfo>
    {
        public AddUserHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        static ILog log = LogManager.GetLogger<AddUserHandler>();
        private AppDbContext _appDbContext;

        public Task Handle(UserInfo message, IMessageHandlerContext context)
        {
            log.Info($"new AddUserRequest : {message}");
            try
            {
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
            catch (System.Exception ex)
            {
                log.Error(ex.Message, ex);
                throw;
            }

            return context.Reply(new GetUserResponse(message));
        }
    }
}