using MovieApp.Entities;

namespace tdCompany.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
        Guid ValidateToken(string token);
    }
}
