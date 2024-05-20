using EmployeeManagement.Database.Entities;

namespace EmployeeManagement.Database.Dtos.Common
{
    public class DepartmentDto : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public IList<int> EmployeesIds { get; set; } = null!;
    }
}