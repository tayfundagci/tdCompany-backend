using MovieApp.Dto;
using MovieApp.Entities;
using MovieApp.Message.Request;
using tdCompany.Dto;
using tdCompany.Entities;

namespace MovieApp
{
    public interface IUserDetailRepository
    {
        public Task<UserDetail> GetUserDetail(Guid UserDetailId);
        public Task<UserDetail> CreateUserDetail(UserDetailForCreationDto UserDetail);
    }
}
