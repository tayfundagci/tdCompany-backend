using System.Text.Json.Serialization;

namespace MovieApp.Message.Request
{
    public class EmployeeUpdateRequest
    {
        [JsonIgnore]
        public int? Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Position { get; set; }
        public int CompanyId { get; set; }
    }
}
