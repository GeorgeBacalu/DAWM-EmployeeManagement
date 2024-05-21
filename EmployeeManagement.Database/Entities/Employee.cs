namespace EmployeeManagement.Database.Entities
{
    public class Employee : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string PasswordSalt { get; set; } = null!;
        public string Mobile { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateOnly Birthday { get; set; }
        public Role Role { get; set; } = null!;
        public IList<Authority> Authorities { get; set; } = null!;
        public EmploymentType EmploymentType { get; set; }
        public Position Position { get; set; }
        public Grade Grade { get; set; }
        public double Salary { get; set; }
        public DateOnly HiredAt { get; set; }
    }

    public enum EmploymentType { PartTime = 10, FullTime = 20 }

    public enum Position { Frontend = 10, Backend = 20, FullStack = 30, DevOps = 40, QA = 50, UiUx = 60, DataScientist = 70, MachineLearning = 80, BusinessAnalyst = 90, ScrumMaster = 100 }

    public enum Grade { Intern = 10, Junior = 20, Mid = 30, Senior = 40, Lead = 50 }
}