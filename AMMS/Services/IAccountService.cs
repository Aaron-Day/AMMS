using AMMS.Models;
using AMMS.Models.AccountViewModels;
using AMMS.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;

namespace AMMS.Services
{
    public interface IAccountService
    {
        /*----------GETTERS----------*/
        // Get user(s)
        ApplicationUser GetCurrentUser(ClaimsPrincipal user);
        RegisterViewModel GetUserById(string id);
        RegisterViewModel GetUserByPid(string pid);
        RegisterViewModel GetUserByEmail(string email);
        IEnumerable<RegisterViewModel> GetUsersByRole(string role);
        IEnumerable<RegisterViewModel> GetUsersByUic(string uic);
        IEnumerable<RegisterViewModel> GetAllUsers();

        // Get role(s)
        RoleListViewModel GetRoleById(string id);
        RoleListViewModel GetRoleByName(string name);
        IEnumerable<RoleListViewModel> GetAllRoles();

        // Get user role(s)
        IList<UserRolesViewModel> GetUserRoles(string id);

        // Get user request(s)
        RequestViewModel GetRequestById(string id);
        RequestViewModel GetRequestByEmail(string email);
        IEnumerable<RequestViewModel> GetRequestsByUic(string uic);
        IEnumerable<RequestViewModel> GetAllRequests();

        // Get unit(s)
        UnitViewModel GetUnitById(string id);
        UnitViewModel GetUnitByUic(string uic);
        IEnumerable<UnitViewModel> GetAllUnits();


        /*----------SETTERS----------*/
        // Set user(s)
        void CreateUser(RegisterViewModel user);
        void UpdateUser(RegisterViewModel user);
        void DeleteUser(string id);

        // Set role(s)
        void CreateRole(RoleListViewModel role);
        void UpdateRole(RoleListViewModel role);
        void DeleteRole(string id);

        // Set user role(s)
        void UpdateUserRoles(IList<UserRolesViewModel> assignments);

        // Set user request(s)
        void CreateRequest(RequestViewModel request);
        void DeleteRequest(string id);

        // Set unit(s)
        void CreateUnit(UnitViewModel unit);
        void UpdateUnit(UnitViewModel unit);
        void DeleteUnit(string id);


        /*----------ACCOUNT----------*/
        SignInResult Login(LoginViewModel login);
        void Logout(string id);
        void ResetPassword(string id);
        void ChangePassword(ChangePasswordViewModel change);
        string GetType(string value, ClaimsPrincipal user);
        IEnumerable<RegisterViewModel> GetUsers(string value, ClaimsPrincipal user);
    }
}
