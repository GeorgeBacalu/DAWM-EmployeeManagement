namespace EmployeeManagement.Database.Entities
{
    public class Department : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public IList<Employee> Employees { get; set; } = null!;
    }
}