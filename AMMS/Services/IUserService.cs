using AMMS.Models.AccountViewModels;
using System.Collections.Generic;
using AMMS.Models.ViewModels;

namespace AMMS.Services
{
    public interface IUserService
    {
        RegisterViewModel GetUser(string id);

        IEnumerable<RegisterViewModel> GetAllUsers();

        void SaveUser(RegisterViewModel viewModel);

        void UpdateUser(RegisterViewModel viewModel);

        void DeleteUser(string id);

        string GetUserId(string email);

        string GetUserSalt(string id);

        //----------------------------------------------//

        IEnumerable<UnitViewModel> GetUnits(); 
            
        RequestViewModel GetRequest(string id);

        IEnumerable<RequestViewModel> GetAllRequests();

        bool RequestExists(string email);

        void SaveRequest(RequestViewModel viewModel);

        void DeleteRequest(string id);
    }
}
