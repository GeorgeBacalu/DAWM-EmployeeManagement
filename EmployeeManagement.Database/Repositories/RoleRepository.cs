using EmployeeManagement.Database.Context;
using EmployeeManagement.Database.Entities;

namespace EmployeeManagement.Database.Repositories
{
    public class RoleRepository
    {
        private readonly EmployeeManagementDbContext _context;

        public RoleRepository(EmployeeManagementDbContext context) => _context = context;

        public IList<Role> GetAll()
        {
            IList<Role> roles = _context.Roles
                .Where(role => role.DeletedAt == null)
                .OrderBy(role => role.CreatedAt)
                .ToList();
            return roles;
        }

        public Role GetById(int id)
        {
            Role role = _context.Roles
                .Where(role => role.DeletedAt == null)
                .FirstOrDefault(role => role.Id == id)
                ?? throw new Exception($"Role with id {id} not found");
            return role;
        }

        public Role Add(Role role)
        {
            role.CreatedAt = DateTime.Now;
            _context.Roles.Add(role);
            _context.SaveChanges();
            return role;
        }
    }
}