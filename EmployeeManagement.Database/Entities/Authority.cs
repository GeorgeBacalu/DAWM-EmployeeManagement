namespace EmployeeManagement.Database.Entities
{
    public class Authority : BaseEntity
    {
        public int Id { get; set; }
        public AuthorityType Type { get; set; }
        public IList<User> Users { get; set; } = null!;
        public IList<Employee> Employees { get; set; } = null!;
    }

    public enum AuthorityType { Create = 10, Read = 20, Update = 30, Delete = 40 }
}