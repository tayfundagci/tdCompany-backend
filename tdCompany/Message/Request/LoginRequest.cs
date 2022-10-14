using MovieApp.Entities;
using System.ComponentModel.DataAnnotations;

namespace tdCompany.Message.Request
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

    }

}
