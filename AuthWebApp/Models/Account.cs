using System;
using System.ComponentModel.DataAnnotations;

namespace AuthWebApp.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }

        [EmailAddress]
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        public UserRoles UserRole { get; set; }
    }

    public enum UserRoles { Guest, Admin }
}

