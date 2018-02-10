using AMMS.Models;
using AMMS.Models.ViewModels;
using System.Collections.Generic;

namespace AMMS.Repository
{
    public interface IUserRepository
    {
        ApplicationUser GetUser(string id);

        IEnumerable<ApplicationUser> GetUsers(string uic);

        IEnumerable<ApplicationUser> GetAllUsers();

        void SaveUser(ApplicationUser user);

        void UpdateUser(ApplicationUser user);

        void DeleteUser(string id);

        string GetUserId(string email);

        IList<UserRolesViewModel> GetUserRoles(string id);

        void UpdateUserRoles(IList<UserRolesViewModel> assignments);

        //--------------------------------------------//

        IEnumerable<Unit> GetUnits();

        Request GetRequest(string id);

        IEnumerable<Request> GetAllRequests();

        bool RequestExists(string email);

        void SaveRequest(Request request);

        void DeleteRequest(string id);
    }
}
