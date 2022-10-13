using MovieApp.Dto;

namespace MovieApp.Message.Response
{
    public class CompanyListResponse : CommonResponse
    {
        public IEnumerable<CompanyDto>? CompanyList { get; set; }
    }
}
