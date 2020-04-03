using System;

namespace Shared
{
    public class GetUserResponse : AddUserRequest
    {
        public DateTime CreationDate { get; set; }
        public GetUserResponse()
        {

        }
        public GetUserResponse(AddUserRequest addUserRequest)
        {
            this.FirstName = addUserRequest.FirstName;
            this.LastName = addUserRequest.LastName;
            this.Username = addUserRequest.Username;
            this.PasswordHash = addUserRequest.PasswordHash;
            this.CreationDate = DateTime.UtcNow;

        }
    }
}