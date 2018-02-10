using AMMS.Models.AccountViewModels;
using AMMS.Models.ViewModels;
using System.Collections.Generic;

namespace AMMS.Services
{
    public interface IUserService
    {
        RegisterViewModel GetUser(string id);

        IEnumerable<RegisterViewModel> GetUsers(string uic);

        IEnumerable<RegisterViewModel> GetAllUsers();

        void SaveUser(RegisterViewModel viewModel);

        void UpdateUser(RegisterViewModel viewModel);

        void DeleteUser(string id);

        string GetUserId(string email);

        string GetUserSalt(string id);

        IList<UserRolesViewModel> GetUserRoles(string id);

        void UpdateUserRoles(IList<UserRolesViewModel> assignments);

        //----------------------------------------------//

        IEnumerable<UnitViewModel> GetUnits();

        RequestViewModel GetRequest(string id);

        IEnumerable<RequestViewModel> GetAllRequests();

        bool RequestExists(string email);

        void SaveRequest(RequestViewModel viewModel);

        void DeleteRequest(string id);
    }
}
