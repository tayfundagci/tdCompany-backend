using MovieApp.Dto;

namespace MovieApp.Message.Response
{
    public class CompanyDetailResponse : CommonResponse
    {
        public CompanyDto? Company { get; set; }
    }
}
