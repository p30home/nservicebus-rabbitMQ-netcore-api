using System;

namespace Shared
{
    public class AddUserResponse : AddUserRequest
    {
        public DateTime CreationDate { get; set; }
        public AddUserResponse()
        {

        }
        public AddUserResponse(AddUserRequest addUserRequest)
        {
            this.FirstName = addUserRequest.FirstName;
            this.LastName = addUserRequest.LastName;
            this.Username = addUserRequest.Username;
            this.PasswordHash = addUserRequest.PasswordHash;
            this.CreationDate = DateTime.UtcNow;

        }
    }
}