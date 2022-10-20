using MovieApp.Entities;
using System.Text.Json.Serialization;

namespace MovieApp.Message.Request
{
    public class UserDetailUpdateRequest
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }

        public Guid UserDetailId { get; set; }

    }
}
