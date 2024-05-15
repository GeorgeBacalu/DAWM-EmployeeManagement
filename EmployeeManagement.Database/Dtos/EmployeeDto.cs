using EmployeeManagement.Database.Entities;

namespace EmployeeManagement.Database.Dtos
{
    public class EmployeeDto : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Mobile { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateOnly Birthday { get; set; }
        public int RoleId { get; set; }
        public IList<int> AuthoritiesIds { get; set; } = null!;
        public EmploymentType EmploymentType { get; set; }
        public Position Position { get; set; }
        public Grade Grade { get; set; }
        public double Salary { get; set; }
        public DateOnly HiredAt { get; set; }
    }
}