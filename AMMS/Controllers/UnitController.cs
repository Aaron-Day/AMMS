using AMMS.Models.ViewModels;
using AMMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace AMMS.Controllers
{
    public class UnitController : Controller
    {
        private readonly IUnitService _service;

        public UnitController(IUnitService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        public IActionResult List()
        {
            var viewModels = _service.GetUnits();

            return View(viewModels);
        }

        // <C>RUD
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UnitViewModel viewModel)
        {
            if (!ModelState.IsValid) { return View(); }

            _service.SaveUnit(viewModel);

            return RedirectToAction("List");
        }

        // C<R>UD
        public IActionResult Details(string id)
        {
            var viewModel = _service.GetUnit(id);

            return View(viewModel);
        }

        // CR<U>D
        [HttpGet]
        public IActionResult Edit(string id)
        {
            var viewModel = _service.GetUnit(id);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(UnitViewModel viewModel)
        {
            if (!ModelState.IsValid) { return View(); }

            _service.UpdateUnit(viewModel);

            return RedirectToAction("List");
        }

        // CRU<D>
        public IActionResult Delete(string id)
        {
            _service.DeleteUnit(id);

            return RedirectToAction("List");
        }
    }
}
