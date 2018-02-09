using AMMS.Data;
using AMMS.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace AMMS.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _manager;

        public RoleRepository(ApplicationDbContext context, RoleManager<IdentityRole> manager)
        {
            _context = context;
            _manager = manager;
        }

        public IEnumerable<ApplicationUser> GetUsers(string role)
        {
            var roleEntity = _context.Roles.FirstOrDefault(r => r.Name == role);
            var userRoles = _context.UserRoles.Where(r => r.RoleId == roleEntity.Id).ToList();

            return userRoles.Select(u => _context.Users.Find(u.UserId)).ToList();
        }

        public IdentityRole GetRole(string id)
        {
            return _context.Roles.Find(id);
        }

        public IEnumerable<IdentityRole> GetRoles()
        {
            return _context.Roles.ToList();
        }

        public void SaveRole(IdentityRole role)
        {
            _manager.CreateAsync(role).Wait();

            _context.SaveChanges();
        }

        public void UpdateRole(IdentityRole role)
        {
            _context.SaveChanges();
        }

        public void DeleteRole(string id)
        {
            var role = _context.Roles.Find(id);

            if (role == null) return;

            var userRoles = _context.UserRoles.Where(r => r.RoleId == id).ToList();
            foreach (var userRole in userRoles)
            {
                _context.UserRoles.Remove(userRole);
            }

            _context.Roles.Remove(role);

            _context.SaveChanges();
        }
    }
}
