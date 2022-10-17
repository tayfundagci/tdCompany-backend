using MovieApp.Entities;

namespace MovieApp.Dto
{
    public class UserDetailForCreationDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public Guid UserDetailId { get; set; }
    }
}
