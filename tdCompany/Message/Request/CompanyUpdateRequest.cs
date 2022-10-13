using System.Text.Json.Serialization;

namespace MovieApp.Message.Request
{
    public class CompanyUpdateRequest
    {
        [JsonIgnore]
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
    }
}
