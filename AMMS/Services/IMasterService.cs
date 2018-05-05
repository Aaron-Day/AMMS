using AMMS.Models;
using AMMS.Models.AccountViewModels;
using AMMS.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;

namespace AMMS.Services
{
    public interface IMasterService
    {
        /***************************************************************************
         * 
         * REQUESTS
         * 
         ***************************************************************************/

        /***   GETTERS   ***/

        RequestViewModel GetRequestById(string id);
        RequestViewModel GetRequestByEmail(string email);
        IEnumerable<RequestViewModel> GetRequestsByUic(string uic);
        IEnumerable<RequestViewModel> GetAllRequests();

        /***   SETTERS   ***/

        void CreateRequest(RequestViewModel request);
        void DeleteRequest(string id);

        /***************************************************************************
         * 
         * USERS
         * 
         ***************************************************************************/

        /***   GETTERS   ***/

        ApplicationUser GetCurrentUser(ClaimsPrincipal user);
        RegisterViewModel GetUserById(string id);
        RegisterViewModel GetUserByPid(string pid);
        RegisterViewModel GetUserByEmail(string email);
        IEnumerable<RegisterViewModel> GetUsersByRole(string role);
        IEnumerable<RegisterViewModel> GetUsersByUic(string uic);
        IEnumerable<RegisterViewModel> GetAllUsers();
        IEnumerable<RegisterViewModel> GetUsers(string value, ClaimsPrincipal user);

        /***   SETTERS   ***/

        void CreateUser(RegisterViewModel user);
        void UpdateUser(RegisterViewModel user);
        void DeleteUser(string id);

        /***   MISC ACCOUNT   ***/

        SignInResult Login(LoginViewModel login);
        void Logout(string id);
        void ResetPassword(string id);
        void ChangePassword(ChangePasswordViewModel change);
        string GetType(string value, ClaimsPrincipal user);

        /***************************************************************************
         * 
         * ROLES
         * 
         ***************************************************************************/

        /***   GETTERS   ***/

        RoleListViewModel GetRoleById(string id);
        RoleListViewModel GetRoleByName(string name);
        IEnumerable<RoleListViewModel> GetAllRoles();

        /***   SETTERS   ***/

        void CreateRole(RoleListViewModel role);
        void UpdateRole(RoleListViewModel role);
        void DeleteRole(string id);

        /***************************************************************************
         * 
         * USER ROLES
         * 
         ***************************************************************************/

        /***   GETTERS   ***/

        IList<UserRolesViewModel> GetUserRoles(string id);

        /***   SETTERS   ***/

        void UpdateUserRoles(IList<UserRolesViewModel> assignments);

        /***************************************************************************
         * 
         * UNITS
         * 
         ***************************************************************************/

        /***   GETTERS   ***/

        string GetUnitId(string id);
        UnitViewModel GetUnitById(string id);
        UnitViewModel GetUnitByUic(string uic);
        IEnumerable<UnitViewModel> GetAllUnits();

        /***   SETTERS   ***/

        void CreateUnit(UnitViewModel unit);
        void UpdateUnit(UnitViewModel unit);
        void DeleteUnit(string id);

        /***************************************************************************
         * 
         * AIRCRAFT MODELS
         * 
         ***************************************************************************/

        /***   GETTERS   ***/

        AircraftModelViewModel GetAircraftModelById(string id);
        AircraftModelViewModel GetAircraftModelByMds(string mds);
        IEnumerable<AircraftModelViewModel> GetAircraftModelsByUIC(string uic);
        IEnumerable<AircraftModelViewModel> GetAllAircraftModels();

        /***   SETTERS   ***/

        void CreateAircraftModel(AircraftModelViewModel model);
        void UpdateAircraftModel(AircraftModelViewModel model);
        void DeleteAircraftModel(string id);

        /***************************************************************************
         * 
         * AIRCRAFT
         * 
         ***************************************************************************/

        /***   GETTERS   ***/

        AircraftViewModel GetAircraftById(string id);
        IEnumerable<AircraftViewModel> GetAircraftByUnitId(string id);
        IEnumerable<AircraftViewModel> GetAircraftByUIC(string uic);
        IEnumerable<AircraftViewModel> GetAllAircraft();

        /***   SETTERS   ***/

        void CreateAircraft(AircraftViewModel aircraft);
        void UpdateAircraft(AircraftViewModel aircraft);
        void DeleteAircraft(string id);

        /***************************************************************************
         * 
         * FLIGHT RECORDS
         * 
         ***************************************************************************/

        /***   GETTERS   ***/

        FlightViewModel GetFlightById(string id);
        IEnumerable<FlightViewModel> GetFlightsByAircraftId(string aircraftId);

        /***   SETTERS   ***/

        void CreateFlight(FlightViewModel flight);
        void UpdateFlight(FlightViewModel flight);
        void DeleteFlight(string id);

        /***************************************************************************
         * 
         * INSPECTION RECORDS
         * 
         ***************************************************************************/

        /***   GETTERS   ***/

        InspectionViewModel GetInspectionById(string id);
        IEnumerable<InspectionViewModel> GetInspectionsByAircraftId(string aircraftId);

        /***   SETTERS   ***/

        void CreateInspection(InspectionViewModel inspection);
        void UpdateInspection(InspectionViewModel inspection);
        void DeleteInspection(string id);

        /***************************************************************************
         * 
         * FAULT RECORDS
         * 
         ***************************************************************************/

        /***   GETTERS   ***/

        FaultViewModel GetFaultById(string id);
        IEnumerable<FaultViewModel> GetFaultsByAircraftId(string aircraftId);

        /***   SETTERS   ***/

        void CreateFault(FaultViewModel fault);
        void UpdateFault(FaultViewModel fault);
        void DeleteFault(string id);

        /***************************************************************************
         * 
         * RELATED MAINTENANCE RECORDS
         * 
         ***************************************************************************/

        /***   GETTERS   ***/

        RelatedMaintenanceViewModel GetRelatedMaintenanceById(string id);
        IEnumerable<RelatedMaintenanceViewModel> GetRelatedMaintenanceByFaultId(string faultId);

        /***   SETTERS   ***/

        void CreateRelatedMaintenance(RelatedMaintenanceViewModel related);
        void UpdateRelatedMaintenance(RelatedMaintenanceViewModel related);
        void DeleteRelatedMaintenance(string id);
    }
}
