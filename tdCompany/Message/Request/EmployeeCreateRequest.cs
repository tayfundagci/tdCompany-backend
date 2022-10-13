namespace MovieApp.Message.Request
{
    public class EmployeeCreateRequest
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Position { get; set; }
        public int CompanyId { get; set; }
    }
}
