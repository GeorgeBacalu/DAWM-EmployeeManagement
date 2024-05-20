using EmployeeManagement.Database.Context;
using EmployeeManagement.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Database.Repositories
{
    public class EmployeeRepository
    {
        private readonly EmployeeManagementDbContext _context;

        public EmployeeRepository(EmployeeManagementDbContext context) => _context = context;

        public IList<Employee> GetAll() => _context.Employees
            .Include(employee => employee.Role)
            .Include(employee => employee.Authorities)
            .Where(employee => employee.DeletedAt == null)
            .OrderBy(employee => employee.CreatedAt)
            .ToList();

        public Employee GetById(int id) => _context.Employees
            .Include(employee => employee.Role)
            .Include(employee => employee.Authorities)
            .Where(employee => employee.DeletedAt == null)
            .FirstOrDefault(employee => employee.Id == id)
            ?? throw new Exception($"Employee with id {id} not found");

        public Employee Add(Employee employee)
        {
            employee.CreatedAt = DateTime.Now;
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return employee;
        }

        public Employee UpdateById(Employee employee, int id)
        {
            Employee employeeToUpdate = GetById(id);
            employeeToUpdate.Name = employee.Name;
            employeeToUpdate.Email = employee.Email;
            employeeToUpdate.Password = employee.Password;
            employeeToUpdate.Mobile = employee.Mobile;
            employeeToUpdate.Address = employee.Address;
            employeeToUpdate.Birthday = employee.Birthday;
            employeeToUpdate.EmploymentType = employee.EmploymentType;
            employeeToUpdate.Position = employee.Position;
            employeeToUpdate.Grade = employee.Grade;
            employeeToUpdate.Salary = employee.Salary;
            employeeToUpdate.HiredAt = employee.HiredAt;
            if (_context.Entry(employeeToUpdate).State == EntityState.Modified)
                employeeToUpdate.UpdatedAt = DateTime.Now;
            _context.SaveChanges();
            return employeeToUpdate;
        }

        public Employee DisableById(int id)
        {
            Employee employeeToDisable = GetById(id);
            employeeToDisable.DeletedAt = DateTime.Now;
            _context.SaveChanges();
            return employeeToDisable;
        }

        public void LinkRoleToEmployee(Employee employee) // for new employee, role entity state should be unchaged
        {
            Role existingRole = _context.Roles.Find(employee.Role.Id);
            if (existingRole != null)
            {
                _context.Entry(existingRole).State = EntityState.Unchanged;
                employee.Role = existingRole;
            }
        }

        public void LinkAuthoritiesToEmployee(Employee employee) // for new employee, authorities entity state should be unchanged
        {
            employee.Authorities = employee.Authorities
                .Select(authority => _context.Authorities.Find(authority.Id))
                .Where(authority => authority != null)
                .ToList();
            employee.Authorities.Select(authority => _context.Entry(authority).State = EntityState.Unchanged);
        }
    }
}