using AMMS.Models;
using AMMS.Models.AccountViewModels;
using AMMS.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;

namespace AMMS.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
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

        [TempData]
        public string ErrorMessage { get; set; }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid) return View();

            var email = model.Email;
            var id = _service.GetUserId(email);
            var pass = model.Password;
            var salt = _service.GetUserSalt(id);

            var result = await _signInManager.PasswordSignInAsync(email, PasswordProtocol.CalculateHash(pass, salt), false, true);

            if (result.Succeeded)
            {
                return RedirectToLocal(returnUrl);
            }

            if (result.IsLockedOut)
            {
                return RedirectToAction(nameof(Lockout));
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
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
            if (id == null)
            {
                ViewBag.Status = "Not Found";
                return View();
            }
            ViewBag.Status = "Found";
            ViewBag.Request = _service.GetRequest(id);

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model, string id = null)
        {
            if (!ModelState.IsValid || id == null) return View();

            _service.SaveUser(model);
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
        public IActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = _service.GetUserId(model.Email);
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
        public IActionResult RequestAccount(RequestViewModel request)
        {
            if (!ModelState.IsValid) return View();

            if (_service.RequestExists(request.Email)) return View("RequestDenied");

            _service.SaveRequest(request);

            return View("RequestConfirmation");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Requests()
        {
            return View(_service.GetAllRequests());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ApproveRequest(string id = null)
        {
            return RedirectToAction("Register",
                new RouteValueDictionary(new { controller = "Account", action = "Register", id }));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DenyRequest(string id = null)
        {
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
