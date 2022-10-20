using MovieApp.Dto;

namespace MovieApp.Message.Response
{
    public class UserDetailUpdateResponse : CommonResponse
    {
        public UserDetailDto? UserDetail { get; set; }
    }
}
