﻿using AMMS.Models;
using AMMS.Models.AccountViewModels;
using AMMS.Models.ViewModels;
using AMMS.Repository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
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
                return requests.Select(MapToRequestViewModel).ToList();
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
                return requests.Select(MapToRequestViewModel).ToList();
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
                return users.Select(MapToRegisterViewModel).ToList();
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
                return users.Select(MapToRegisterViewModel).ToList();
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
                return users.Select(MapToRegisterViewModel).ToList();
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
                NormalizedEmail = user.Email.ToUpper(),
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumber != null,
                SocialSecurityNumber = user.SocialSecurityNumber,
                Rank = user.Rank,
                DateOfBirth = user.DateOfBirth,
                UserName = user.Email.ToLower(),
                NormalizedUserName = user.Email.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString("D"),
                FullName = (user.Rank == null ? "" : $"{user.Rank} ") +
                           $"{user.LastName}, {user.FirstName}",
                AssignedUnit = user.AssignedUnit
            };
            var password = new PasswordHasher<ApplicationUser>();
            var hashed = password.HashPassword(update, PasswordProtocol.CalculateHash(user.Password, update.Salt));
            update.PasswordHash = hashed;

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
                return roles.Select(MapToRoleListViewModel).ToList();
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
            return new RoleListViewModel
            {
                Id = role.Id,
                RoleName = role.Name,
                // TODO: TEST
                NumberOfUsers = GetUsersByRole(role.Name).Count()
            };
        }

        private static IdentityRole MapToRole(RoleListViewModel role)
        {
            if (role == null) return null;
            return new IdentityRole
            {
                Id = role.Id,
                Name = role.RoleName,
                NormalizedName = role.RoleName.ToUpper()
            };
        }

        private static void CopyToRole(RoleListViewModel role, IdentityRole update)
        {
            if (role.Id != update.Id) return;
            update.Name = role.RoleName;
            update.NormalizedName = role.RoleName.ToUpper();
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
                return units.Select(MapToUnitViewModel).ToList();
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
                return models.Select(MapToAircraftModelViewModel).ToList();
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
                return models.Select(MapToAircraftModelViewModel).ToList();
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
                return aircraft.Select(MapToAircraftViewModel).ToList();
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
                return aircraft.Select(MapToAircraftViewModel).ToList();
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
                return aircraft.Select(MapToAircraftViewModel).ToList();
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
            return flights.Select(MapToFlightViewModel).ToList();
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
                Date = record.Date,
                SerialNumber = aircraft.SerialNumber,
                Model = acftModel.Mds,
                Organization = unit.UnitName,
                Station = unit.Station,
                FlightNumber = record.FlightNumber,
                From = record.From,
                To = record.To,
                Start = record.Start,
                End = record.End,
                FlightHours = record.FlightHours,
                AircraftId = record.AircraftId
            };
        }

        public static Flight MapToFlight(FlightViewModel flight)
        {
            return new Flight
            {
                Id = flight.Id,
                Date = flight.Date,
                FlightNumber = flight.FlightNumber,
                From = flight.From,
                To = flight.To,
                Start = flight.Start,
                End = flight.End,
                FlightHours = flight.FlightHours,
                AircraftId = flight.AircraftId
            };
        }

        private static void CopyToFlight(FlightViewModel viewModel, Flight flight)
        {
            flight.Id = viewModel.Id;
            flight.Date = viewModel.Date;
            flight.FlightNumber = viewModel.FlightNumber;
            flight.From = viewModel.From;
            flight.To = viewModel.To;
            flight.Start = viewModel.Start;
            flight.End = viewModel.End;
            flight.FlightHours = viewModel.FlightHours;
            flight.AircraftId = viewModel.AircraftId;
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
            return inspections.Select(MapToInspectionViewModel).ToList();
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
                Model = acftModel.Mds,
                SerialNumber = aircraft.SerialNumber,
                InspectionNumber = inspection.InspectionNumber,
                ItemToBeInspected = inspection.ItemToBeInspected,
                Reference = inspection.Reference,
                Frequency = inspection.Frequency,
                NextDue = inspection.NextDue,
                CompletedAt = inspection.CompletedAt,
                AircraftId = inspection.AircraftId
            };
        }

        private static Inspection MapToInspection(InspectionViewModel inspection)
        {
            return new Inspection
            {
                Id = inspection.Id,
                InspectionNumber = inspection.InspectionNumber,
                ItemToBeInspected = inspection.ItemToBeInspected,
                Reference = inspection.Reference,
                Frequency = inspection.Frequency,
                NextDue = inspection.NextDue,
                CompletedAt = inspection.CompletedAt,
                AircraftId = inspection.AircraftId
            };
        }

        private static void CopyToInspection(InspectionViewModel viewModel, Inspection inspection)
        {
            inspection.Id = viewModel.Id;
            inspection.InspectionNumber = viewModel.InspectionNumber;
            inspection.ItemToBeInspected = viewModel.ItemToBeInspected;
            inspection.Reference = viewModel.Reference;
            inspection.Frequency = viewModel.Frequency;
            inspection.NextDue = viewModel.NextDue;
            inspection.CompletedAt = viewModel.CompletedAt;
            inspection.AircraftId = viewModel.AircraftId;
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
            return faults.Select(MapToFaultViewModel).ToList();
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
                AcftModel = acftModel.Mds,
                Status = fault.Status,
                SystemCode = fault.SystemCode,
                FaultDate = fault.FaultDate,
                FaultNumber = fault.FaultNumber,
                FaultTime = fault.FaultTime,
                DiscPID = fault.DiscPID,
                FaultText = fault.FaultText,
                DiscAcftHrs = fault.DiscAcftHrs,
                WhenDisc = fault.WhenDisc,
                HowRecog = fault.HowRecog,
                MalEff = fault.MalEff,
                Delay = fault.Delay,
                WUC = fault.WUC,
                CompDate = fault.CompDate,
                CompTime = fault.CompTime,
                CompAcftHrs = fault.CompAcftHrs,
                Rounds = fault.Rounds,
                ActionCode = fault.ActionCode,
                CompWUC = fault.CompWUC,
                Action = fault.Action,
                CompPID = fault.CompPID,
                CompCat = fault.CompCat,
                CompHrs = fault.CompHrs,
                TIPID = fault.TIPID,
                TIManHrs = fault.TIManHrs,
                AircraftId = fault.AircraftId
            };
        }

        private static Fault MapToFault(FaultViewModel fault)
        {
            return new Fault
            {
                Id = fault.Id,
                Status = fault.Status,
                SystemCode = fault.SystemCode,
                FaultDate = fault.FaultDate,
                FaultNumber = fault.FaultNumber,
                FaultTime = fault.FaultTime,
                DiscPID = fault.DiscPID,
                FaultText = fault.FaultText,
                DiscAcftHrs = fault.DiscAcftHrs,
                WhenDisc = fault.WhenDisc,
                HowRecog = fault.HowRecog,
                MalEff = fault.MalEff,
                Delay = fault.Delay,
                WUC = fault.WUC,
                CompDate = fault.CompDate,
                CompTime = fault.CompTime,
                CompAcftHrs = fault.CompAcftHrs,
                Rounds = fault.Rounds,
                ActionCode = fault.ActionCode,
                CompWUC = fault.CompWUC,
                Action = fault.Action,
                CompPID = fault.CompPID,
                CompCat = fault.CompCat,
                CompHrs = fault.CompHrs,
                TIPID = fault.TIPID,
                TIManHrs = fault.TIManHrs,
                AircraftId = fault.AircraftId
            };
        }

        private static void CopyToFault(FaultViewModel viewModel, Fault fault)
        {
            fault.Id = viewModel.Id;
            fault.Status = viewModel.Status;
            fault.SystemCode = viewModel.SystemCode;
            fault.FaultDate = viewModel.FaultDate;
            fault.FaultNumber = viewModel.FaultNumber;
            fault.FaultTime = viewModel.FaultTime;
            fault.DiscPID = viewModel.DiscPID;
            fault.FaultText = viewModel.FaultText;
            fault.DiscAcftHrs = viewModel.DiscAcftHrs;
            fault.WhenDisc = viewModel.WhenDisc;
            fault.HowRecog = viewModel.HowRecog;
            fault.MalEff = viewModel.MalEff;
            fault.Delay = viewModel.Delay;
            fault.WUC = viewModel.WUC;
            fault.CompDate = viewModel.CompDate;
            fault.CompTime = viewModel.CompTime;
            fault.CompAcftHrs = viewModel.CompAcftHrs;
            fault.Rounds = viewModel.Rounds;
            fault.ActionCode = viewModel.ActionCode;
            fault.CompWUC = viewModel.CompWUC;
            fault.Action = viewModel.Action;
            fault.CompPID = viewModel.CompPID;
            fault.CompCat = viewModel.CompCat;
            fault.CompHrs = viewModel.CompHrs;
            fault.TIPID = viewModel.TIPID;
            fault.TIManHrs = viewModel.TIManHrs;
            fault.AircraftId = viewModel.AircraftId;
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
            return related.Select(MapToRelatedMaintenanceViewModel).ToList();
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
            var f = _repository.GetFaultById(fault.Id);
            var acft = _repository.GetAircraftById(f.AircraftId);

            return new RelatedMaintenanceViewModel
            {
                Id = fault.Id,
                FaultStatus = f.Status,
                SerialNumber = acft.SerialNumber,
                SystemCode = f.SystemCode,
                FaultDate = f.FaultDate,
                FaultNumber = f.FaultNumber,
                FaultText = f.FaultText,
                Status = fault.Status,
                RelatedMaintenanceAction = fault.RelatedMaintenanceAction,
                CorrectiveAction = fault.CorrectiveAction,
                PID = fault.PID,
                Category = fault.Category,
                MMH = fault.MMH,
                TIPID = fault.TIPID,
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
                RelatedMaintenanceAction = fault.RelatedMaintenanceAction,
                CorrectiveAction = fault.CorrectiveAction,
                PID = fault.PID,
                Category = fault.Category,
                MMH = fault.MMH,
                TIPID = fault.TIPID,
                TIManHrs = fault.TIManHrs,
                FaultId = fault.FaultId
            };
        }

        private void CopyToRelatedMaintenance(RelatedMaintenanceViewModel viewModel, RelatedMaintenance fault)
        {
            fault.Id = viewModel.Id;
            fault.Status = viewModel.Status;
            fault.RelatedMaintenanceAction = viewModel.RelatedMaintenanceAction;
            fault.CorrectiveAction = viewModel.CorrectiveAction;
            fault.PID = viewModel.PID;
            fault.Category = viewModel.Category;
            fault.MMH = viewModel.MMH;
            fault.TIPID = viewModel.TIPID;
            fault.TIManHrs = viewModel.TIManHrs;
            fault.FaultId = viewModel.FaultId;
        }
    }
}