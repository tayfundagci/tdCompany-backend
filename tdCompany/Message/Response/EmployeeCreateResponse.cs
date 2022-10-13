using MovieApp.Dto;

namespace MovieApp.Message.Response
{
    public class EmployeeCreateResponse : CommonResponse
    {
        public EmployeeDto? Employee { get; set; }
    }
}
