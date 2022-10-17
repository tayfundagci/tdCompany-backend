using MovieApp.Dto;
using tdCompany.Entities;

namespace MovieApp.Message.Response
{
    public class UserDetailResponse : CommonResponse
    {
        public UserDetail? UserDetail { get; set; }
    }
}
