using NServiceBus;

namespace Shared
{
    public class AddUserRequest : IMessage
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}