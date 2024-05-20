namespace EmployeeManagement.Database.Entities
{
    public class User : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public Role Role { get; set; } = null!;
        public IList<Authority> Authorities { get; set; } = null!;
    }
}