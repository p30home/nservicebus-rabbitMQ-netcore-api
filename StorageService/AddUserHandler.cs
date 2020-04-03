using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Shared;

namespace StorageService
{
    public class AddUserHandler : IHandleMessages<AddUserRequest>
    {
        static ILog log = LogManager.GetLogger<AddUserHandler>();
        public Task Handle(AddUserRequest message, IMessageHandlerContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}