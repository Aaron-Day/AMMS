using AMMS.Models.ViewModels;
using AMMS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMMS.Controllers
{
    [Authorize]
    public class AircraftModelController : Controller
    {
        private readonly IMasterService _service;

        public AircraftModelController(IMasterService service)
        {
            _service = service;
        }

        public IActionResult Index(string parentId)
        {
            return RedirectToAction("List", new { parentId });
        }

        public IActionResult List(string parentId)
        {
            TempData["ParentId"] = parentId;

            var models = parentId == null
                ? _service.GetAllAircraftModels()
                : _service.GetAircraftModelsByUIC(parentId);

            return View(models);
        }

        // <C>RUD
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AircraftModelViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            _service.CreateAircraftModel(model);

            return RedirectToAction("List", new { parentId = TempData["ParentId"] });
        }

        // C<R>UD
        public IActionResult Details(string id)
        {
            var model = _service.GetAircraftModelById(id);

            return View(model);
        }

        // CR<U>D
        [HttpGet]
        public IActionResult Edit(string id)
        {
            var viewModel = _service.GetAircraftModelById(id);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AircraftModelViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            _service.UpdateAircraftModel(model);

            return RedirectToAction("List", new { parentId = TempData["ParentId"] });
        }

        //TODO: Recursively delete aircraft
        // CRU<D>
        [HttpGet]
        public IActionResult Delete(string id)
        {
            var model = _service.GetAircraftModelById(id);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(AircraftModelViewModel model)
        {
            _service.DeleteAircraftModel(model.Id);

            return RedirectToAction("List", new { parentId = TempData["ParentId"] });
        }
    }
}
