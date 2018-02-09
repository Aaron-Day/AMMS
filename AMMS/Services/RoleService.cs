using AMMS.Models;
using AMMS.Models.AccountViewModels;
using AMMS.Repository;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace AMMS.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;

        public RoleService(IRoleRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<RegisterViewModel> GetUsers(string role)
        {
            var users = _repository.GetUsers(role);

            return users.Select(MapToRegisterViewModel).ToList();
        }

        public RoleListViewModel GetRole(string id)
        {
            return MapToRoleListViewModel(_repository.GetRole(id));
        }

        public IEnumerable<RoleListViewModel> GetRoles()
        {
            var roles = _repository.GetRoles();

            return roles.Select(MapToRoleListViewModel).ToList();
        }

        public void SaveRole(RoleListViewModel viewModel)
        {
            _repository.SaveRole(MapToRole(viewModel));
        }

        public void UpdateRole(RoleListViewModel viewModel)
        {
            var role = _repository.GetRole(viewModel.Id);
            CopyToRole(viewModel, role);

            _repository.UpdateRole(role);
        }

        public void DeleteRole(string id)
        {
            _repository.DeleteRole(id);
        }

        //-----------------------------------------------------------------//

        private RoleListViewModel MapToRoleListViewModel(IdentityRole role)
        {
            return new RoleListViewModel
            {
                Id = role.Id,
                RoleName = role.Name,
                NumberOfUsers = GetUsers(role.Name).Count()
            };
        }

        private static IdentityRole MapToRole(RoleListViewModel viewModel)
        {
            return new IdentityRole
            {
                Id = viewModel.Id,
                Name = viewModel.RoleName,
                NormalizedName = viewModel.RoleName.ToUpper()
            };
        }

        private static void CopyToRole(RoleListViewModel viewModel, IdentityRole role)
        {
            role.Id = viewModel.Id;
            role.Name = viewModel.RoleName;
            role.NormalizedName = viewModel.RoleName.ToUpper();
        }

        private static RegisterViewModel MapToRegisterViewModel(ApplicationUser user)
        {
            return new RegisterViewModel
            {
                Id = user.Id,
                Rank = user.Rank,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                DateOfBirth = user.DateOfBirth,
                SocialSecurityNumber = user.SocialSecurityNumber,
                AssignedUnit = user.AssignedUnit
            };
        }
    }
}
