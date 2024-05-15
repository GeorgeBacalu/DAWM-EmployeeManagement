using EmployeeManagement.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EmployeeManagement.Database.Context
{
    public class EmployeeManagementDbContext : DbContext
    {
        public EmployeeManagementDbContext(DbContextOptions<EmployeeManagementDbContext> options) : base(options) { }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Authority> Authorities { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasIndex(employee => employee.Name).IsUnique();
                entity.HasIndex(employee => employee.Email).IsUnique();
                entity.Property(employee => employee.EmploymentType).HasConversion(new EnumToStringConverter<EmploymentType>());
                entity.Property(employee => employee.Position).HasConversion(new EnumToStringConverter<Position>());
                entity.Property(employee => employee.Grade).HasConversion(new EnumToStringConverter<Grade>());
            });
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasIndex(role => role.Type).IsUnique();
                entity.Property(role => role.Type).HasConversion(new EnumToStringConverter<RoleType>());
            });
            modelBuilder.Entity<Authority>(entity =>
            {
                entity.HasIndex(authority => authority.Type).IsUnique();
                entity.Property(authority => authority.Type).HasConversion(new EnumToStringConverter<AuthorityType>());
            });
        }
    }
}