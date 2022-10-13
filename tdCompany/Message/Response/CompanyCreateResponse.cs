using MovieApp.Dto;

namespace MovieApp.Message.Response
{
    public class CompanyCreateResponse : CommonResponse
    {
        public CompanyDto? Company { get; set; }
    }
}
