using Microsoft.AspNetCore.Mvc;
using MovieApp.Dto;
using MovieApp.Entities;
using MovieApp.Message.Request;

namespace MovieApp.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepo;

        public EmployeeController(IEmployeeRepository employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var employees = await _employeeRepo.GetEmployees();

                var response = new Message.Response.EmployeeListResponse()
                {
                    Code = 200,
                    Message = "Success",
                    EmployeeList = ConvertTo(employees.ToList())
                };

                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("{id}", Name = "EmployeeById")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            try
            {
                var employee = await _employeeRepo.GetEmployee(id);

                if (employee == null)
                    return NotFound();

                var response = new Message.Response.EmployeeDetailResponse()
                {
                    Code = 200,
                    Message = "Success",
                    Employee = new EmployeeDto(employee)
                };

                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(UserRole.Admin)]
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(EmployeeForCreationDto employee)
        {
            try
            {
                var createdEmployee = await _employeeRepo.CreateEmployee(employee);
                var response = new Message.Response.EmployeeCreateResponse()
                {
                    Code = 200,
                    Message = "A new employee was created",
                    Employee = new EmployeeDto(createdEmployee)
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(UserRole.Admin)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EmployeeUpdateRequest request)
        {
            try
            {
                var dbEmployee = await _employeeRepo.GetEmployee(id);
                if (dbEmployee == null)
                    return NotFound();
                else
                {
                    request.Id = id;
                    var result = await _employeeRepo.UpdateEmployee(request);
                    var updatedResult = await _employeeRepo.GetEmployee(id);
                    var response = new Message.Response.EmployeeUpdateResponse()
                    {
                        Code = 200,
                        Message = "Employee Updated",
                        Employee = new EmployeeDto(updatedResult)
                    };

                    return Ok(response);
                }

            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(UserRole.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var dbEmployee = await _employeeRepo.GetEmployee(id);
                if (dbEmployee == null)
                    return NotFound();

                var response = new Message.Response.EmployeeDeleteResponse()
                {
                    Code = 200,
                    Message = "Employee deleted",
                };

                await _employeeRepo.DeleteEmployee(id);
                    return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        public List<EmployeeDto> ConvertTo(List<Employee> employeeList)
        {
            List<EmployeeDto> dtoList = new List<EmployeeDto>();
            foreach (Employee employee in employeeList)
            {
                dtoList.Add(new EmployeeDto(employee));
            }
            return dtoList;
        }

    }
}
