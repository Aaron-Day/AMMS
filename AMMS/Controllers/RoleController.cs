using AMMS.Models.AccountViewModels;
using AMMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace AMMS.Controllers
{
    // TODO: Restrict all to admin
    public class RoleController : Controller
    {
        private readonly IRoleService _service;

        public RoleController(IRoleService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        public IActionResult List()
        {
            return View();
        }

        // <C>RUD
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(RoleListViewModel viewModel)
        {
            _service.SaveRole(viewModel);

            return RedirectToAction("List");
        }

        // C<R>UD
        public IActionResult Details(string id)
        {
            var role = _service.GetRole(id);

            return View(role);
        }

        // CR<U>D
        [HttpGet]
        public IActionResult Edit(string id)
        {
            var role = _service.GetRole(id);

            return View(role);
        }

        [HttpPost]
        public IActionResult Edit(RoleListViewModel viewModel)
        {
            _service.UpdateRole(viewModel);

            return RedirectToAction("List");
        }

        // CRU<D>
        [HttpGet]
        public IActionResult Delete(string id)
        {
            var role = _service.GetRole(id);

            return View(role);
        }

        [HttpPost]
        public IActionResult Delete(RoleListViewModel viewModel)
        {
            _service.DeleteRole(viewModel.Id);

            return RedirectToAction("List");
        }
    }
}
