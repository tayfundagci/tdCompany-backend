using MovieApp.Dto;

namespace MovieApp.Message.Response
{
    public class EmployeeDetailResponse : CommonResponse
    {
        public EmployeeDto? Employee { get; set; }
    }
}
