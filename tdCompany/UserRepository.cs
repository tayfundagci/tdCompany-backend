using Dapper;
using MovieApp.Context;
using MovieApp.Entities;

namespace MovieApp
{
    public class UserRepository : IUserRepository
    {

        private readonly DapperContext _context;

        public UserRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<User> GetById(Guid id)
        {
            var query = "SELECT * from Users where Id=@Id";

            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QueryAsync<User>(query, new { id });
                return user.FirstOrDefault();
            }
        }

        public async Task<User> GetByUsernameAndPassword(string username, string password)
        {
            var query = "SELECT * from Users where Username=@Username and Password=@Password";

            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QueryAsync<User>(query, new {username,password});
                return user.FirstOrDefault();
            }
        }
    }
}
