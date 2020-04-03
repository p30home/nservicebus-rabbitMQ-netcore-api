using NServiceBus;

namespace Shared
{
    public class GetGeoLineHistory : IMessage
    {
        public string UserId { get; set; }

        public override string ToString()
        {
            return $@"{nameof(GetGeoLineHistory)}@
            {nameof(UserId)}:{UserId}";
        }
    }
}