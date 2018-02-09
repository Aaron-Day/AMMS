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

        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        public IActionResult List()
        {
            var viewModels = _service.GetAllModels();

            return View(viewModels);
        }

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

        public IActionResult Details(string id)
        {
            var viewModel = _service.GetModel(id);

            return View(viewModel);
        }

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

        public IActionResult Delete(string id)
        {
            _service.DeleteModel(id);

            return RedirectToAction("List");
        }
    }
}
