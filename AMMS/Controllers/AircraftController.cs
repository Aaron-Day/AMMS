using AMMS.Models.ViewModels;
using AMMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace AMMS.Controllers
{
    public class AircraftController : Controller
    {
        private readonly IAircraftService _service;

        public AircraftController(IAircraftService service)
        {
            _service = service;
        }

        public IActionResult Index(string parentId)
        {
            return RedirectToAction("List", new { parentId });
        }

        public IActionResult List(string parentId)
        {
            /* accepts user, unit or aircraft id
             * and returns all aircraft for the unit
             * (user's assigned unit
             * or aircraft's owning unit)
             */
             //TODO: Restrict adding users to units that don't exist
            parentId = _service.GetUnitId(parentId);

            TempData["ParentId"] = parentId;

            var viewModels = _service.GetAllAircraft(parentId);

            return View(viewModels);
        }

        // <C>RUD
        [HttpGet]
        public IActionResult Create(string parentId)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AircraftViewModel viewModel)
        {
            if (!ModelState.IsValid) return View();

            _service.SaveAircraft(viewModel);

            return RedirectToAction("List", new { parentId = TempData["ParentId"] });
        }

        // C<R>UD
        public IActionResult Details(string id)
        {
            var viewModel = _service.GetAircraft(id);

            return View(viewModel);
        }

        // CR<U>D
        [HttpGet]
        public IActionResult Edit(string id)
        {
            var viewModel = _service.GetAircraft(id);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(AircraftViewModel viewModel)
        {
            if (!ModelState.IsValid) return View();

            _service.UpdateAircraft(viewModel);

            return RedirectToAction("List", new { parentId = viewModel.AircraftModelId });
        }

        // CRU<D>
        public IActionResult Delete(string id)
        {
            var viewModel = _service.GetAircraft(id);

            _service.DeleteAircraft(id);

            return RedirectToAction("List", new { parentId = viewModel.AircraftModelId });
        }
    }
}
