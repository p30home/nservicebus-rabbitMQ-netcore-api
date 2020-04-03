using NServiceBus;

namespace Shared
{
    public class UserInfo : IMessage
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public override string ToString()
        {
            return $@"UserInfo :
            {nameof(UserId)}:{UserId}
            {nameof(FirstName)}:{FirstName}
            {nameof(LastName)}:{LastName}
            {nameof(Username)}:{Username}
            {nameof(PasswordHash)}:{PasswordHash}";
        }
    }
}