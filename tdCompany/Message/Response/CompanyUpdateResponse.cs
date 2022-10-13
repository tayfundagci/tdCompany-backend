using MovieApp.Dto;

namespace MovieApp.Message.Response
{
    public class CompanyUpdateResponse : CommonResponse
    {
        public CompanyDto? Company { get; set; }
    }
}
