﻿using System;
using System.ComponentModel.DataAnnotations;

namespace StorageService.Models
{
    public class User
    {
        [Key]
        [StringLength(128)]
        [Required(AllowEmptyStrings = false)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Username { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string PasswordHash { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
