using AMMS.Models;
using System.Collections.Generic;

namespace AMMS.Repository
{
    public interface IRoleRepository
    {
        IEnumerable<ApplicationUser> GetUsers(string role);

        IEnumerable<string> GetRoles();

        void SaveRole(string role);

        void UpdateRole(string role);

        void DeleteRole(string role);
    }
}
