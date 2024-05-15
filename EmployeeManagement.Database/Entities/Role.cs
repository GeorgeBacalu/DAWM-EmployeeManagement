namespace EmployeeManagement.Database.Entities
{
    public class Role : BaseEntity
    {
        public int Id { get; set; }
        public RoleType Type { get; set; }
    }

    public enum RoleType { User = 10, Admin = 20 }
}