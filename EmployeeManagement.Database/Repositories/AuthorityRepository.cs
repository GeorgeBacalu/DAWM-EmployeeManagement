using EmployeeManagement.Database.Context;
using EmployeeManagement.Database.Entities;

namespace EmployeeManagement.Database.Repositories
{
    public class AuthorityRepository
    {
        private readonly EmployeeManagementDbContext _context;

        public AuthorityRepository(EmployeeManagementDbContext context) => _context = context;

        public IList<Authority> GetAll()
        {
            IList<Authority> authorities = _context.Authorities
                .Where(authority => authority.DeletedAt == null)
                .OrderBy(authority => authority.CreatedAt)
                .ToList();
            return authorities;
        }

        public Authority GetById(int id)
        {
            Authority authority = _context.Authorities
                .Where(authority => authority.DeletedAt == null)
                .FirstOrDefault(authority => authority.Id == id)
                ?? throw new Exception($"Authority with id {id} not found");
            return authority;
        }

        public IList<Authority> GetByRole(Role role)
        {
            IList<Authority> authoritiesByRole = _context.Authorities
                .Where(authority => authority.DeletedAt == null)
                .Where(authority => role.Type != RoleType.User || authority.Type != AuthorityType.Delete)
                .OrderBy(authority => authority.CreatedAt)
                .ToList();
            return authoritiesByRole;
        }

        public Authority Add(Authority authority)
        {
            authority.CreatedAt = DateTime.Now;
            _context.Authorities.Add(authority);
            _context.SaveChanges();
            return authority;
        }
    }
}