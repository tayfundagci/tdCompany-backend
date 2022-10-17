

using MovieApp.Entities;

namespace tdCompany.Entities
{
    public class UserDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public Guid UserDetailId { get; set; }
        public User? User { get; set; }
    }
}
