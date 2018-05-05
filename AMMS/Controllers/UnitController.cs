using AMMS.Models.ViewModels;
using AMMS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMMS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UnitController : Controller
    {
        private readonly IAccountService _service;

        public UnitController(IAccountService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        public IActionResult List()
        {
            var viewModels = _service.GetAllUnits();

            return View(viewModels);
        }

        // <C>RUD
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UnitViewModel unit)
        {
            if (!ModelState.IsValid) { return View(); }

            _service.CreateUnit(unit);

            return RedirectToAction("List");
        }

        // C<R>UD
        public IActionResult Details(string id)
        {
            var viewModel = _service.GetUnitById(id);

            return View(viewModel);
        }

        // CR<U>D
        [HttpGet]
        public IActionResult Edit(string id)
        {
            var viewModel = _service.GetUnitById(id);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UnitViewModel unit)
        {
            if (!ModelState.IsValid) { return View(); }

            _service.UpdateUnit(unit);

            return RedirectToAction("List");
        }

        // CRU<D>
        [HttpGet]
        public IActionResult Delete(string id)
        {
            var viewModel = _service.GetUnitById(id);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(UnitViewModel unit)
        {
            _service.DeleteUnit(unit.Id);

            return RedirectToAction("List");
        }
    }
}
