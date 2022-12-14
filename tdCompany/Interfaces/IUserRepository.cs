using MovieApp.Entities;

namespace tdCompany.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByUsernameAndPassword(string username, string password);
        Task<User> GetById(Guid id);
        Task<Guid> CreateUser(User user);
    }
}
