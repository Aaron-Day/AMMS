using AMMS.Models;
using AMMS.Models.AccountViewModels;
using AMMS.Models.ViewModels;
using AMMS.Repository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;

namespace AMMS.Services
{
    public class MasterService : IMasterService
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(MasterService));

        private readonly IMasterRepository _repository;

        public MasterService(IMasterRepository repository)
        {
            _repository = repository;
        }

        /***************************************************************************
         * 
         * REQUESTS
         * 
         ***************************************************************************/

        /***   GETTERS   ***/

        public RequestViewModel GetRequestById(string id)
        {
            try
            {
                var request = _repository.GetRequestById(id);
                return MapToRequestViewModel(request);
            }
            catch (Exception e)
            {
                Log.Error("GetRequestById: Failed!", e);
                throw;
            }
        }

        public RequestViewModel GetRequestByEmail(string email)
        {
            try
            {
                var request = _repository.GetRequestByEmail(email);
                return MapToRequestViewModel(request);
            }
            catch (Exception e)
            {
                Log.Error("GetRequestByEmail: Failed!", e);
                throw;
            }
        }

        public IEnumerable<RequestViewModel> GetRequestsByUic(string uic)
        {
            try
            {
                var requests = _repository.GetRequestsByUic(uic);
                return requests.Select(MapToRequestViewModel).ToList().OrderBy(r => r.Requested);
            }
            catch (Exception e)
            {
                Log.Error("GetRequestsByUic: Failed!", e);
                throw;
            }
        }

        public IEnumerable<RequestViewModel> GetAllRequests()
        {
            try
            {
                var requests = _repository.GetAllRequests();
                return requests.Select(MapToRequestViewModel).ToList().OrderBy(r => r.Requested);
            }
            catch (Exception e)
            {
                Log.Error("GetAllRequests: Failed!", e);
                throw;
            }
        }

        /***   SETTERS   ***/

        public void CreateRequest(RequestViewModel request)
        {
            try
            {
                _repository.CreateRequest(MapToRequest(request));
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
                _repository.DeleteRequest(id);
            }
            catch (Exception e)
            {
                Log.Error("DeleteRequest: Failed!", e);
                throw;
            }
        }

        /***   HELPERS   ***/

        private RequestViewModel MapToRequestViewModel(Request request)
        {
            if (request == null) return null;
            return new RequestViewModel
            {
                Id = request.Id,
                Requested = request.Requested,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Password = request.Password,
                ConfirmPassword = request.Password,
                Unit = request.Unit
            };
        }

        private Request MapToRequest(RequestViewModel request)
        {
            if (request == null) return null;
            return new Request
            {
                Id = request.Id,
                Requested = request.Requested,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Password = request.Password,
                Unit = request.Unit
            };
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
                return _repository.GetCurrentUser(user);
            }
            catch (Exception e)
            {
                Log.Error("GetCurrentUser: Failed!", e);
                throw;
            }
        }

        public RegisterViewModel GetUserById(string id)
        {
            try
            {
                var user = _repository.GetUserById(id);
                return MapToRegisterViewModel(user);
            }
            catch (Exception e)
            {
                Log.Error("GetUserById: Failed!", e);
                throw;
            }
        }

        public RegisterViewModel GetUserByPid(string pid)
        {
            try
            {
                var user = _repository.GetUserByPid(pid);
                return MapToRegisterViewModel(user);
            }
            catch (Exception e)
            {
                Log.Error("GetUserByPid: Failed!", e);
                throw;
            }
        }

        public RegisterViewModel GetUserByEmail(string email)
        {
            try
            {
                var user = _repository.GetUserByEmail(email);
                return MapToRegisterViewModel(user);
            }
            catch (Exception e)
            {
                Log.Error("GetUserByEmail: Failed!", e);
                throw;
            }
        }

        public IEnumerable<RegisterViewModel> GetUsersByRole(string role)
        {
            try
            {
                var users = _repository.GetUsersByRole(role);
                return users.Select(MapToRegisterViewModel).ToList().OrderBy(u => u.LastName);
            }
            catch (Exception e)
            {
                Log.Error("GetUsersByRole: Failed!", e);
                throw;
            }
        }

        public IEnumerable<RegisterViewModel> GetUsersByUic(string uic)
        {
            try
            {
                var users = _repository.GetUsersByUic(uic);
                return users.Select(MapToRegisterViewModel).ToList().OrderBy(u => u.LastName);
            }
            catch (Exception e)
            {
                Log.Error("GetUsersByUic: Failed!", e);
                throw;
            }
        }

        public IEnumerable<RegisterViewModel> GetAllUsers()
        {
            try
            {
                var users = _repository.GetAllUsers();
                return users.Select(MapToRegisterViewModel).ToList().OrderBy(u => u.LastName);
            }
            catch (Exception e)
            {
                Log.Error("GetAllUsers: Failed!", e);
                throw;
            }
        }

        public IEnumerable<RegisterViewModel> GetUsers(string value, ClaimsPrincipal user)
        {
            var current = GetCurrentUser(user);
            var admin = current.AssignedUnit == "ADMIN";

            var showAll = admin && (current.Id == value || current.Email == value || current.AssignedUnit == value);

            if (showAll)
                return GetAllUsers();

            switch (_repository.GetType(value))
            {
                case "AllUsers":
                    if (admin)
                        return GetAllUsers();
                    return GetUsersByUic(current.AssignedUnit);
                case "UserId":
                    if (GetUserById(value).AssignedUnit == current.AssignedUnit)
                        return GetUsersByUic(current.AssignedUnit);
                    break;
                case "UserEmail":
                    if (GetUserByEmail(value).AssignedUnit == current.AssignedUnit)
                        return GetUsersByUic(current.AssignedUnit);
                    break;
                case "UnitId":
                    if (GetUnitById(value).UIC == current.AssignedUnit)
                        return GetUsersByUic(current.AssignedUnit);
                    break;
                case "UnitUIC":
                    if (value == current.AssignedUnit)
                        return GetUsersByUic(current.AssignedUnit);
                    break;
                case "RoleId":
                    if (admin)
                        return GetUsersByRole(GetRoleById(value).RoleName);
                    return GetUsersByRole(GetRoleById(value).RoleName).Where(u => u.AssignedUnit == current.AssignedUnit);
                case "RoleName":
                    if (admin)
                        return GetUsersByRole(value);
                    return GetUsersByRole(value).Where(u => u.AssignedUnit == current.AssignedUnit);
                default:
                    return new List<RegisterViewModel>();
            }
            return new List<RegisterViewModel>();
        }

        /***   SETTERS   ***/

        public void CreateUser(RegisterViewModel user)
        {
            try
            {
                _repository.CreateUser(MapToUser(user));
            }
            catch (Exception e)
            {
                Log.Error("CreateUser: Failed!", e);
                throw;
            }
        }

        public void UpdateUser(RegisterViewModel user)
        {
            try
            {
                var update = _repository.GetUserById(user.Id);
                CopyToUser(user, update);
                _repository.UpdateUser(update);
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
                _repository.DeleteUser(id);
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
                return _repository.Login(login);
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
                _repository.Logout(id);
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
                _repository.ResetPassword(id);
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
                _repository.ChangePassword(change);
            }
            catch (Exception e)
            {
                Log.Error("ChangePassword: Failed!", e);
                throw;
            }
        }

        public string GetType(string value, ClaimsPrincipal user)
        {
            var current = GetCurrentUser(user);
            var admin = current.AssignedUnit == "ADMIN";

            var showAll = admin && (current.Id == value || current.Email == value || current.AssignedUnit == value);

            if (showAll)
                return "All Users";

            switch (_repository.GetType(value))
            {
                case "AllUsers":
                    if (admin)
                        return "All Users";
                    return $"By UIC: {current.AssignedUnit}";
                case "UserId":
                    if (GetUserById(value).AssignedUnit == current.AssignedUnit)
                        return $"By UIC: {current.AssignedUnit}";
                    break;
                case "UserEmail":
                    if (GetUserByEmail(value).AssignedUnit == current.AssignedUnit)
                        return $"By UIC: {current.AssignedUnit}";
                    break;
                case "UnitId":
                    if (GetUnitById(value).UIC == current.AssignedUnit)
                        return $"By UIC: {current.AssignedUnit}";
                    break;
                case "UnitUIC":
                    if (value == current.AssignedUnit)
                        return $"By UIC: {current.AssignedUnit}";
                    break;
                case "RoleId":
                    if (admin)
                        return $"By role: {GetRoleById(value).RoleName}";
                    return $"By role: {GetRoleById(value).RoleName} and UIC: {current.AssignedUnit}";
                case "RoleName":
                    if (admin)
                        return $"By role: {value}";
                    return $"By role: {value} and UIC: {current.AssignedUnit}";
                default:
                    return "";
            }

            return "";
        }

        /***   HELPERS   ***/

        private static RegisterViewModel MapToRegisterViewModel(ApplicationUser user)
        {
            if (user == null) return null;
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

        private ApplicationUser MapToUser(RegisterViewModel user)
        {
            if (user == null) return null;
            var update = _repository.GetUserById(user.Id);
            if (update != null)
            {
                CopyToUser(user, update);
                return update;
            }
            update = new ApplicationUser
            {
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Email = user.Email.ToLower(),
                NormalizedEmail = user.Email?.ToUpper(),
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumber != null,
                SocialSecurityNumber = user.SocialSecurityNumber,
                Rank = user.Rank,
                DateOfBirth = user.DateOfBirth,
                UserName = user.Email.ToLower(),
                NormalizedUserName = user.Email?.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString("D"),
                FullName = (user.Rank == null ? "" : $"{user.Rank} ") +
                           $"{user.LastName}, {user.FirstName}",
                AssignedUnit = user.AssignedUnit
            };
            var password = new PasswordHasher<ApplicationUser>();
            var hashed = password.HashPassword(update, PasswordProtocol.CalculateHash(user.Password, update.Salt));
            update.PasswordHash = hashed;
            if (update.DateOfBirth != null && update.DateOfBirth.Contains("-"))
            {
                update.DateOfBirth = DateTime.ParseExact(update.DateOfBirth, "yyyy'-'MM'-'dd", CultureInfo.InvariantCulture)
                    .ToString("dd' 'MMM' 'yy")?.ToUpper();
            }

            return update;
        }

        private static void CopyToUser(RegisterViewModel user, ApplicationUser update)
        {
            if (user.Id != update.Id) return;
            update.Rank = user.Rank;
            update.FirstName = user.FirstName;
            update.MiddleName = user.MiddleName;
            update.LastName = user.LastName;
            update.Email = user.Email;
            update.PhoneNumber = user.PhoneNumber;
            update.DateOfBirth = user.DateOfBirth;
            update.SocialSecurityNumber = user.SocialSecurityNumber;
            update.AssignedUnit = user.AssignedUnit;
            if (update.DateOfBirth != null && update.DateOfBirth.Contains("-"))
            {
                update.DateOfBirth = DateTime.ParseExact(update.DateOfBirth, "yyyy'-'MM'-'dd", CultureInfo.InvariantCulture)
                    .ToString("dd' 'MMM' 'yy")?.ToUpper();
            }
        }

        /***************************************************************************
         * 
         * ROLES
         * 
         ***************************************************************************/

        /***   GETTERS   ***/

        public RoleListViewModel GetRoleById(string id)
        {
            try
            {
                var role = _repository.GetRoleById(id);
                return MapToRoleListViewModel(role);
            }
            catch (Exception e)
            {
                Log.Error("GetRoleById: Failed!", e);
                throw;
            }
        }

        public RoleListViewModel GetRoleByName(string name)
        {
            try
            {
                var role = _repository.GetRoleByName(name);
                return MapToRoleListViewModel(role);
            }
            catch (Exception e)
            {
                Log.Error("GetRoleByName: Failed!", e);
                throw;
            }
        }

        public IEnumerable<RoleListViewModel> GetAllRoles()
        {
            try
            {
                var roles = _repository.GetAllRoles();
                return roles.Select(MapToRoleListViewModel).ToList().OrderBy(r => r.RoleName);
            }
            catch (Exception e)
            {
                Log.Error("GetAllRoles: Failed!", e);
                throw;
            }
        }

        /***   SETTERS   ***/

        public void CreateRole(RoleListViewModel role)
        {
            try
            {
                _repository.CreateRole(MapToRole(role));
            }
            catch (Exception e)
            {
                Log.Error("CreateRole: Failed!", e);
                throw;
            }
        }

        public void UpdateRole(RoleListViewModel role)
        {
            try
            {
                // Do not update admin role
                if (role.RoleName == "Admin")
                    return;
                var update = _repository.GetRoleById(role.Id);
                CopyToRole(role, update);
                _repository.UpdateRole(update);
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
                // Do not delete admin role
                if (id == GetRoleByName("Admin").Id)
                    return;
                _repository.DeleteRole(id);
            }
            catch (Exception e)
            {
                Log.Error("DeleteRole: Failed!", e);
                throw;
            }
        }

        /***   HELPERS   ***/

        private RoleListViewModel MapToRoleListViewModel(IdentityRole role)
        {
            if (role == null) return null;
            var view = new RoleListViewModel
            {
                Id = role.Id,
                RoleName = role.Name,
                // TODO: TEST
                NumberOfUsers = GetUsersByRole(role.Name).Count()
            };
            if (view.RoleName == "Admin")
                --view.NumberOfUsers;
            return view;
        }

        private static IdentityRole MapToRole(RoleListViewModel role)
        {
            if (role == null) return null;
            return new IdentityRole
            {
                Id = role.Id,
                Name = role.RoleName,
                NormalizedName = role.RoleName?.ToUpper()
            };
        }

        private static void CopyToRole(RoleListViewModel role, IdentityRole update)
        {
            if (role.Id != update.Id) return;
            update.Name = role.RoleName;
            update.NormalizedName = role.RoleName?.ToUpper();
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
                return _repository.GetUserRoles(id);
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
                // Do not update admin user
                if (assignments.FirstOrDefault().UserId == GetUserByEmail("admin@us.army.mil").Id)
                    return;
                _repository.UpdateUserRoles(assignments);
            }
            catch (Exception e)
            {
                Log.Error("UpdateUserRoles: Failed!", e);
                throw;
            }
        }

        /***   HELPERS   ***/

        /***************************************************************************
         * 
         * UNITS
         * 
         ***************************************************************************/

        /***   GETTERS   ***/

        public string GetUnitId(string id)
        {
            try
            {
                return _repository.GetUnitId(id);
            }
            catch (Exception e)
            {
                Log.Error("GetUnitId: Failed!", e);
                throw;
            }
        }

        public UnitViewModel GetUnitById(string id)
        {
            try
            {
                return MapToUnitViewModel(_repository.GetUnitById(id));
            }
            catch (Exception e)
            {
                Log.Error("GetUnitById: Failed!", e);
                throw;
            }
        }

        public UnitViewModel GetUnitByUic(string uic)
        {
            try
            {
                var unit = _repository.GetUnitByUic(uic);
                return MapToUnitViewModel(unit);
            }
            catch (Exception e)
            {
                Log.Error("GetUnitByUic: Failed!", e);
                throw;
            }
        }

        public IEnumerable<UnitViewModel> GetAllUnits()
        {
            try
            {
                var units = _repository.GetAllUnits();
                return units.Select(MapToUnitViewModel).ToList().OrderBy(u => u.UIC);
            }
            catch (Exception e)
            {
                Log.Error("GetAllUnits: Failed!", e);
                throw;
            }
        }

        /***   SETTERS   ***/

        public void CreateUnit(UnitViewModel unit)
        {
            try
            {
                _repository.CreateUnit(MapToUnit(unit));
            }
            catch (Exception e)
            {
                Log.Error("CreateUnit: Failed!", e);
                throw;
            }
        }

        public void UpdateUnit(UnitViewModel unit)
        {
            try
            {
                var update = _repository.GetUnitById(unit.Id);
                CopyToUnit(unit, update);
                _repository.UpdateUnit(update);
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
                _repository.DeleteUnit(id);
            }
            catch (Exception e)
            {
                Log.Error("DeleteUnit: Failed!", e);
                throw;
            }
        }

        /***   HELPERS   ***/

        private static Unit MapToUnit(UnitViewModel unit)
        {
            if (unit == null) return null;
            return new Unit
            {
                Id = unit.Id,
                CompanyName = unit.CompanyName,
                Station = unit.Station,
                UIC = unit.UIC,
                UnitName = unit.UnitName,
                UnitPhone = unit.UnitPhone
            };
        }

        private static UnitViewModel MapToUnitViewModel(Unit unit)
        {
            return new UnitViewModel
            {
                Id = unit.Id,
                UIC = unit.UIC,
                UnitName = unit.UnitName,
                CompanyName = unit.CompanyName,
                UnitPhone = unit.UnitPhone,
                Station = unit.Station
            };
        }

        private static void CopyToUnit(UnitViewModel unit, Unit update)
        {
            if (unit.Id != update.Id) return;
            update.CompanyName = unit.CompanyName;
            update.Station = unit.Station;
            update.UIC = unit.UIC;
            update.UnitName = unit.UnitName;
            update.UnitPhone = unit.UnitPhone;
        }

        /***************************************************************************
         * 
         * AIRCRAFT MODELS
         * 
         ***************************************************************************/

        /***   GETTERS   ***/

        public AircraftModelViewModel GetAircraftModelById(string id)
        {
            try
            {
                return MapToAircraftModelViewModel(_repository.GetAircraftModelById(id));
            }
            catch (Exception e)
            {
                Log.Error("GetAircraftModelById: Failed!", e);
                throw;
            }
        }

        public AircraftModelViewModel GetAircraftModelByMds(string mds)
        {
            try
            {
                return MapToAircraftModelViewModel(_repository.GetAircraftModelByMds(mds));
            }
            catch (Exception e)
            {
                Log.Error("GetAircraftModelByMds: Failed!", e);
                throw;
            }
        }

        public IEnumerable<AircraftModelViewModel> GetAircraftModelsByUIC(string uic)
        {
            try
            {
                var models = _repository.GetAircraftModelsByUIC(uic);
                return models.Select(MapToAircraftModelViewModel).ToList().OrderBy(m => m.Mds);
            }
            catch (Exception e)
            {
                Log.Error("GetAircraftModelsByUIC: Failed!", e);
                throw;
            }
        }

        public IEnumerable<AircraftModelViewModel> GetAllAircraftModels()
        {
            try
            {
                var models = _repository.GetAllAircraftModels();
                return models.Select(MapToAircraftModelViewModel).ToList().OrderBy(m => m.Mds);
            }
            catch (Exception e)
            {
                Log.Error("GetAllAircraftModels: Failed!", e);
                throw;
            }
        }

        /***   SETTERS   ***/

        public void CreateAircraftModel(AircraftModelViewModel model)
        {
            try
            {
                _repository.CreateAircraftModel(MapToAircraftModel(model));
            }
            catch (Exception e)
            {
                Log.Error("CreateAircraftModel: Failed!", e);
                throw;
            }
        }

        public void UpdateAircraftModel(AircraftModelViewModel model)
        {
            try
            {
                var mod = _repository.GetAircraftModelById(model.Id);
                CopyToAircraftModel(model, mod);

                _repository.UpdateAircraftModel(mod);
            }
            catch (Exception e)
            {
                Log.Error("UpdateAircraftModel: Failed!", e);
                throw;
            }
        }

        public void DeleteAircraftModel(string id)
        {
            try
            {
                _repository.DeleteAircraftModel(id);
            }
            catch (Exception e)
            {
                Log.Error("DeleteAircraftModel: Failed!", e);
                throw;
            }
        }

        /***   HELPERS   ***/

        private static AircraftModelViewModel MapToAircraftModelViewModel(AircraftModel model)
        {
            return new AircraftModelViewModel
            {
                Id = model.Id,
                Eic = model.Eic,
                Mds = model.Mds,
                Name = model.Name,
                Nsn = model.Nsn
            };
        }

        private static AircraftModel MapToAircraftModel(AircraftModelViewModel model)
        {
            return new AircraftModel
            {
                Id = model.Id,
                Eic = model.Eic,
                Mds = model.Mds,
                Name = model.Name,
                Nsn = model.Nsn
            };
        }

        private static void CopyToAircraftModel(AircraftModelViewModel view, AircraftModel model)
        {
            model.Id = view.Id;
            model.Eic = view.Eic;
            model.Mds = view.Mds;
            model.Name = view.Name;
            model.Nsn = view.Nsn;
        }

        /***************************************************************************
         * 
         * AIRCRAFT
         * 
         ***************************************************************************/

        /***   GETTERS   ***/

        public AircraftViewModel GetAircraftById(string id)
        {
            try
            {
                return MapToAircraftViewModel(_repository.GetAircraftById(id));
            }
            catch (Exception e)
            {
                Log.Error("GetAircraftById: Failed!", e);
                throw;
            }
        }

        public IEnumerable<AircraftViewModel> GetAircraftByUnitId(string id)
        {
            try
            {
                var aircraft = _repository.GetAircraftByUnitId(id);
                return aircraft.Select(MapToAircraftViewModel).ToList().OrderBy(a => a.SerialNumber);
            }
            catch (Exception e)
            {
                Log.Error("GetAircraftByUnitId: Failed!", e);
                throw;
            }
        }

        public IEnumerable<AircraftViewModel> GetAircraftByUIC(string uic)
        {
            try
            {
                var aircraft = _repository.GetAircraftByUIC(uic);
                return aircraft.Select(MapToAircraftViewModel).ToList().OrderBy(a => a.SerialNumber);
            }
            catch (Exception e)
            {
                Log.Error("GetAircraftByUIC: Failed!", e);
                throw;
            }
        }

        public IEnumerable<AircraftViewModel> GetAllAircraft()
        {
            try
            {
                var aircraft = _repository.GetAllAircraft();
                return aircraft.Select(MapToAircraftViewModel).ToList().OrderBy(a => a.UnitId).ThenBy(a => a.SerialNumber);
            }
            catch (Exception e)
            {
                Log.Error("GetAllAircraft: Failed!", e);
                throw;
            }
        }

        /***   SETTERS   ***/

        public void CreateAircraft(AircraftViewModel aircraft)
        {
            try
            {
                _repository.CreateAircraft(MapToAircraft(aircraft));
            }
            catch (Exception e)
            {
                Log.Error("CreateAircraft: Failed!", e);
                throw;
            }
        }

        public void UpdateAircraft(AircraftViewModel aircraft)
        {
            try
            {
                var acft = _repository.GetAircraftById(aircraft.Id);
                CopyToAircraft(aircraft, acft);

                _repository.UpdateAircraft(acft);
            }
            catch (Exception e)
            {
                Log.Error("UpdateAircraft: Failed!", e);
                throw;
            }
        }

        public void DeleteAircraft(string id)
        {
            try
            {
                _repository.DeleteAircraft(id);
            }
            catch (Exception e)
            {
                Log.Error("DeleteAircraft: Failed!", e);
                throw;
            }
        }

        /***   HELPERS   ***/

        private static AircraftViewModel MapToAircraftViewModel(Aircraft aircraft)
        {
            return new AircraftViewModel
            {
                Id = aircraft.Id,
                AcftHrs = aircraft.AcftHrs,
                SerialNumber = aircraft.SerialNumber,
                AircraftModelId = aircraft.AircraftModelId,
                UnitId = aircraft.UnitId
            };
        }

        private static Aircraft MapToAircraft(AircraftViewModel aircraft)
        {
            return new Aircraft
            {
                Id = aircraft.Id,
                AcftHrs = aircraft.AcftHrs,
                SerialNumber = aircraft.SerialNumber,
                AircraftModelId = aircraft.AircraftModelId,
                UnitId = aircraft.UnitId
            };
        }

        private static void CopyToAircraft(AircraftViewModel view, Aircraft aircraft)
        {
            aircraft.Id = view.Id;
            aircraft.AcftHrs = view.AcftHrs;
            aircraft.SerialNumber = view.SerialNumber;
            aircraft.AircraftModelId = view.AircraftModelId;
            aircraft.UnitId = view.UnitId;
        }

        /***************************************************************************
         * 
         * FLIGHT RECORDS
         * 
         ***************************************************************************/

        /***   GETTERS   ***/

        public FlightViewModel GetFlightById(string id)
        {
            return MapToFlightViewModel(_repository.GetFlightById(id));
        }

        public IEnumerable<FlightViewModel> GetFlightsByAircraftId(string aircraftId)
        {
            var flights = _repository.GetFlightsByAircraftId(aircraftId);
            return flights.Select(MapToFlightViewModel).ToList().OrderBy(f => f.Date);
        }

        /***   SETTERS   ***/

        public void CreateFlight(FlightViewModel flight)
        {
            var aircraft = _repository.GetAircraftById(flight.AircraftId);
            aircraft.AcftHrs += flight.FlightHours;
            _repository.CreateFlight(MapToFlight(flight));
        }

        public void UpdateFlight(FlightViewModel flight)
        {
            var flt = _repository.GetFlightById(flight.Id);
            var aircraft = _repository.GetAircraftById(flight.AircraftId);
            if (Math.Abs(flight.FlightHours - flt.FlightHours) > 0.1)
            {
                aircraft.AcftHrs -= flt.FlightHours;
                aircraft.AcftHrs += flight.FlightHours;
            }
            CopyToFlight(flight, flt);

            _repository.UpdateFlight(flt);
        }

        public void DeleteFlight(string id)
        {
            var flight = _repository.GetFlightById(id);
            var aircraft = _repository.GetAircraftById(flight.AircraftId);
            aircraft.AcftHrs -= flight.FlightHours;
            _repository.DeleteFlight(id);
        }

        /***   HELPERS   ***/

        public FlightViewModel MapToFlightViewModel(Flight record)
        {
            var aircraft = _repository.GetAircraftById(record.AircraftId);
            var acftModel = _repository.GetAircraftModelById(aircraft.AircraftModelId);
            var unit = _repository.GetUnitById(aircraft.UnitId);
            return new FlightViewModel
            {
                Id = record.Id,
                Date = record.Date?.ToUpper(),
                SerialNumber = aircraft.SerialNumber,
                Model = acftModel.Mds?.ToUpper(),
                Organization = unit.UnitName,
                Station = unit.Station,
                FlightNumber = record.FlightNumber,
                From = record.From?.ToUpper(),
                To = record.To?.ToUpper(),
                Start = record.Start,
                End = record.End,
                FlightHours = record.FlightHours,
                AircraftId = record.AircraftId
            };
        }

        public static Flight MapToFlight(FlightViewModel flight)
        {
            var map = new Flight
            {
                Id = flight.Id,
                Date = flight.Date?.ToUpper(),
                FlightNumber = flight.FlightNumber,
                From = flight.From?.ToUpper(),
                To = flight.To?.ToUpper(),
                Start = flight.Start,
                End = flight.End,
                FlightHours = flight.FlightHours,
                AircraftId = flight.AircraftId
            };
            if (map.Date != null && map.Date.Contains("-"))
            {
                map.Date = DateTime.ParseExact(map.Date, "yyyy'-'MM'-'dd", CultureInfo.InvariantCulture)
                    .ToString("dd' 'MMM' 'yy")?.ToUpper();
            }
            return map;
        }

        private static void CopyToFlight(FlightViewModel viewModel, Flight flight)
        {
            flight.Id = viewModel.Id;
            flight.Date = viewModel.Date?.ToUpper();
            flight.FlightNumber = viewModel.FlightNumber;
            flight.From = viewModel.From?.ToUpper();
            flight.To = viewModel.To?.ToUpper();
            flight.Start = viewModel.Start;
            flight.End = viewModel.End;
            flight.FlightHours = viewModel.FlightHours;
            flight.AircraftId = viewModel.AircraftId;
            if (flight.Date != null && flight.Date.Contains("-"))
            {
                flight.Date = DateTime.ParseExact(flight.Date, "yyyy'-'MM'-'dd", CultureInfo.InvariantCulture)
                    .ToString("dd' 'MMM' 'yy")?.ToUpper();
            }
        }

        /***************************************************************************
         * 
         * INSPECTION RECORDS
         * 
         ***************************************************************************/

        /***   GETTERS   ***/

        public InspectionViewModel GetInspectionById(string id)
        {
            return MapToInspectionViewModel(_repository.GetInspectionById(id));
        }

        public IEnumerable<InspectionViewModel> GetInspectionsByAircraftId(string id)
        {
            var inspections = _repository.GetInspectionsByAircraftId(id);
            return inspections.Select(MapToInspectionViewModel).ToList().OrderBy(i => i.InspectionNumber);
        }

        /***   SETTERS   ***/

        public void CreateInspection(InspectionViewModel inspection)
        {
            _repository.CreateInspection(MapToInspection(inspection));
        }

        public void UpdateInspection(InspectionViewModel inspection)
        {
            _repository.UpdateInspection(MapToInspection(inspection));
        }

        public void DeleteInspection(string id)
        {
            _repository.DeleteInspection(id);
        }

        /***   HELPERS   ***/

        private InspectionViewModel MapToInspectionViewModel(Inspection inspection)
        {
            var aircraft = _repository.GetAircraftById(inspection.AircraftId);
            var acftModel = _repository.GetAircraftModelById(aircraft.AircraftModelId);
            return new InspectionViewModel
            {
                Id = inspection.Id,
                Nomenclature = acftModel.Name,
                Model = acftModel.Mds?.ToUpper(),
                SerialNumber = aircraft.SerialNumber,
                InspectionNumber = inspection.InspectionNumber?.ToUpper(),
                ItemToBeInspected = inspection.ItemToBeInspected?.ToUpper(),
                Reference = inspection.Reference?.ToUpper(),
                Frequency = inspection.Frequency?.ToUpper(),
                NextDue = inspection.NextDue?.ToUpper(),
                AircraftId = inspection.AircraftId
            };
        }

        private static Inspection MapToInspection(InspectionViewModel inspection)
        {
            var map = new Inspection
            {
                Id = inspection.Id,
                InspectionNumber = inspection.InspectionNumber?.ToUpper(),
                ItemToBeInspected = inspection.ItemToBeInspected?.ToUpper(),
                Reference = inspection.Reference?.ToUpper(),
                Frequency = inspection.Frequency?.ToUpper(),
                NextDue = inspection.NextDue?.ToUpper(),
                AircraftId = inspection.AircraftId
            };
            if (map.NextDue != null && map.NextDue.Contains("-"))
            {
                map.NextDue = DateTime.ParseExact(map.NextDue, "yyyy'-'MM'-'dd", CultureInfo.InvariantCulture)
                    .ToString("dd' 'MMM' 'yy")?.ToUpper();
            }
            return map;
        }

        private static void CopyToInspection(InspectionViewModel viewModel, Inspection inspection)
        {
            inspection.Id = viewModel.Id;
            inspection.InspectionNumber = viewModel.InspectionNumber?.ToUpper();
            inspection.ItemToBeInspected = viewModel.ItemToBeInspected?.ToUpper();
            inspection.Reference = viewModel.Reference?.ToUpper();
            inspection.Frequency = viewModel.Frequency?.ToUpper();
            inspection.NextDue = viewModel.NextDue?.ToUpper();
            inspection.AircraftId = viewModel.AircraftId;
            if (inspection.NextDue != null && inspection.NextDue.Contains("-"))
            {
                inspection.NextDue = DateTime.ParseExact(inspection.NextDue, "yyyy'-'MM'-'dd", CultureInfo.InvariantCulture)
                    .ToString("dd' 'MMM' 'yy")?.ToUpper();
            }
        }

        /***************************************************************************
         * 
         * FAULT RECORDS
         * 
         ***************************************************************************/

        /***   GETTERS   ***/

        public FaultViewModel GetFaultById(string id)
        {
            return MapToFaultViewModel(_repository.GetFaultById(id));
        }

        public IEnumerable<FaultViewModel> GetFaultsByAircraftId(string id)
        {
            var faults = _repository.GetFaultsByAircraftId(id);
            return faults.Select(MapToFaultViewModel).ToList().OrderBy(f => f.FaultDate).ThenBy(f => f.FaultNumber);
        }

        /***   SETTERS   ***/

        public void CreateFault(FaultViewModel fault)
        {
            _repository.CreateFault(MapToFault(fault));
        }

        public void UpdateFault(FaultViewModel fault)
        {
            _repository.UpdateFault(MapToFault(fault));
        }

        public void DeleteFault(string id)
        {
            _repository.DeleteFault(id);
        }

        /***   HELPERS   ***/

        private FaultViewModel MapToFaultViewModel(Fault fault)
        {
            var aircraft = _repository.GetAircraftById(fault.AircraftId);
            var acftModel = _repository.GetAircraftModelById(aircraft.AircraftModelId);
            return new FaultViewModel
            {
                Id = fault.Id,
                AcftSerialNumber = aircraft.SerialNumber,
                AcftModel = acftModel.Mds?.ToUpper(),
                Status = fault.Status,
                SystemCode = fault.SystemCode?.ToUpper(),
                FaultDate = fault.FaultDate?.ToUpper(),
                FaultNumber = fault.FaultNumber,
                FaultTime = fault.FaultTime,
                DiscPID = fault.DiscPID?.ToUpper(),
                FaultText = fault.FaultText?.ToUpper(),
                DiscAcftHrs = fault.DiscAcftHrs,
                WhenDisc = fault.WhenDisc?.ToUpper(),
                HowRecog = fault.HowRecog?.ToUpper(),
                MalEff = fault.MalEff?.ToUpper(),
                Delay = fault.Delay?.ToUpper(),
                WUC = fault.WUC?.ToUpper(),
                CompDate = fault.CompDate?.ToUpper(),
                CompTime = fault.CompTime,
                CompAcftHrs = fault.CompAcftHrs,
                Rounds = fault.Rounds,
                ActionCode = fault.ActionCode?.ToUpper(),
                CompWUC = fault.CompWUC?.ToUpper(),
                Action = fault.Action?.ToUpper(),
                CompPID = fault.CompPID?.ToUpper(),
                CompCat = fault.CompCat?.ToUpper(),
                CompHrs = fault.CompHrs,
                TIPID = fault.TIPID?.ToUpper(),
                TIManHrs = fault.TIManHrs,
                AircraftId = fault.AircraftId
            };
        }

        private static Fault MapToFault(FaultViewModel fault)
        {
            var map = new Fault
            {
                Id = fault.Id,
                Status = fault.Status,
                SystemCode = fault.SystemCode?.ToUpper(),
                FaultDate = fault.FaultDate?.ToUpper(),
                FaultNumber = fault.FaultNumber,
                FaultTime = fault.FaultTime,
                DiscPID = fault.DiscPID?.ToUpper(),
                FaultText = fault.FaultText?.ToUpper(),
                DiscAcftHrs = fault.DiscAcftHrs,
                WhenDisc = fault.WhenDisc?.ToUpper(),
                HowRecog = fault.HowRecog?.ToUpper(),
                MalEff = fault.MalEff?.ToUpper(),
                Delay = fault.Delay?.ToUpper(),
                WUC = fault.WUC?.ToUpper(),
                CompDate = fault.CompDate?.ToUpper(),
                CompTime = fault.CompTime,
                CompAcftHrs = fault.CompAcftHrs,
                Rounds = fault.Rounds,
                ActionCode = fault.ActionCode?.ToUpper(),
                CompWUC = fault.CompWUC?.ToUpper(),
                Action = fault.Action?.ToUpper(),
                CompPID = fault.CompPID?.ToUpper(),
                CompCat = fault.CompCat?.ToUpper(),
                CompHrs = fault.CompHrs,
                TIPID = fault.TIPID?.ToUpper(),
                TIManHrs = fault.TIManHrs,
                AircraftId = fault.AircraftId
            };
            if (map.FaultDate != null && map.FaultDate.Contains("-"))
            {
                map.FaultDate = DateTime.ParseExact(map.FaultDate, "yyyy'-'MM'-'dd", CultureInfo.InvariantCulture)
                    .ToString("dd' 'MMM' 'yy")?.ToUpper();
            }
            if (map.CompDate != null && map.CompDate.Contains("-"))
            {
                map.CompDate = DateTime.ParseExact(map.CompDate, "yyyy'-'MM'-'dd", CultureInfo.InvariantCulture)
                    .ToString("dd' 'MMM' 'yy")?.ToUpper();
            }
            return map;
        }

        private static void CopyToFault(FaultViewModel viewModel, Fault fault)
        {
            fault.Id = viewModel.Id;
            fault.Status = viewModel.Status;
            fault.SystemCode = viewModel.SystemCode?.ToUpper();
            fault.FaultDate = viewModel.FaultDate?.ToUpper();
            fault.FaultNumber = viewModel.FaultNumber;
            fault.FaultTime = viewModel.FaultTime;
            fault.DiscPID = viewModel.DiscPID?.ToUpper();
            fault.FaultText = viewModel.FaultText?.ToUpper();
            fault.DiscAcftHrs = viewModel.DiscAcftHrs;
            fault.WhenDisc = viewModel.WhenDisc?.ToUpper();
            fault.HowRecog = viewModel.HowRecog?.ToUpper();
            fault.MalEff = viewModel.MalEff?.ToUpper();
            fault.Delay = viewModel.Delay?.ToUpper();
            fault.WUC = viewModel.WUC?.ToUpper();
            fault.CompDate = viewModel.CompDate?.ToUpper();
            fault.CompTime = viewModel.CompTime;
            fault.CompAcftHrs = viewModel.CompAcftHrs;
            fault.Rounds = viewModel.Rounds;
            fault.ActionCode = viewModel.ActionCode?.ToUpper();
            fault.CompWUC = viewModel.CompWUC?.ToUpper();
            fault.Action = viewModel.Action?.ToUpper();
            fault.CompPID = viewModel.CompPID?.ToUpper();
            fault.CompCat = viewModel.CompCat?.ToUpper();
            fault.CompHrs = viewModel.CompHrs;
            fault.TIPID = viewModel.TIPID?.ToUpper();
            fault.TIManHrs = viewModel.TIManHrs;
            fault.AircraftId = viewModel.AircraftId;
            if (fault.FaultDate != null && fault.FaultDate.Contains("-"))
            {
                fault.FaultDate = DateTime.ParseExact(fault.FaultDate, "yyyy'-'MM'-'dd", CultureInfo.InvariantCulture)
                    .ToString("dd' 'MMM' 'yy")?.ToUpper();
            }
            if (fault.CompDate != null && fault.CompDate.Contains("-"))
            {
                fault.CompDate = DateTime.ParseExact(fault.CompDate, "yyyy'-'MM'-'dd", CultureInfo.InvariantCulture)
                    .ToString("dd' 'MMM' 'yy")?.ToUpper();
            }
        }

        /***************************************************************************
         * 
         * RELATED MAINTENANCE RECORDS
         * 
         ***************************************************************************/

        /***   GETTERS   ***/

        public RelatedMaintenanceViewModel GetRelatedMaintenanceById(string id)
        {
            return MapToRelatedMaintenanceViewModel(_repository.GetRelatedMaintenanceById(id));
        }

        public IEnumerable<RelatedMaintenanceViewModel> GetRelatedMaintenanceByFaultId(string id)
        {
            var related = _repository.GetRelatedMaintenanceByFaultId(id);
            return related.Select(MapToRelatedMaintenanceViewModel).ToList().OrderBy(r => r.Id);
        }

        /***   SETTERS   ***/

        public void CreateRelatedMaintenance(RelatedMaintenanceViewModel related)
        {
            _repository.CreateRelatedMaintenance(MapToRelatedMaintenance(related));
        }

        public void UpdateRelatedMaintenance(RelatedMaintenanceViewModel related)
        {
            _repository.UpdateRelatedMaintenance(MapToRelatedMaintenance(related));
        }

        public void DeleteRelatedMaintenance(string id)
        {
            _repository.DeleteRelatedMaintenance(id);
        }

        /***   HELPERS   ***/

        private RelatedMaintenanceViewModel MapToRelatedMaintenanceViewModel(RelatedMaintenance fault)
        {
            var f = _repository.GetFaultById(fault.FaultId);
            var acft = _repository.GetAircraftById(f.AircraftId);

            return new RelatedMaintenanceViewModel
            {
                Id = fault.Id,
                FaultStatus = f.Status?.ToUpper(),
                SerialNumber = acft.SerialNumber,
                SystemCode = f.SystemCode?.ToUpper(),
                FaultDate = f.FaultDate?.ToUpper(),
                FaultNumber = f.FaultNumber,
                FaultText = f.FaultText?.ToUpper(),
                Status = fault.Status,
                RelatedMaintenanceAction = fault.RelatedMaintenanceAction?.ToUpper(),
                CorrectiveAction = fault.CorrectiveAction?.ToUpper(),
                PID = fault.PID?.ToUpper(),
                Category = fault.Category?.ToUpper(),
                MMH = fault.MMH,
                TIPID = fault.TIPID?.ToUpper(),
                TIManHrs = fault.TIManHrs,
                FaultId = fault.FaultId
            };
        }

        private RelatedMaintenance MapToRelatedMaintenance(RelatedMaintenanceViewModel fault)
        {
            return new RelatedMaintenance
            {
                Id = fault.Id,
                Status = fault.Status,
                RelatedMaintenanceAction = fault.RelatedMaintenanceAction?.ToUpper(),
                CorrectiveAction = fault.CorrectiveAction?.ToUpper(),
                PID = fault.PID?.ToUpper(),
                Category = fault.Category?.ToUpper(),
                MMH = fault.MMH,
                TIPID = fault.TIPID?.ToUpper(),
                TIManHrs = fault.TIManHrs,
                FaultId = fault.FaultId
            };
        }

        private void CopyToRelatedMaintenance(RelatedMaintenanceViewModel viewModel, RelatedMaintenance fault)
        {
            fault.Id = viewModel.Id;
            fault.Status = viewModel.Status;
            fault.RelatedMaintenanceAction = viewModel.RelatedMaintenanceAction?.ToUpper();
            fault.CorrectiveAction = viewModel.CorrectiveAction?.ToUpper();
            fault.PID = viewModel.PID?.ToUpper();
            fault.Category = viewModel.Category?.ToUpper();
            fault.MMH = viewModel.MMH;
            fault.TIPID = viewModel.TIPID?.ToUpper();
            fault.TIManHrs = viewModel.TIManHrs;
            fault.FaultId = viewModel.FaultId;
        }
    }
}
