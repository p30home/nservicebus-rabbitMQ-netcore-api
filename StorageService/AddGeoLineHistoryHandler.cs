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
        private AppDbContext _appDbContext;

        public AddGeoLineHistoryHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public Task Handle(GeoLineResult message, IMessageHandlerContext context)
        {
            log.Info($"new AddGeoHistoryRequest : {message}");
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
            return Task.CompletedTask;
        }
    }
}