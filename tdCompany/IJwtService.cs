using MovieApp.Entities;

namespace MovieApp
{
    public interface IJwtService
    {
        string GenerateToken(User user);
        Guid ValidateToken(string token);
    }
}
