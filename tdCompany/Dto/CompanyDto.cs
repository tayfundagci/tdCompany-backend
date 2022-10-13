using MovieApp.Entities;

namespace MovieApp.Dto
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string? CompanyName { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }

        public CompanyDto(Company company)
        {
            this.Id = company.Id;
            this.CompanyName = company.Name;
            this.Address = company.Address;
            this.Country = company.Country;
        }
    }
}
