using EmployeeManagement.Database.Context;
using EmployeeManagement.Database.Entities;

namespace EmployeeManagement.Database.Repositories
{
    public class AuthorityRepository
    {
        private readonly EmployeeManagementDbContext _context;

        public AuthorityRepository(EmployeeManagementDbContext context) => _context = context;

        public IList<Authority> GetAll() => _context.Authorities
            .Where(authority => authority.DeletedAt == null)
            .OrderBy(authority => authority.CreatedAt)
            .ToList();

        public Authority GetById(int id) => _context.Authorities
            .Where(authority => authority.DeletedAt == null)
            .FirstOrDefault(authority => authority.Id == id)
            ?? throw new Exception($"Authority with id {id} not found");

        public IList<Authority> GetByRole(Role role)
        {
            IList<Authority> authorities = GetAll();
            if (role.Type == RoleType.User)
                authorities.ToList().RemoveAll(authority => authority.Type == AuthorityType.Delete);
            return authorities;
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