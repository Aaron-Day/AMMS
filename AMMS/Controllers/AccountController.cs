using AMMS.Models.AccountViewModels;
using AMMS.Models.ViewModels;
using AMMS.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AMMS.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(AccountController));

        private readonly IMasterService _service;

        public AccountController(IMasterService service)
        {
            _service = service;
        }

        public IActionResult Index(string value = null)
        {
            return RedirectToAction("List", new { value });
        }

        [Authorize(Roles = "Admin, PC, QC")]
        public IActionResult List(string value = null)
        {
            var users = _service.GetUsers(value, User);
            var filter = _service.GetType(value, User);

            ViewData["Filter"] = $" - {filter}";
            return View(users);
        }

        [Authorize]
        public IActionResult Details(string id)
        {
            if (id == null) return NotFound();
            var user = _service.GetUserById(id);

            return View(user);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, PC, QC")]
        public IActionResult Edit(string id)
        {
            if (id == null) return NotFound();
            var user = _service.GetUserById(id);

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, PC, QC")]
        public IActionResult Edit(RegisterViewModel registration)
        {
            if (registration == null) return NotFound();
            if (!ModelState.IsValid) return View(registration);
            _service.UpdateUser(registration);

            return RedirectToAction("List", new { uic = registration.AssignedUnit });
        }

        [HttpGet]
        [Authorize(Roles = "Admin, PC, QC")]
        public IActionResult Delete(string id)
        {
            if (id == null) return NotFound();
            var user = _service.GetUserById(id);

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, PC, QC")]
        public IActionResult Delete(RegisterViewModel registration)
        {
            if (registration == null) return NotFound();
            _service.DeleteUser(registration.Id);

            return RedirectToAction("List", new { uic = registration.AssignedUnit });
        }

        [HttpGet]
        public IActionResult ManageRoles(string id)
        {
            if (id == null) return NotFound();
            var user = _service.GetUserById(id);
            ViewBag.User = user;
            TempData["Assigned Unit"] = user.AssignedUnit;
            var roles = _service.GetUserRoles(id);

            return View(roles);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ManageRoles(IList<UserRolesViewModel> roles)
        {
            if (roles == null) return NotFound();
            if (!ModelState.IsValid) return View(roles);
            foreach (var role in roles)
            {
                if (role.Assigned)
                {
                    // At least one role assigned
                    _service.UpdateUserRoles(roles);

                    return RedirectToAction("List", new { uic = TempData["Assigned Unit"] });
                }
            }
            // No roles assigned
            ModelState.AddModelError("Error", "Empty Roles");
            return View(roles);
        }

        [TempData]
        public string ErrorMessage { get; set; }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel login)
        {
            if (login == null) return NotFound();
            if (!ModelState.IsValid) return View(login);

            var result = _service.Login(login);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            if (result.IsLockedOut)
            {
                return RedirectToAction(nameof(Lockout));
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(login);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin, PC, QC")]
        public IActionResult Register(string id = null)
        {
            if (id == null) return NotFound();

            ViewBag.Request = _service.GetRequestById(id);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, PC, QC")]
        public IActionResult Register(RegisterViewModel registration, string id)
        {
            if (id == null) return NotFound();
            if (!ModelState.IsValid) return View(registration);

            ViewBag.Units = _service.GetAllUnits();
            _service.CreateUser(registration);
            _service.DeleteRequest(id);

            return RedirectToAction("Requests");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout(string id)
        {
            // Prevent any auto-login from working
            _service.Logout(id);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ForgotPassword(ForgotPasswordViewModel forgotPassword)
        {
            if (!ModelState.IsValid) return View(forgotPassword);

            var user = _service.GetUserByEmail(forgotPassword.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist or is not confirmed
                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }
            // TODO: notify admin that password is forgotten
            return RedirectToAction(nameof(ForgotPasswordConfirmation));
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult ChangePassword(string id)
        {
            if (id == null) return NotFound();
            ViewBag.Id = id;

            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(ChangePasswordViewModel change)
        {
            if (change == null) return NotFound();
            if (change.OldPassword == change.NewPassword)
                ModelState.AddModelError(string.Empty, "Old password cannot match new password!");
            if (!ModelState.IsValid) return View(change);
            _service.ChangePassword(change);
            return RedirectToAction("ChangePasswordConfirmation");
        }

        public IActionResult ChangePasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        //-----------------------------------------------------------------------------------//
        // ACCOUNT REQUEST

        [HttpGet]
        [AllowAnonymous]
        public IActionResult RequestAccount()
        {
            ViewBag.Units = _service.GetAllUnits();

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult RequestAccount(RequestViewModel request)
        {
            if (request == null) return NotFound();
            if (!ModelState.IsValid) return View(request);

            if (_service.GetRequestByEmail(request.Email) != null) return View("RequestDenied");
            if (_service.GetUserByEmail(request.Email) != null) return View("AccountExists");

            _service.CreateRequest(request);

            return View("RequestConfirmation");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Requests()
        {
            return View(_service.GetAllRequests());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ApproveRequest(string id)
        {
            if (id == null) return NotFound();
            return RedirectToAction("Register", new { id });
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DenyRequest(string id)
        {
            if (id == null) return NotFound();
            _service.DeleteRequest(id);

            return RedirectToAction("Requests");
        }
    }
}
