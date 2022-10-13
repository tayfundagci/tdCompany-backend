using Dapper;
using MovieApp.Context;
using MovieApp.Dto;
using MovieApp.Entities;
using MovieApp.Message.Request;
using System.Data;

namespace MovieApp.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DapperContext _context;

        public CompanyRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Company>> GetCompanies()
        {
            var query = "SELECT Id, Name, Address, Country FROM Company";

            using (var connection = _context.CreateConnection())
            {
                var companies = await connection.QueryAsync<Company>(query);
                return companies.ToList();
            }
        }

        public async Task<Company> GetCompany(int id)
        {
            var query = "SELECT Id, Name, Address, Country FROM Company WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var company = await connection.QuerySingleOrDefaultAsync<Company>(query, new {id});
                return company;
            }
        }

        public async Task<IEnumerable<CompanyPersonCount>> GetCompanyPerson()
        {
            var query = "SELECT Company.Name, Count(Company.Name) as PersonCount FROM Company, Employee WHERE Employee.CompanyId = Company.Id GROUP BY Company.Name ORDER BY PersonCount DESC";

            using(var connection = _context.CreateConnection())
            {
                var companyPersonCount = await connection.QueryAsync<CompanyPersonCount>(query);
                return companyPersonCount;
            }
        }

        public async Task<Company> CreateCompany(CompanyCreateRequest company)
        {
            var query = "INSERT INTO Company (Name, Address, Country) VALUES (@Name, @Address, @Country);" + " SELECT SCOPE_IDENTITY();";

            var parameters = new DynamicParameters();
            parameters.Add("Name", company.Name, DbType.String);
            parameters.Add("Address", company.Address, DbType.String);
            parameters.Add("Country", company.Country, DbType.String);

            using(var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);

                var createdCompany = new Company
                {
                    Id = id,
                    Name = company.Name,
                    Address = company.Address,
                    Country = company.Country
                };

                return createdCompany;
            }
        }

        public async Task<Company> UpdateCompany(CompanyUpdateRequest request)
        {
            var query = "UPDATE Company SET Name = @Name, Address = @Address, Country = @Country WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", request.Id, DbType.Int32);
            parameters.Add("Name", request.Name, DbType.String);
            parameters.Add("Address", request.Address, DbType.String);
            parameters.Add("Country", request.Country, DbType.String);

            using(var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
                return new Company() { Id = request.Id.Value, Name = request.Name, Country = request.Country };
            }

           
        }

        public async Task DeleteCompany(int id)
        {
            var query = "DELETE FROM Company WHERE Id = @Id";

            using(var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }


        
    }
}
