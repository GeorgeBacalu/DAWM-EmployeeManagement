using EmployeeManagement.Database.Context;
using EmployeeManagement.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Database.Repositories
{
    public class UserRepository
    {
        private readonly EmployeeManagementDbContext _context;

        public UserRepository(EmployeeManagementDbContext context) => _context = context;

        public IList<User> GetAll()
        {
            IList<User> users = _context.Users
                .Include(user => user.Role)
                .Include(user => user.Authorities)
                .Where(user => user.DeletedAt == null)
                .OrderBy(user => user.CreatedAt)
                .ToList();
            return users;
        }

        public User GetById(int id)
        {
            User user = _context.Users
                .Include(user => user.Role)
                .Include(user => user.Authorities)
                .Where(user => user.DeletedAt == null)
                .FirstOrDefault(user => user.Id == id)
                ?? throw new Exception($"User with id {id} not found");
            return user;
        }

        public User GetByEmail(string email)
        {
            User user = _context.Users
                .Include(user => user.Role)
                .Include(user => user.Authorities)
                .Where(user => user.DeletedAt == null)
                .FirstOrDefault(user => user.Email == email)
                ?? throw new Exception($"User with email {email} not found");
            return user;
        }

        public User Add(User user)
        {
            user.CreatedAt = DateTime.Now;
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User UpdateById(User user, int id)
        {
            User userToUpdate = GetById(id);
            userToUpdate.Name = user.Name;
            userToUpdate.Email = user.Email;
            userToUpdate.Password = user.Password;
            if (_context.Entry(userToUpdate).State == EntityState.Modified)
                userToUpdate.UpdatedAt = DateTime.Now;
            return userToUpdate;
        }

        public User DisableById(int id)
        {
            User userToDisable = GetById(id);
            userToDisable.DeletedAt = DateTime.Now;
            _context.SaveChanges();
            return userToDisable;
        }

        public void LinkRoleToUser(User user) // for new user, role entity state should be unchanged
        {
            Role existingRole = _context.Roles.Find(user.Role.Id);
            if (existingRole != null)
            {
                _context.Entry(existingRole).State = EntityState.Unchanged;
                user.Role = existingRole;
            }
        }

        public void LinkAuthoritiesToUser(User user) // for new user, authorities entity state should be unchanged
        {
            user.Authorities = user.Authorities
                .Select(authority => _context.Authorities.Find(authority.Id))
                .Where(authority => authority != null)
                .ToList();
            user.Authorities.Select(authority => _context.Entry(authority).State = EntityState.Unchanged);
        }
    }
}