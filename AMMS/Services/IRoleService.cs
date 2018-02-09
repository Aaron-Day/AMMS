using AMMS.Models.AccountViewModels;
using System.Collections.Generic;

namespace AMMS.Services
{
    public interface IRoleService
    {
        IEnumerable<RegisterViewModel> GetUsers(string role);

        RoleListViewModel GetRole(string id);

        IEnumerable<RoleListViewModel> GetRoles();

        void SaveRole(RoleListViewModel viewModel);

        void UpdateRole(RoleListViewModel viewModel);

        void DeleteRole(string id);
    }
}
