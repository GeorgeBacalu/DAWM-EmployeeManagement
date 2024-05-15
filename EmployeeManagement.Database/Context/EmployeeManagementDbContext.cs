using EmployeeManagement.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Database.Context
{
    public class EmployeeManagementDbContext : DbContext
    {
        public EmployeeManagementDbContext(DbContextOptions<EmployeeManagementDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
    }
}