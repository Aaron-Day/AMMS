using AMMS.Data;
using AMMS.Models;
using AMMS.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace AMMS.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ApplicationUser GetUser(string id)
        {
            return _context.Users.Find(id);
        }

        public IEnumerable<ApplicationUser> GetUsers(string uic)
        {
            return uic == null
                ? _context.Users.ToList()
                : _context.Users.Where(u => u.AssignedUnit == uic).ToList();
        }

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public void SaveUser(ApplicationUser user)
        {
            var userStore = new UserStore<ApplicationUser>(_context);
            userStore.CreateAsync(user).Wait();
            userStore.AddToRoleAsync(user, "CE").Wait();

            _context.SaveChanges();
        }

        public void UpdateUser(ApplicationUser user)
        {
            _context.SaveChanges();
        }

        public void DeleteUser(string id)
        {
            var user = _context.Users.Find(id);

            if (user == null) return;

            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public string GetUserId(string email)
        {
            return _context.Users.SingleOrDefault(u => u.Email == email)?.Id;
        }

        public IList<UserRolesViewModel> GetUserRoles(string id)
        {
            var roles = _context.Roles.ToList();
            var userRoles = _context.UserRoles.ToList();
            return roles.Select(role => new UserRolesViewModel
            {
                UserId = id,
                RoleId = role.Id,
                RoleName = role.Name,
                Assigned = userRoles.SingleOrDefault(u => u.UserId == id && u.RoleId == role.Id) != null
            })
                .ToList();
        }

        public void UpdateUserRoles(IList<UserRolesViewModel> assignments)
        {
            var userRoles = _context.UserRoles.ToList();

            var userManager = _context.GetService<UserManager<ApplicationUser>>();
            foreach (var assignment in assignments)
            {
                if (userRoles.SingleOrDefault(u => u.UserId == assignment.UserId && u.RoleId == assignment.RoleId) ==
                    null)
                {
                    if (assignment.Assigned)
                    {
                        /*ADD TO USER ROLES*/
                        var user = GetUser(assignment.UserId);
                        var role = assignment.RoleName.ToUpper();
                        userManager.AddToRoleAsync(user, role).Wait();
                    }
                }
                else
                {
                    if (!assignment.Assigned)
                    {
                        /*REMOVE FROM USER ROLES*/
                        var user = GetUser(assignment.UserId);
                        var role = assignment.RoleName.ToUpper();
                        userManager.RemoveFromRoleAsync(user, role).Wait();
                    }
                }
            }
            _context.SaveChanges();
        }

        //------------------------------------------------------//

        public IEnumerable<Unit> GetUnits()
        {
            return _context.Units.ToList();
        }

        public Request GetRequest(string id)
        {
            return _context.Requests.Find(id);
        }

        public IEnumerable<Request> GetAllRequests()
        {
            return _context.Requests.ToList();
        }

        public bool RequestExists(string email)
        {
            return _context.Requests.Any(r => r.Email == email);
        }

        public void SaveRequest(Request request)
        {
            _context.Requests.Add(request);
            _context.SaveChanges();
        }

        public void DeleteRequest(string id)
        {
            var request = _context.Requests.Find(id);

            if (request == null) return;

            _context.Requests.Remove(request);
            _context.SaveChanges();
        }
    }
}
