using AMMS.Models;
using AMMS.Models.AccountViewModels;
using AMMS.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace AMMS.Repository
{
    public interface IAccountRepository
    {
        /*----------GETTERS----------*/
        // Get user(s)
        ApplicationUser GetUserById(string id);
        ApplicationUser GetUserByPid(string pid);
        ApplicationUser GetUserByEmail(string email);
        IEnumerable<ApplicationUser> GetUsersByRole(string role);
        IEnumerable<ApplicationUser> GetUsersByUic(string uic);
        IEnumerable<ApplicationUser> GetAllUsers();

        // Get role(s)
        IdentityRole GetRoleById(string id);
        IdentityRole GetRoleByName(string name);
        IEnumerable<IdentityRole> GetAllRoles();

        // Get user role(s)
        IList<UserRolesViewModel> GetUserRoles(string id);

        // Get user request(s)
        Request GetRequestById(string id);
        Request GetRequestByEmail(string email);
        IEnumerable<Request> GetRequestsByUic(string uic);
        IEnumerable<Request> GetAllRequests();

        // Get unit(s)
        Unit GetUnitById(string id);
        Unit GetUnitByUic(string uic);
        IEnumerable<Unit> GetAllUnits();


        /*----------SETTERS----------*/
        // Set user(s)
        void CreateUser(ApplicationUser user);
        void UpdateUser(ApplicationUser user);
        void DeleteUser(string id);

        // Set role(s)
        void CreateRole(IdentityRole role);
        void UpdateRole(IdentityRole role);
        void DeleteRole(string id);

        // Set user role(s)
        void UpdateUserRoles(IList<UserRolesViewModel> assignments);

        // Set user request(s)
        void CreateRequest(Request request);
        void DeleteRequest(string id);

        // Set unit(s)
        void CreateUnit(Unit unit);
        void UpdateUnit(Unit unit);
        void DeleteUnit(string id);


        /*----------ACCOUNT----------*/
        SignInResult Login(LoginViewModel login);
        void Logout(string id);
        void ResetPassword(string id);
        void ChangePassword(ChangePasswordViewModel change);
        string GetType(string value);
    }
}
