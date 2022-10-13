using Dapper;
using Microsoft.Data.SqlClient;
using MovieApp;
using MovieApp.Context;
using MovieApp.Dto;
using MovieApp.Entities;

namespace tdCompany.Repository
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
                var user = await connection.QueryAsync<User>(query, new { username, password });
                return user.FirstOrDefault();
            }
        }

        public async Task<Guid> CreateUser(User user)
        {
            var query = "INSERT INTO Users (Id, Username, Password, Role) values (@Id, @Username, @Password, @Role)";

            using (var connection = _context.CreateConnection())
            {
                    await connection.QueryAsync(query, user);
                    return user.Id; 
            }
        }
    }
}
