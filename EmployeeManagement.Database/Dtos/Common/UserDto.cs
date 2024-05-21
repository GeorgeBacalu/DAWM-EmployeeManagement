using EmployeeManagement.Database.Entities;

namespace EmployeeManagement.Database.Dtos.Common
{
    public class UserDto : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string PasswordSalt { get; set; } = null!;
        public int RoleId { get; set; }
        public IList<int> AuthoritiesIds { get; set; } = null!;
    }
}