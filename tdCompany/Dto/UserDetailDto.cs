using MovieApp.Entities;
using tdCompany.Entities;

namespace MovieApp.Dto
{
    public class UserDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public Guid UserDetailId { get; set; }
        public User? User { get; set; }


        public UserDetailDto(UserDetail user)
        {
            this.Id = user.Id;
            this.Name = user.Name;
            this.Surname = user.Surname;
            this.Address = user.Address;
            this.Phone = user.Phone;
            this.UserDetailId = user.UserDetailId;
            this.User = user.User;
        }
    }
}
