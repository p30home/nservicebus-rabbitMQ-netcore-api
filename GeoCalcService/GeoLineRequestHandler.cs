using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Shared;

namespace GeoCalcService
{
    public class GeoLineRequestHandler : IHandleMessages<GeoLineRequest>
    {
        static ILog log = LogManager.GetLogger<GeoLineRequestHandler>();
        public Task Handle(GeoLineRequest message, IMessageHandlerContext context)
        {
            log.Info($"New GeoLine request: {message}");
            Task reply;
            var distance = GeoDistanceCalculator.Calculate(message);
            reply = context.Reply(new GeoLineResponse(message,distance));
            return reply;
        }
    }
}