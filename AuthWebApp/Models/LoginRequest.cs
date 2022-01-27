using System.ComponentModel.DataAnnotations;

namespace AuthWebApp.Models
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
