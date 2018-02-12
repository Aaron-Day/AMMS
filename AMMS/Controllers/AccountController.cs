using AMMS.Models;
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

        private readonly IUserService _service;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUserService service)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _service = service;
        }

        [Authorize(Roles = "Admin, PC, QC")]
        public IActionResult List(string uic = null)
        {
            if (uic == null && TempData["Assigned Unit"] != null)
                uic = (string)TempData["Assigned Unit"];
            var users = _service.GetUsers(uic);

            return View(users);
        }

        [Authorize]
        public IActionResult Details(string id)
        {
            if (id == null) return NotFound();
            var user = _service.GetUser(id);

            return View(user);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, PC, QC")]
        public IActionResult Edit(string id)
        {
            if (id == null) return NotFound();
            var user = _service.GetUser(id);

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, PC, QC")]
        public IActionResult Edit(RegisterViewModel viewModel)
        {
            if (viewModel == null) return NotFound();
            _service.UpdateUser(viewModel);

            return RedirectToAction("List", new { uic = viewModel.AssignedUnit });
        }

        [HttpGet]
        [Authorize(Roles = "Admin, PC, QC")]
        public IActionResult Delete(string id)
        {
            if (id == null) return NotFound();
            var user = _service.GetUser(id);

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, PC, QC")]
        public IActionResult Delete(RegisterViewModel viewModel)
        {
            if (viewModel == null) return NotFound();
            _service.DeleteUser(viewModel.Id);

            return RedirectToAction("List", new { uic = viewModel.AssignedUnit });
        }

        [HttpGet]
        public IActionResult ManageRoles(string id)
        {
            if (id == null) return NotFound();
            var user = _service.GetUser(id);
            ViewBag.User = user;
            TempData["Assigned Unit"] = user.AssignedUnit;
            var roles = _service.GetUserRoles(id);

            return View(roles);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ManageRoles(IList<UserRolesViewModel> viewModel)
        {
            if (viewModel == null) return NotFound();
            _service.UpdateUserRoles(viewModel);

            return RedirectToAction("List", new { uic = TempData["Assigned Unit"] });
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
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (viewModel == null) return NotFound();
            if (!ModelState.IsValid) return View();

            var email = viewModel.Email;
            var id = _service.GetUserId(email);
            var pass = viewModel.Password;
            var salt = _service.GetUserSalt(id);

            var result = await _signInManager.PasswordSignInAsync(email, PasswordProtocol.CalculateHash(pass, salt), false, true);

            if (result.Succeeded)
            {
                return RedirectToLocal(null);
            }

            if (result.IsLockedOut)
            {
                return RedirectToAction(nameof(Lockout));
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(viewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Register(string id = null)
        {
            if (id == null) return NotFound();

            ViewBag.Request = _service.GetRequest(id);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Register(RegisterViewModel viewModel, string id)
        {
            if (id == null) return NotFound();
            if (!ModelState.IsValid) return View();

            ViewBag.Units = _service.GetUnits();
            _service.SaveUser(viewModel);
            _service.DeleteRequest(id);

            return RedirectToAction("Requests");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // Prevent any auto-login from working
            await _userManager.UpdateSecurityStampAsync(await _userManager.GetUserAsync(User));
            await _signInManager.SignOutAsync();
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
        public IActionResult ForgotPassword(ForgotPasswordViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var user = _service.GetUserId(viewModel.Email);
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
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult RequestAccount()
        {
            ViewBag.Units = _service.GetUnits();

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult RequestAccount(RequestViewModel viewModel)
        {
            if (viewModel == null) return NotFound();
            if (!ModelState.IsValid) return View(viewModel);

            if (_service.RequestExists(viewModel.Email)) return View("RequestDenied");

            _service.SaveRequest(viewModel);

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
            return RedirectToAction("Register",
                new RouteValueDictionary(new { controller = "Account", action = "Register", id }));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DenyRequest(string id)
        {
            if (id == null) return NotFound();
            _service.DeleteRequest(id);

            return RedirectToAction("Requests");
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion
    }
}
