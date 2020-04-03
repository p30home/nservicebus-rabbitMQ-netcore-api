using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Shared;
using StorageService.Models;

namespace StorageService
{
    public class AddGeoLineHistoryHandler : IHandleMessages<GeoLineResult>
    {
        private static ILog log = LogManager.GetLogger<AddGeoLineHistoryHandler>();
        private DataContext _dataContext;

        public AddGeoLineHistoryHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public Task Handle(GeoLineResult message, IMessageHandlerContext context)
        {
            log.Info($"new AddGeoHistoryRequest : {message}");
            try
            {
                _dataContext.AddGeuLineHistory(message);
            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message,ex);
                throw;
            }
            return Task.CompletedTask;
        }
    }
}