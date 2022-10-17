using Dapper;
using MovieApp.Context;
using MovieApp.Dto;
using MovieApp.Entities;
using MovieApp.Message.Request;
using System.Data;
using tdCompany.Entities;

namespace MovieApp.Repository
{
    public class UserDetailRepository : IUserDetailRepository
    {
        private readonly DapperContext _context;

        public UserDetailRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<UserDetail> GetUserDetail(Guid UserDetailId)
        {
            var query = "Select UserDetail.Id, UserDetail.Name, UserDetail.Surname, UserDetail.Address, UserDetail.Phone, UserDetail.UserDetailId, Users.Id, Users.Username, Users.Role FROM UserDetail INNER JOIN Users on Users.Id = UserDetail.UserDetailId WHERE UserDetail.UserDetailId = @UserDetailId";

            using (var connection = _context.CreateConnection())
            {
                var userdetail = await connection.QueryAsync<UserDetail, User, UserDetail>(query, (userdetail, user) =>
                {
                    userdetail.User = user;
                    return userdetail;
                }, new { UserDetailId }, splitOn: "Id");

                return userdetail.FirstOrDefault();
            }
        }

        public async Task<UserDetail> CreateUserDetail(UserDetailForCreationDto UserDetail)
        {
            var query = "insert into UserDetail (Name, Surname, Address,Phone, UserDetailId) values (@Name, @Surname, @Address, @Phone, @UserDetailId)" + " SELECT SCOPE_IDENTITY();";

            var parameters = new DynamicParameters();
            parameters.Add("Name", UserDetail.Name, DbType.String);
            parameters.Add("Surname", UserDetail.Surname, DbType.String);
            parameters.Add("Address", UserDetail.Address, DbType.String);
            parameters.Add("Phone", UserDetail.Phone, DbType.String);
            parameters.Add("UserDetailId", UserDetail.UserDetailId, DbType.Guid);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);

                var createdUserDetail = new UserDetail
                {
                    Id = id,
                    Name = UserDetail.Name,
                    Surname = UserDetail.Surname,
                    Address = UserDetail.Address,
                    Phone = UserDetail.Phone,
                    UserDetailId = UserDetail.UserDetailId,
                };

                return createdUserDetail;
            }
        }

    }
}
