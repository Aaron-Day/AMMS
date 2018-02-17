using AMMS.Models;
using AMMS.Models.AccountViewModels;
using AMMS.Models.ViewModels;
using AMMS.Repository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AMMS.Services
{
    public class AccountService : IAccountService
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(AccountService));

        private readonly IAccountRepository _repository;

        public AccountService(IAccountRepository repository)
        {
            _repository = repository;
        }

        #region Getters
        /*----------GETTERS----------*/
        // Get user(s)
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

        // Get role(s)
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


        // Get user role(s)
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

        // Get user request(s)
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

        // Get unit(s)
        public UnitViewModel GetUnitById(string id)
        {
            try
            {
                var unit = _repository.GetUnitById(id);
                return MapToUnitViewModel(unit);
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
        #endregion

        #region Setters
        /*----------SETTERS----------*/
        // Set user(s)
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

        // Set role(s)
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

        // Set user role(s)
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

        // Set user request(s)
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

        // Set unit(s)
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
        #endregion

        #region Account
        /*----------ACCOUNT----------*/
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
        #endregion

        #region Helpers
        //------------------------------------------------------------------------//
        // HELPERS

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

        private Unit MapToUnit(UnitViewModel unit)
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

        public UnitViewModel MapToUnitViewModel(Unit unit)
        {
            if (unit == null) return null;
            return new UnitViewModel
            {
                Id = unit.Id,
                CompanyName = unit.CompanyName,
                Station = unit.Station,
                UIC = unit.UIC,
                UnitName = unit.UnitName,
                UnitPhone = unit.UnitPhone
            };
        }

        private void CopyToUnit(UnitViewModel unit, Unit update)
        {
            if (unit.Id != update.Id) return;
            update.CompanyName = unit.CompanyName;
            update.Station = unit.Station;
            update.UIC = unit.UIC;
            update.UnitName = unit.UnitName;
            update.UnitPhone = unit.UnitPhone;
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

        public string GetType(string value)
        {
            return _repository.GetType(value);
        }
        #endregion
    }
}
