using AMMS.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace AMMS.Repository
{
    public interface IRoleRepository
    {
        IEnumerable<ApplicationUser> GetUsers(string role);

        IdentityRole GetRole(string id);

        IEnumerable<IdentityRole> GetRoles();

        void SaveRole(IdentityRole role);

        void UpdateRole(IdentityRole role);

        void DeleteRole(string id);
    }
}
