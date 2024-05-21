using EmployeeManagement.Database.Entities;

namespace EmployeeManagement.Database.Dtos.Common
{
    public class AuthorityDto : BaseEntity
    {
        public int Id { get; set; }
        public AuthorityType Type { get; set; }
    }
}