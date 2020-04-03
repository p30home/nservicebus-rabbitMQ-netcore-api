using NServiceBus;

namespace Shared
{
    public class GetUserRequest : IMessage
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}