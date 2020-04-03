using System.Linq;
using System.Threading.Tasks;
using NServiceBus;
using Shared;
using StorageService.Models;

namespace StorageService
{
    public class GetGeoLineHostoriesHandler : IHandleMessages<GetGeoLineHistory>
    {
        private AppDbContext _appDbContext;

        public GetGeoLineHostoriesHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public Task Handle(GetGeoLineHistory message, IMessageHandlerContext context)
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
    }
}