using AMMS.Data;
using AMMS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
