using System.Threading.Tasks;
using NServiceBus;
using Shared;

namespace AuthService.Services
{
    public class BusService
    {
        private IMessageSession _messageSession;

        public BusService(IMessageSession messageSession)
        {
            _messageSession = messageSession;
        }
        public async Task<GeoLineResult> SendGeoLineRequest(GeoLineRequest geoLineRequest)
        {
            var sendOptions = new SendOptions();
            sendOptions.SetDestination("GeoAPI.GeoCalcServer");
            var geoLineResponse = await _messageSession.Request<GeoLineResult>(geoLineRequest, sendOptions);
            return geoLineResponse;
        }

        public async Task SaveGeoLineResult(GeoLineResult geoLineResult)
        {
            var sendOptions = new SendOptions();
            sendOptions.SetDestination("GeoAPI.StorageService");
            await _messageSession.Send(geoLineResult, sendOptions);
        }

        public async Task<GeoLineHistories> GetGeoLineHistories(GetGeoLineHistory geoLineHistory)
        {
            var sendOptions = new SendOptions();
            sendOptions.SetDestination("GeoAPI.StorageService");
            var geoHistory = await _messageSession.Request<GeoLineHistories>(geoLineHistory, sendOptions);
            return geoHistory;
        }

        public async Task<GetUserResponse> SendAddUserRequest(UserInfo addUserRequest)
        {
            var sendOptions = new SendOptions();
            sendOptions.SetDestination("GeoAPI.StorageService");
            return await _messageSession.Request<GetUserResponse>(addUserRequest, sendOptions);
        }

        public async Task<GetUserResponse> SendGetUserRequest(GetUserRequest getUserRequest)
        {
            var sendOptions = new SendOptions();
            sendOptions.SetDestination("GeoAPI.StorageService");
            return await _messageSession.Request<GetUserResponse>(getUserRequest, sendOptions);
        }
    }
}