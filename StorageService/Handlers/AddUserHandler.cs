using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Shared;
using StorageService.Models;

namespace StorageService
{
    public class AddUserHandler : IHandleMessages<UserInfo>
    {
        public AddUserHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        static ILog log = LogManager.GetLogger<AddUserHandler>();

        private DataContext _dataContext;

        public Task Handle(UserInfo message, IMessageHandlerContext context)
        {
            log.Info($"new AddUserRequest : {message}");
            try
            {
                _dataContext.AddUser(message);
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