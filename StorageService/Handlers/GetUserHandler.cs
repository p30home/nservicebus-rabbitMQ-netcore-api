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
        public GetUserHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        private static ILog log = LogManager.GetLogger<GetUserHandler>();
        private DataContext _dataContext;

        public Task Handle(GetUserRequest message, IMessageHandlerContext context)
        {
            log.Info($"new GetUserRuquest : {message}");
            try
            {
                return context.Reply(_dataContext.GetUser(message.Username, message.PasswordHash));
            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message, ex);
                throw;
            }

        }
    }
}