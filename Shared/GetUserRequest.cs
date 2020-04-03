using NServiceBus;

namespace Shared
{
    public class GetUserRequest : IMessage
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }

        public override string ToString(){
            return $@"{nameof(GetUserRequest)}@
            {nameof(Username)}:{Username}
            {nameof(PasswordHash)}:{PasswordHash}";
        }
    }
}