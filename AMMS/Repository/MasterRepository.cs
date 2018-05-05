using AMMS.Data;
using AMMS.Models;
using AMMS.Models.AccountViewModels;
using AMMS.Models.ViewModels;
using AMMS.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace AMMS.Repository
{
    public class MasterRepository : IMasterRepository
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(MasterRepository));

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public MasterRepository(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        /***************************************************************************
         * 
         * REQUESTS
         * 
         ***************************************************************************/

        /***   GETTERS   ***/

        public Request GetRequestById(string id)
        {
            try
            {
                return _context.Requests.Find(id);
            }
            catch (Exception e)
            {
                Log.Error("GetRequestById: Failed!", e);
                throw;
            }
        }

        public Request GetRequestByEmail(string email)
        {
            try
            {
                return _context.Requests.SingleOrDefault(r => r.Email == email);
            }
            catch (Exception e)
            {
                Log.Error("GetRequestByEmail: Failed!", e);
                throw;
            }
        }

        public IEnumerable<Request> GetRequestsByUic(string uic)
        {
            try
            {
                return _context.Requests.Where(r => r.Unit == uic);
            }
            catch (Exception e)
            {
                Log.Error("GetRequestsByUic: Failed!", e);
                throw;
            }
        }

        public IEnumerable<Request> GetAllRequests()
        {
            try
            {
                return _context.Requests.ToList();
            }
            catch (Exception e)
            {
                Log.Error("GetAllRequests: Failed!", e);
                throw;
            }
        }

        /***   SETTERS   ***/

        public void CreateRequest(Request request)
        {
            try
            {
                _context.Requests.Add(request);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Log.Error("CreateRequest: Failed!", e);
                throw;
            }
        }

        public void DeleteRequest(string id)
        {
            try
            {
                var request = _context.Requests.Find(id);
                _context.Requests.Remove(request);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Log.Error("DeleteRequest: Failed!", e);
                throw;
            }
        }

        /***************************************************************************
         * 
         * USERS
         * 
         ***************************************************************************/

        /***   GETTERS   ***/

        public ApplicationUser GetCurrentUser(ClaimsPrincipal user)
        {
            try
            {
                var userId = _userManager.GetUserId(user);
                return _context.Users.Find(userId);
            }
            catch (Exception e)
            {
                Log.Error("GetCurrentUser: Failed!", e);
                throw;
            }
        }

        public ApplicationUser GetUserById(string id)
        {
            try
            {
                return _context.Users.Find(id);
            }
            catch (Exception e)
            {
                Log.Error("GetUserById: Failed!", e);
                throw;
            }
        }

        public ApplicationUser GetUserByPid(string pid)
        {
            try
            {
                return _context.Users.SingleOrDefault(u =>
                    u.FirstName.StartsWith(pid.Substring(0, 1)) &&
                    u.LastName.StartsWith(pid.Substring(1, 1)) &&
                    u.SocialSecurityNumber.EndsWith(pid.Substring(2, 4)));
            }
            catch (Exception e)
            {
                Log.Error("GetUserByPid: Failed!", e);
                throw;
            }
        }

        public ApplicationUser GetUserByEmail(string email)
        {
            try
            {
                return _context.Users.SingleOrDefault(u => u.Email == email);
            }
            catch (Exception e)
            {
                Log.Error("GetUserByEmail: Failed!", e);
                throw;
            }
        }

        public IEnumerable<ApplicationUser> GetUsersByRole(string role)
        {
            try
            {
                var list = new List<ApplicationUser>();
                var roleid = GetRoleByName(role).Id;
                foreach (var userrole in _context.UserRoles.Where(userrole => userrole.RoleId == roleid))
                {
                    var user = GetUserById(userrole.UserId);
                    list.Add(user);
                }
                return list;
            }
            catch (Exception e)
            {
                Log.Error("GetUsersByRole: Failed!", e);
                throw;
            }
        }

        public IEnumerable<ApplicationUser> GetUsersByUic(string uic)
        {
            try
            {
                return _context.Users.Where(u => u.AssignedUnit == uic).ToList();
            }
            catch (Exception e)
            {
                Log.Error("GetUsersByUic: Failed!", e);
                throw;
            }
        }

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            try
            {
                return _context.Users.ToList();
            }
            catch (Exception e)
            {
                Log.Error("GetAllUsers: Failed!", e);
                throw;
            }
        }

        /***   SETTERS   ***/

        public void CreateUser(ApplicationUser user)
        {
            try
            {
                _userManager.CreateAsync(user).Wait();
                _userManager.AddToRoleAsync(user, "CE").Wait();
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Log.Error("CreateUser: Failed!", e);
                throw;
            }
        }

        public void UpdateUser(ApplicationUser user)
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Log.Error("UpdateUser: Failed!", e);
                throw;
            }
        }

        public void DeleteUser(string id)
        {
            try
            {
                foreach (var userrole in _context.UserRoles.Where(x => x.UserId == id))
                {
                    _context.UserRoles.Remove(userrole);
                }
                foreach (var claim in _context.UserClaims.Where(x => x.UserId == id))
                {
                    _context.UserClaims.Remove(claim);
                }
                foreach (var login in _context.UserLogins.Where(x => x.UserId == id))
                {
                    _context.UserLogins.Remove(login);
                }
                foreach (var tokin in _context.UserTokens.Where(x => x.UserId == id))
                {
                    _context.UserTokens.Remove(tokin);
                }
                _userManager.DeleteAsync(GetUserById(id)).Wait();
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Log.Error("DeleteUser: Failed!", e);
                throw;
            }
        }

        /***   MISC ACCOUNT   ***/

        public SignInResult Login(LoginViewModel login)
        {
            try
            {
                var email = login.Email;
                var pass = login.Password;
                var salt = GetUserByEmail(email).Salt;
                var hash = PasswordProtocol.CalculateHash(pass, salt);
                var result = _signInManager.PasswordSignInAsync(email.ToUpper(), hash, false, true).Result;
                if (result == SignInResult.Success)
                    GetUserByEmail(email).LastActive = Formatting.AsMilDateTime(DateTime.UtcNow);
                return result;
            }
            catch (Exception e)
            {
                Log.Error("Login: Failed!", e);
                throw;
            }
        }

        public void Logout(string id)
        {
            try
            {
                var user = GetUserById(id);
                _userManager.UpdateSecurityStampAsync(user).Wait();
                _signInManager.SignOutAsync().Wait();
            }
            catch (Exception e)
            {
                Log.Error("Logout: Failed!", e);
                throw;
            }
        }

        public void ResetPassword(string id)
        {
            try
            {
                var user = GetUserById(id);
                var change = new ChangePasswordViewModel
                {
                    Id = id,
                    NewPassword = user.Email
                };
                ChangePassword(change);
            }
            catch (Exception e)
            {
                Log.Error("ResetPassword: Failed!", e);
                throw;
            }
        }

        public void ChangePassword(ChangePasswordViewModel change)
        {
            try
            {
                var user = GetUserById(change.Id);
                var hash = PasswordProtocol.CalculateHash(change.NewPassword, user.Salt);
                var tokin = _userManager.GeneratePasswordResetTokenAsync(user).Result;
                _userManager.ResetPasswordAsync(user, tokin, hash).Wait();
            }
            catch (Exception e)
            {
                Log.Error("ChangePassword: Failed!", e);
                throw;
            }
        }

        public string GetType(string value)
        {
            if (value == null || value == "ADMIN")
                return "AllUsers";
            if (_context.Users.Count(u => u.Id == value) > 0)
                return "UserId";
            if (_context.Users.Count(u => u.Email == value) > 0)
                return "UserEmail";
            if (_context.Units.Count(u => u.Id == value) > 0)
                return "UnitId";
            if (_context.Units.Count(u => u.UIC == value) > 0)
                return "UnitUIC";
            if (_context.Roles.Count(r => r.Id == value) > 0)
                return "RoleId";
            if (_context.Roles.Count(r => r.Name == value) > 0)
                return "RoleName";

            return string.Empty;
        }

        /***************************************************************************
         * 
         * ROLES
         * 
         ***************************************************************************/

        /***   GETTERS   ***/

        public IdentityRole GetRoleById(string id)
        {
            try
            {
                return _context.Roles.Find(id);
            }
            catch (Exception e)
            {
                Log.Error("GetRoleById: Failed!", e);
                throw;
            }
        }

        public IdentityRole GetRoleByName(string name)
        {
            try
            {
                return _context.Roles.SingleOrDefault(r => r.Name == name);
            }
            catch (Exception e)
            {
                Log.Error("GetRoleByName: Failed!", e);
                throw;
            }
        }

        public IEnumerable<IdentityRole> GetAllRoles()
        {
            try
            {
                return _context.Roles.ToList();
            }
            catch (Exception e)
            {
                Log.Error("GetAllRoles: Failed!", e);
                throw;
            }
        }

        /***   SETTERS   ***/

        public void CreateRole(IdentityRole role)
        {
            try
            {
                _roleManager.CreateAsync(role).Wait();
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Log.Error("CreateRole: Failed!", e);
                throw;
            }
        }

        public void UpdateRole(IdentityRole role)
        {
            try
            {
                _roleManager.UpdateAsync(role).Wait();
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Log.Error("UpdateRole: Failed!", e);
                throw;
            }
        }

        public void DeleteRole(string id)
        {
            try
            {
                foreach (var userrole in _context.UserRoles.Where(ur => ur.RoleId == id))
                {
                    _context.UserRoles.Remove(userrole);
                }
                var role = _context.Roles.Find(id);
                _roleManager.DeleteAsync(role).Wait();
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Log.Error("DeleteRole: Failed!", e);
                throw;
            }
        }

        /***************************************************************************
         * 
         * USER ROLES
         * 
         ***************************************************************************/

        /***   GETTERS   ***/

        public IList<UserRolesViewModel> GetUserRoles(string id)
        {
            try
            {
                var roles = _context.Roles.ToList();
                var userroles = _context.UserRoles.ToList();
                return roles.Select(role => new UserRolesViewModel
                {
                    UserId = id,
                    RoleId = role.Id,
                    RoleName = role.Name,
                    Assigned = userroles.SingleOrDefault(u => u.UserId == id && u.RoleId == role.Id) != null
                }).ToList();
            }
            catch (Exception e)
            {
                Log.Error("GetUserRoles: Failed!", e);
                throw;
            }
        }

        /***   SETTERS   ***/

        public void UpdateUserRoles(IList<UserRolesViewModel> assignments)
        {
            try
            {
                var userRoles = _context.UserRoles.ToList();

                foreach (var assignment in assignments)
                {
                    if (userRoles.SingleOrDefault(u => u.UserId == assignment.UserId && u.RoleId == assignment.RoleId) ==
                        null)
                    {
                        if (!assignment.Assigned) continue;
                        /*ADD TO USER ROLES*/
                        var user = GetUserById(assignment.UserId);
                        var role = assignment.RoleName.ToUpper();
                        _userManager.AddToRoleAsync(user, role).Wait();
                    }
                    else
                    {
                        if (assignment.Assigned) continue;
                        /*REMOVE FROM USER ROLES*/
                        var user = GetUserById(assignment.UserId);
                        var role = assignment.RoleName.ToUpper();
                        _userManager.RemoveFromRoleAsync(user, role).Wait();
                    }
                }
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Log.Error("UpdateUserRoles: Failed!", e);
                throw;
            }
        }

        /***************************************************************************
         * 
         * UNITS
         * 
         ***************************************************************************/

        /***   GETTERS   ***/

        public string GetUnitId(string id)
        {
            // accepts unit, user or acft id
            try
            {
                if (_context.Units.Find(id) != null) return id;
                var acft = _context.Aircraft.Find(id);
                if (acft != null) return acft.UnitId;
                var user = _context.Users.Find(id);
                return user != null
                    ? _context.Units.SingleOrDefault(u => u.UIC == user.AssignedUnit)?.Id
                    : string.Empty;
            }
            catch (Exception e)
            {
                Log.Error("GetUnitId: Failed!", e);
                throw;
            }
        }

        public Unit GetUnitById(string id)
        {
            try
            {
                return _context.Units.Find(id);
            }
            catch (Exception e)
            {
                Log.Error("GetUnitById: Failed!", e);
                throw;
            }
        }

        public Unit GetUnitByUic(string uic)
        {
            try
            {
                return _context.Units.SingleOrDefault(u => u.UIC == uic);
            }
            catch (Exception e)
            {
                Log.Error("GetUnitByUic: Failed!", e);
                throw;
            }
        }

        public IEnumerable<Unit> GetAllUnits()
        {
            try
            {
                return _context.Units.ToList();
            }
            catch (Exception e)
            {
                Log.Error("GetAllUnits: Failed!", e);
                throw;
            }
        }

        /***   SETTERS   ***/

        public void CreateUnit(Unit unit)
        {
            try
            {
                _context.Units.Add(unit);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Log.Error("CreateUnit: Failed!", e);
                throw;
            }
        }

        public void UpdateUnit(Unit unit)
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Log.Error("UpdateUnit: Failed!", e);
                throw;
            }
        }

        public void DeleteUnit(string id)
        {
            try
            {
                var unit = _context.Units.Find(id);
                _context.Units.Remove(unit);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Log.Error("DeleteUnit: Failed!", e);
                throw;
            }
        }

        /***************************************************************************
         * 
         * AIRCRAFT MODELS
         * 
         ***************************************************************************/

        /***   GETTERS   ***/

        public AircraftModel GetAircraftModelById(string id)
        {
            try
            {
                return _context.AircraftModels.Find(id);
            }
            catch (Exception e)
            {
                Log.Error("GetAircraftModelById: Failed!", e);
                throw;
            }
        }

        public AircraftModel GetAircraftModelByMds(string mds)
        {
            try
            {
                return _context.AircraftModels.FirstOrDefault(m => m.Mds == mds);
            }
            catch (Exception e)
            {
                Log.Error("GetAircraftModelByMds: Failed!", e);
                throw;
            }
        }

        public IEnumerable<AircraftModel> GetAircraftModelsByUIC(string uic)
        {
            try
            {
                if (uic == null || uic == "ADMIN") return GetAllAircraftModels();

                var unitId = _context.Units.FirstOrDefault(u => u.UIC == uic)?.Id;
                var models = _context.Aircraft.Where(a => a.UnitId == unitId).GroupBy(x => x.AircraftModelId).ToList();
                return models.Select(acft => _context.AircraftModels.Find(acft)).ToList();
            }
            catch (Exception e)
            {
                Log.Error("GetAircraftModelsByUIC: Failed!", e);
                throw;
            }
        }

        public IEnumerable<AircraftModel> GetAllAircraftModels()
        {
            try
            {
                return _context.AircraftModels.ToList();
            }
            catch (Exception e)
            {
                Log.Error("GetAllAircraftModels: Failed!", e);
                throw;
            }
        }

        /***   SETTERS   ***/

        public void CreateAircraftModel(AircraftModel model)
        {
            _context.AircraftModels.Add(model);
            _context.SaveChanges();
        }

        public void UpdateAircraftModel(AircraftModel model)
        {
            _context.SaveChanges();
        }

        public void DeleteAircraftModel(string id)
        {
            var model = _context.AircraftModels.Find(id);

            if (model == null) return;

            _context.AircraftModels.Remove(model);
            _context.SaveChanges();
        }

        /***************************************************************************
         * 
         * AIRCRAFT
         * 
         ***************************************************************************/

        /***   GETTERS   ***/

        public Aircraft GetAircraftById(string id)
        {
            try
            {
                return _context.Aircraft.Find(id);
            }
            catch (Exception e)
            {
                Log.Error("GetAircraftById: Failed!", e);
                throw;
            }
        }

        public IEnumerable<Aircraft> GetAircraftByUnitId(string id)
        {
            try
            {
                return _context.Aircraft.Where(m => m.UnitId == id).ToList();
            }
            catch (Exception e)
            {
                Log.Error("GetAircraftByUnitId: Failed!", e);
                throw;
            }
        }

        public IEnumerable<Aircraft> GetAircraftByUIC(string uic)
        {
            try
            {
                var unitId = _context.Units.FirstOrDefault(u => u.UIC == uic)?.Id;
                return uic == "ADMIN"
                    ? GetAllAircraft()
                    : _context.Aircraft.Where(m => m.UnitId == unitId).ToList();
            }
            catch (Exception e)
            {
                Log.Error("GetAircraftByUIC: Failed!", e);
                throw;
            }
        }

        public IEnumerable<Aircraft> GetAllAircraft()
        {
            try
            {
                return _context.Aircraft.ToList();
            }
            catch (Exception e)
            {
                Log.Error("GetAllAircraft: Failed!", e);
                throw;
            }
        }

        /***   SETTERS   ***/

        public void CreateAircraft(Aircraft aircraft)
        {
            _context.Aircraft.Add(aircraft);
            _context.SaveChanges();
        }

        public void UpdateAircraft(Aircraft aircraft)
        {
            _context.SaveChanges();
        }

        public void DeleteAircraft(string id)
        {
            var aircraft = _context.Aircraft.Find(id);

            if (aircraft == null) return;

            _context.Aircraft.Remove(aircraft);
            _context.SaveChanges();
        }

        /***************************************************************************
         * 
         * FLIGHT RECORDS
         * 
         ***************************************************************************/

        /***   GETTERS   ***/

        public Flight GetFlightById(string id)
        {
            return _context.Flights.Find(id);
        }

        public IEnumerable<Flight> GetFlightsByAircraftId(string aircraftId)
        {
            return _context.Flights.Where(flt => flt.AircraftId == aircraftId);
        }

        /***   SETTERS   ***/

        public void CreateFlight(Flight flight)
        {
            _context.Flights.Add(flight);
            _context.SaveChanges();
        }

        public void UpdateFlight(Flight flight)
        {
            _context.SaveChanges();
        }

        public void DeleteFlight(string id)
        {
            var flight = _context.Flights.Find(id);

            if (flight == null) return;

            _context.Flights.Remove(flight);
            _context.SaveChanges();
        }

        /***************************************************************************
         * 
         * INSPECTION RECORDS
         * 
         ***************************************************************************/

        /***   GETTERS   ***/

        public Inspection GetInspectionById(string id)
        {
            return _context.Inspections.Find(id);
        }

        public IEnumerable<Inspection> GetInspectionsByAircraftId(string id)
        {
            return _context.Inspections.Where(insp => insp.AircraftId == id && insp.CompletedAt == null).ToList();
        }

        /***   SETTERS   ***/

        public void CreateInspection(Inspection inspection)
        {
            _context.Inspections.Add(inspection);
            _context.SaveChanges();
        }

        public void UpdateInspection(Inspection inspection)
        {
            _context.SaveChanges();
        }

        public void DeleteInspection(string id)
        {
            var insp = _context.Inspections.Find(id);

            if (insp == null) return;

            _context.Inspections.Remove(insp);
            _context.SaveChanges();
        }

        /***************************************************************************
         * 
         * FAULT RECORDS
         * 
         ***************************************************************************/

        /***   GETTERS   ***/

        public Fault GetFaultById(string id)
        {
            return _context.Faults.Find(id);
        }

        public IEnumerable<Fault> GetFaultsByAircraftId(string id)
        {
            return _context.Faults.Where(fault => fault.AircraftId == id);
        }

        /***   SETTERS   ***/

        public void CreateFault(Fault fault)
        {
            _context.Faults.Add(fault);
            _context.SaveChanges();
        }

        public void UpdateFault(Fault fault)
        {
            _context.SaveChanges();
        }

        public void DeleteFault(string id)
        {
            var f = _context.Faults.Find(id);

            if (f == null) return;

            _context.Faults.Remove(f);
            _context.SaveChanges();
        }

        /***************************************************************************
         * 
         * RELATED MAINTENANCE RECORDS
         * 
         ***************************************************************************/

        /***   GETTERS   ***/

        public RelatedMaintenance GetRelatedMaintenanceById(string id)
        {
            return _context.RelatedMaintenance.Find(id);
        }

        public IEnumerable<RelatedMaintenance> GetRelatedMaintenanceByFaultId(string id)
        {
            return _context.RelatedMaintenance.Where(maint => maint.FaultId == id);
        }

        /***   SETTERS   ***/

        public void CreateRelatedMaintenance(RelatedMaintenance related)
        {
            _context.RelatedMaintenance.Add(related);
            _context.SaveChanges();
        }

        public void UpdateRelatedMaintenance(RelatedMaintenance related)
        {
            _context.SaveChanges();
        }

        public void DeleteRelatedMaintenance(string id)
        {
            var related = _context.RelatedMaintenance.Find(id);

            if (related == null) return;

            _context.RelatedMaintenance.Remove(related);
            _context.SaveChanges();
        }
    }
}
