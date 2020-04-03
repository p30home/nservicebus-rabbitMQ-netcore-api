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
        private AppDbContext _appDbContext;

        public GetGeoLineHostoriesHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public Task Handle(GetGeoLineHistory message, IMessageHandlerContext context)
        {
            log.Info($"new GetGeoHistoryRequest : {message}");
            try
            {
                var histories = _appDbContext.ResultHistories.Where(c => c.UserId == message.UserId).Select(c => new GeoLineResult
                {
                    Distance = c.DistanceResult,
                    FromLat = c.FromLat,
                    FromLong = c.FromLong,
                    ToLong = c.ToLong,
                    ToLat = c.ToLat,
                    UserId = c.UserId
                }).ToList();

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