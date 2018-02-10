using AMMS.Models.ViewModels;
using AMMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace AMMS.Controllers
{
    public class AircraftModelController : Controller
    {
        private readonly IAircraftModelService _service;

        public AircraftModelController(IAircraftModelService service)
        {
            _service = service;
        }

        public IActionResult Index(string parentId)
        {
            return RedirectToAction("List", new { parentId });
        }

        public IActionResult List(string parentId)
        {
            var viewModels = _service.GetModels(parentId);

            TempData["ParentId"] = parentId;

            return View(viewModels);
        }

        // <C>RUD
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AircraftModelViewModel viewModel)
        {
            if (!ModelState.IsValid) return View();

            _service.SaveModel(viewModel);

            return RedirectToAction("List");
        }

        // C<R>UD
        public IActionResult Details(string id)
        {
            var viewModel = _service.GetModel(id);

            return View(viewModel);
        }

        // CR<U>D
        [HttpGet]
        public IActionResult Edit(string id)
        {
            var viewModel = _service.GetModel(id);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(AircraftModelViewModel viewModel)
        {
            if (!ModelState.IsValid) return View();

            _service.UpdateModel(viewModel);

            return RedirectToAction("List");
        }

        // CRU<D>
        [HttpGet]
        public IActionResult Delete(string id)
        {
            var viewModel = _service.GetModel(id);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Delete(AircraftModelViewModel model)
        {
            _service.DeleteModel(model.Id);

            return RedirectToAction("List");
        }
    }
}
