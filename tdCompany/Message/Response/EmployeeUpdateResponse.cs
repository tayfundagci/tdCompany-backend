using MovieApp.Dto;
using MovieApp.Entities;

namespace MovieApp.Message.Response
{
    public class EmployeeUpdateResponse : CommonResponse
    {
        public EmployeeDto? Employee { get; set; }
    }
}
