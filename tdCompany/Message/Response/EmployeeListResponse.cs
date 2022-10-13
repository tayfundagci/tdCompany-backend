using MovieApp.Dto;

namespace MovieApp.Message.Response
{
    public class EmployeeListResponse : CommonResponse
    {
        public IEnumerable<EmployeeDto>? EmployeeList { get; set; }
    }
}
