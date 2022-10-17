using MovieApp.Entities;

namespace MovieApp.Message.Response
{
    public class LoginResponse : CommonResponse
    {
        public string Token { get; set; }
        public string Username { get; set; }
        public Guid Id { get; set; }
        public UserRole Role { get; set; }
    }
}
