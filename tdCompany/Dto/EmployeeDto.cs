using MovieApp.Entities;

namespace MovieApp.Dto
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Position { get; set; }
        public Company? Company { get; set; }


        public EmployeeDto(Employee employee)
        {
            this.Id = employee.Id;
            this.Name = employee.Name;
            this.Age = employee.Age;
            this.Position = employee.Position;
            this.Company = employee.Company;
        }
    }
}
