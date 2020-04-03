using System;
using NServiceBus;

namespace Shared
{
    public class GetUserResponse : IMessage
    {

        public UserInfo UserInfo { get; set; }
        public GetUserResponse()
        {

        }
        public GetUserResponse(UserInfo userInfo)
        {
            this.UserInfo = userInfo;

        }
    }
}