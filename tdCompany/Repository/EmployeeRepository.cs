using Dapper;
using MovieApp.Context;
using MovieApp.Dto;
using MovieApp.Entities;
using MovieApp.Message.Request;
using System.Data;
using tdCompany.Interfaces;

namespace MovieApp.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DapperContext _context;

        public EmployeeRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            var query = "SELECT Employee.Id, Employee.Name, Employee.Age, Employee.Position, Company.Id , Company.Name , Company.Address, Company.Country " +
                "FROM Employee INNER JOIN Company on Employee.CompanyId = Company.Id ";

            using (var connection = _context.CreateConnection())
            {
                var employees = await connection.QueryAsync<Employee, Company, Employee>(query, (employee, commpany) =>
                {
                    employee.Company = commpany;
                    return employee;
                }, splitOn: "Id");
                return employees;
            }
        }

        public async Task<Employee> GetEmployee(int id)
        {
            var query = "SELECT Employee.Id, Employee.Name, Employee.Age, Employee.Position, Company.Id, Company.Name, Company.Address, Company.Country " +
                "FROM Employee INNER JOIN Company on Employee.CompanyId = Company.Id WHERE Employee.Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var employee = await connection.QueryAsync<Employee, Company, Employee>(query, (employeee, commpany) =>
                {
                    employeee.Company = commpany;
                    return employeee;
                }, new { id }, splitOn: "Id");

                return employee.FirstOrDefault();
            }
        }

        public async Task<Employee> CreateEmployee(EmployeeForCreationDto employee)
        {
            var query = "INSERT INTO Employee (Name, Age, Position, CompanyId) VALUES (@Name, @Age, @Position, @CompanyId)" + " SELECT SCOPE_IDENTITY();";

            var parameters = new DynamicParameters();
            parameters.Add("Name", employee.Name, DbType.String);
            parameters.Add("Age", employee.Age, DbType.Int16);
            parameters.Add("Position", employee.Position, DbType.String);
            parameters.Add("CompanyId", employee.CompanyId, DbType.String);

            using(var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);

                var createdEmployee = new Employee
                {
                    Id = id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Position = employee.Position,
                };

                return createdEmployee;
            }
        }

        public async Task<Employee> UpdateEmployee(EmployeeUpdateRequest request)
        {
            var query = "UPDATE Employee SET Name = @Name, Age = @Age, Position = @Position, CompanyId = @CompanyId WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", request.Id, DbType.Int32);
            parameters.Add("Name", request.Name, DbType.String);
            parameters.Add("Age", request.Age, DbType.Int32);
            parameters.Add("Position", request.Position, DbType.String);
            parameters.Add("CompanyId", request.CompanyId, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
                return new Employee() { Id = request.Id.Value, Name = request.Name, Age = request.Age, Position = request.Position, Company = new Company() {Id= request.CompanyId }  };
            }
        }

        public async Task DeleteEmployee(int id)
        {
            var query = "DELETE FROM Employee WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }
    }
}
