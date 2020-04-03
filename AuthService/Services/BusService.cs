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
        public async Task<GeoLineResponse> SendGeoLineRequest(GeoLineRequest geoLineRequest)
        {
            var sendOptions = new SendOptions();
            sendOptions.SetDestination("GeoAPI.GeoCalcServer");
            var geoLineResponse = await _messageSession.Request<GeoLineResponse>(geoLineRequest, sendOptions);
            return geoLineResponse;
        }

        public async Task<GetUserResponse> SendAddUserRequest(AddUserRequest addUserRequest)
        {
            var sendOptions = new SendOptions();
            sendOptions.SetDestination("GeoAPI.StorageService");
            return await _messageSession.Request<GetUserResponse>(addUserRequest, sendOptions);
        }

        public async Task<GetUserResponse> SendGetUserRequest(GetUserRequest getUserRequest)
        {
            var sendOptions = new SendOptions();
            sendOptions.SetDestination("GeoAPI.StorageService");
            return await _messageSession.Request<GetUserResponse>(getUserRequest,sendOptions);
        }
    }
}