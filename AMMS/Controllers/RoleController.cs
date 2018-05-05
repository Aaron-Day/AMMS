using AMMS.Models.AccountViewModels;
using AMMS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMMS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly IAccountService _service;

        public RoleController(IAccountService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        public IActionResult List()
        {
            return View(_service.GetAllRoles());
        }

        // <C>RUD
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RoleListViewModel role)
        {
            _service.CreateRole(role);

            return RedirectToAction("List");
        }

        // C<R>UD
        public IActionResult Details(string id)
        {
            var role = _service.GetRoleById(id);

            return View(role);
        }

        // CR<U>D
        [HttpGet]
        public IActionResult Edit(string id)
        {
            var role = _service.GetRoleById(id);

            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(RoleListViewModel role)
        {
            _service.UpdateRole(role);

            return RedirectToAction("List");
        }

        // CRU<D>
        [HttpGet]
        public IActionResult Delete(string id)
        {
            var role = _service.GetRoleById(id);

            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(RoleListViewModel role)
        {
            _service.DeleteRole(role.Id);

            return RedirectToAction("List");
        }
    }
}
