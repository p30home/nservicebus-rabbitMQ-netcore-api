using System.Linq;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Shared;
using StorageService.Models;

namespace StorageService
{
    public class GetUserHandler : IHandleMessages<GetUserRequest>
    {
        public GetUserHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        private static ILog log = LogManager.GetLogger<GetUserHandler>();
        private AppDbContext _appDbContext;

        public Task Handle(GetUserRequest message, IMessageHandlerContext context)
        {
            log.Info($"new GetUserRuquest : {message}");
            try
            {
                var user = _appDbContext.Users.FirstOrDefault(u => (message.Username == null || u.Username == message.Username) &&
                (message.PasswordHash == null || u.PasswordHash == message.PasswordHash));

                return context.Reply(new GetUserResponse
                {
                    UserInfo = user == null ? null : new UserInfo
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Username = user.Username,
                        PasswordHash = user.PasswordHash,
                        UserId = user.Id
                    }
                });
            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message, ex);
                throw;
            }

        }
    }
}