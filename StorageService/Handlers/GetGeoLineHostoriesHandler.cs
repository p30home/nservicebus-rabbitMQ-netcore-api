using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Shared;
using StorageService.Models;

namespace StorageService
{
    public class GetGeoLineHostoriesHandler : IHandleMessages<GetGeoLineHistory>
    {
        private static ILog log = LogManager.GetLogger<GetGeoLineHostoriesHandler>();

        private DataContext _dataContext;

        public GetGeoLineHostoriesHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public Task Handle(GetGeoLineHistory message, IMessageHandlerContext context)
        {
            log.Info($"new GetGeoHistoryRequest : {message}");
            try
            {
                List<GeoLineResult> histories = _dataContext.GetUserGeoHistories(message.UserId);

                return context.Reply(new GeoLineHistories { GeoLines = histories });
            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message, ex);
                throw;
            }
        }
    }
}