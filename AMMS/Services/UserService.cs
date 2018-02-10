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
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public RegisterViewModel GetUser(string id)
        {
            var user = _repository.GetUser(id);

            return MapToRegisterViewModel(user);
        }

        public IEnumerable<RegisterViewModel> GetUsers(string uic)
        {
            var users = _repository.GetUsers(uic);

            return users.Select(MapToRegisterViewModel).ToList();
        }

        public IEnumerable<RegisterViewModel> GetAllUsers()
        {
            var users = _repository.GetAllUsers();

            return users.Select(MapToRegisterViewModel).ToList();
        }

        public void SaveUser(RegisterViewModel viewModel)
        {
            _repository.SaveUser(MapToUser(viewModel));
        }

        public void UpdateUser(RegisterViewModel viewModel)
        {
            var user = _repository.GetUser(viewModel.Id);
            CopyToUser(viewModel, user);

            _repository.UpdateUser(user);
        }

        public void DeleteUser(string id)
        {
            _repository.DeleteUser(id);
        }

        public string GetUserSalt(string id)
        {
            return _repository.GetUser(id).Salt;
        }

        public IList<UserRolesViewModel> GetUserRoles(string id)
        {
            return _repository.GetUserRoles(id);
        }

        public void UpdateUserRoles(IList<UserRolesViewModel> assignments)
        {
            _repository.UpdateUserRoles(assignments);
        }

        //---------------------------------------------------------------------------//

        public IEnumerable<UnitViewModel> GetUnits()
        {
            var units = _repository.GetUnits();
            return units.Select(MapToUnitViewModel).ToList();
        }

        public RequestViewModel GetRequest(string id)
        {
            var request = _repository.GetRequest(id);
            return MapToRequestViewModel(request);
        }

        public IEnumerable<RequestViewModel> GetAllRequests()
        {
            var requests = _repository.GetAllRequests();

            return requests.Select(MapToRequestViewModel).ToList();
        }

        public bool RequestExists(string email)
        {
            return _repository.RequestExists(email);
        }

        public void SaveRequest(RequestViewModel viewModel)
        {
            _repository.SaveRequest(MapToRequest(viewModel));
        }

        public void DeleteRequest(string id)
        {
            _repository.DeleteRequest(id);
        }

        public string GetUserId(string email)
        {
            return _repository.GetUserId(email);
        }

        //---------------------------------------------------------------------------//

        private ApplicationUser MapToUser(RegisterViewModel viewModel)
        {
            var user = _repository.GetUser(viewModel.Id);
            if (user != null)
            {
                CopyToUser(viewModel, user);
                return user;
            }
            user = new ApplicationUser
            {
                FirstName = viewModel.FirstName,
                MiddleName = viewModel.MiddleName,
                LastName = viewModel.LastName,
                Email = viewModel.Email.ToLower(),
                NormalizedEmail = viewModel.Email.ToUpper(),
                PhoneNumber = viewModel.PhoneNumber,
                PhoneNumberConfirmed = viewModel.PhoneNumber != null,
                SocialSecurityNumber = viewModel.SocialSecurityNumber,
                Rank = viewModel.Rank,
                DateOfBirth = viewModel.DateOfBirth,
                UserName = viewModel.Email.ToLower(),
                NormalizedUserName = viewModel.Email.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString("D"),
                FullName = (viewModel.Rank == null ? "" : $"{viewModel.Rank} ") +
                           $"{viewModel.LastName}, {viewModel.FirstName}",
                AssignedUnit = viewModel.AssignedUnit
            };
            var password = new PasswordHasher<ApplicationUser>();
            var hashed = password.HashPassword(user, PasswordProtocol.CalculateHash(viewModel.Password, user.Salt));
            user.PasswordHash = hashed;

            return user;
        }

        private static RegisterViewModel MapToRegisterViewModel(ApplicationUser user)
        {
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

        private static void CopyToUser(RegisterViewModel viewModel, ApplicationUser user)
        {
            if (viewModel.Id != user.Id) return;
            user.Rank = viewModel.Rank;
            user.FirstName = viewModel.FirstName;
            user.MiddleName = viewModel.MiddleName;
            user.LastName = viewModel.LastName;
            user.Email = viewModel.Email;
            user.PhoneNumber = viewModel.PhoneNumber;
            user.DateOfBirth = viewModel.DateOfBirth;
            user.SocialSecurityNumber = viewModel.SocialSecurityNumber;
            user.AssignedUnit = viewModel.AssignedUnit;
        }

        //-----------------------------------------------------------------------------------//

        private RequestViewModel MapToRequestViewModel(Request request)
        {
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

        private Request MapToRequest(RequestViewModel viewModel)
        {
            return new Request
            {
                Id = viewModel.Id,
                Requested = viewModel.Requested,
                Email = viewModel.Email,
                PhoneNumber = viewModel.PhoneNumber,
                Password = viewModel.Password,
                Unit = viewModel.Unit
            };
        }

        public UnitViewModel MapToUnitViewModel(Unit unit)
        {
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
    }
}
