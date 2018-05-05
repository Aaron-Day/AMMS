using AMMS.Models;
using AMMS.Models.AccountViewModels;
using AMMS.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;

namespace AMMS.Repository
{
    public interface IMasterRepository
    {
        /***************************************************************************
         * 
         * REQUESTS
         * 
         ***************************************************************************/

        /***   GETTERS   ***/

        Request GetRequestById(string id);
        Request GetRequestByEmail(string email);
        IEnumerable<Request> GetRequestsByUic(string uic);
        IEnumerable<Request> GetAllRequests();

        /***   SETTERS   ***/

        void CreateRequest(Request request);
        void DeleteRequest(string id);

        /***************************************************************************
         * 
         * USERS
         * 
         ***************************************************************************/

        /***   GETTERS   ***/

        ApplicationUser GetCurrentUser(ClaimsPrincipal user);
        ApplicationUser GetUserById(string id);
        ApplicationUser GetUserByPid(string pid);
        ApplicationUser GetUserByEmail(string email);
        IEnumerable<ApplicationUser> GetUsersByRole(string role);
        IEnumerable<ApplicationUser> GetUsersByUic(string uic);
        IEnumerable<ApplicationUser> GetAllUsers();

        /***   SETTERS   ***/

        void CreateUser(ApplicationUser user);
        void UpdateUser(ApplicationUser user);
        void DeleteUser(string id);

        /***   MISC ACCOUNT   ***/

        SignInResult Login(LoginViewModel login);
        void Logout(string id);
        void ResetPassword(string id);
        void ChangePassword(ChangePasswordViewModel change);
        string GetType(string value);

        /***************************************************************************
         * 
         * ROLES
         * 
         ***************************************************************************/

        /***   GETTERS   ***/

        IdentityRole GetRoleById(string id);
        IdentityRole GetRoleByName(string name);
        IEnumerable<IdentityRole> GetAllRoles();

        /***   SETTERS   ***/

        void CreateRole(IdentityRole role);
        void UpdateRole(IdentityRole role);
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
        Unit GetUnitById(string id);
        Unit GetUnitByUic(string uic);
        IEnumerable<Unit> GetAllUnits();

        /***   SETTERS   ***/

        void CreateUnit(Unit unit);
        void UpdateUnit(Unit unit);
        void DeleteUnit(string id);

        /***************************************************************************
         * 
         * AIRCRAFT MODELS
         * 
         ***************************************************************************/

        /***   GETTERS   ***/

        AircraftModel GetAircraftModelById(string id);
        AircraftModel GetAircraftModelByMds(string mds);
        IEnumerable<AircraftModel> GetAircraftModelsByUIC(string uic);
        IEnumerable<AircraftModel> GetAllAircraftModels();

        /***   SETTERS   ***/

        void CreateAircraftModel(AircraftModel model);
        void UpdateAircraftModel(AircraftModel model);
        void DeleteAircraftModel(string id);

        /***************************************************************************
         * 
         * AIRCRAFT
         * 
         ***************************************************************************/

        /***   GETTERS   ***/

        Aircraft GetAircraftById(string id);
        IEnumerable<Aircraft> GetAircraftByUnitId(string id);
        IEnumerable<Aircraft> GetAircraftByUIC(string uic);
        IEnumerable<Aircraft> GetAllAircraft();

        /***   SETTERS   ***/

        void CreateAircraft(Aircraft aircraft);
        void UpdateAircraft(Aircraft aircraft);
        void DeleteAircraft(string id);

        /***************************************************************************
         * 
         * FLIGHT RECORDS
         * 
         ***************************************************************************/

        /***   GETTERS   ***/

        Flight GetFlightById(string id);
        IEnumerable<Flight> GetFlightsByAircraftId(string aircraftId);

        /***   SETTERS   ***/

        void CreateFlight(Flight flight);
        void UpdateFlight(Flight flight);
        void DeleteFlight(string id);

        /***************************************************************************
         * 
         * INSPECTION RECORDS
         * 
         ***************************************************************************/

        /***   GETTERS   ***/

        Inspection GetInspectionById(string id);
        IEnumerable<Inspection> GetInspectionsByAircraftId(string aircraftId);

        /***   SETTERS   ***/

        void CreateInspection(Inspection inspection);
        void UpdateInspection(Inspection inspection);
        void DeleteInspection(string id);

        /***************************************************************************
         * 
         * FAULT RECORDS
         * 
         ***************************************************************************/

        /***   GETTERS   ***/

        Fault GetFaultById(string id);
        IEnumerable<Fault> GetFaultsByAircraftId(string aircraftId);

        /***   SETTERS   ***/

        void CreateFault(Fault fault);
        void UpdateFault(Fault fault);
        void DeleteFault(string id);

        /***************************************************************************
         * 
         * RELATED MAINTENANCE RECORDS
         * 
         ***************************************************************************/

        /***   GETTERS   ***/

        RelatedMaintenance GetRelatedMaintenanceById(string id);
        IEnumerable<RelatedMaintenance> GetRelatedMaintenanceByFaultId(string faultId);

        /***   SETTERS   ***/

        void CreateRelatedMaintenance(RelatedMaintenance related);
        void UpdateRelatedMaintenance(RelatedMaintenance related);
        void DeleteRelatedMaintenance(string id);
    }
}
