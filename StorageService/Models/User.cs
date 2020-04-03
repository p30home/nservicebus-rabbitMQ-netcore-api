using System;
using System.ComponentModel.DataAnnotations;

namespace StorageService.Models
{
    public class User
    {
        [Key]
        [StringLength(128)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
