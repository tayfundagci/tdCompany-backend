using MovieApp.Entities;
using System.ComponentModel.DataAnnotations;

namespace MovieApp.Message.Auth
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }

   
}
