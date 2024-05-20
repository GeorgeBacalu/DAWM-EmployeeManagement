using EmployeeManagement.Database.Entities;

namespace EmployeeManagement.Database.Dtos.Common
{
    public class RoleDto : BaseEntity
    {
        public int Id { get; set; }
        public RoleType Type { get; set; }
    }
}