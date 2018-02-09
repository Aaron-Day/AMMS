using AMMS.Data;
using AMMS.Models;
using System.Collections.Generic;
using System.Linq;

namespace AMMS.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ApplicationUser> GetUsers(string role)
        {
            var users = new List<ApplicationUser>();
            var roleEntity = _context.Roles.FirstOrDefault(r => r.Name == role);
            var userRoles = _context.UserRoles.Where(r => r.RoleId == roleEntity.Id).ToList();

            foreach (var u in userRoles)
            {
                var user = _context.Users.Find(u.UserId);
                users.Add(user);
            }

            return users;
        }

        public IEnumerable<string> GetRoles()
        {
            var roles = new List<string>();
            var allRoles = _context.Roles.ToList();
            foreach (var r in allRoles)
            {
                roles.Add(r.Name);
            }

            return roles;
        }

        public void SaveRole(string role)
        {
            // TODO: saved link in school/oit/'web design' folder for dealing with roles
        }

        public void UpdateRole(string role)
        {

        }

        public void DeleteRole(string role)
        {

        }
    }
}
