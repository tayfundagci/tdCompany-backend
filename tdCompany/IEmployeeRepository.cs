using MovieApp.Dto;
using MovieApp.Entities;
using MovieApp.Message.Request;

namespace MovieApp
{
    public interface IEmployeeRepository
    {
        public Task<IEnumerable<Employee>> GetEmployees();
        public Task<Employee> GetEmployee(int id);
        public Task<Employee> CreateEmployee(EmployeeForCreationDto employee);
        public Task<Employee> UpdateEmployee(EmployeeUpdateRequest request);
        public Task DeleteEmployee(int id);
    }
}
