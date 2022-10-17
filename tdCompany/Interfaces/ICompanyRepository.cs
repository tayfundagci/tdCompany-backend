using MovieApp.Dto;
using MovieApp.Entities;
using MovieApp.Message.Request;

namespace tdCompany.Interfaces
{
    public interface ICompanyRepository
    {
        public Task<IEnumerable<Company>> GetCompanies();
        public Task<Company> GetCompany(int id);
        public Task<IEnumerable<CompanyPersonCount>> GetCompanyPerson();
        public Task<Company> CreateCompany(CompanyCreateRequest request);
        public Task<Company> UpdateCompany(CompanyUpdateRequest request);
        public Task DeleteCompany(int id);

    }
}
