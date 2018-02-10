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

        // TODO: Modify view to remove create, edit and delete options for all but PC and QC
        public IActionResult List(string parentId)
        {
            /* accepts user, unit or aircraft id
             * and returns all aircraft for the unit
             * (user's assigned unit
             * or aircraft's owning unit)
             */
            parentId = _service.GetUnitId(parentId);

            TempData["ParentId"] = parentId;

            var viewModels = _service.GetAllAircraft(parentId);

            return View(viewModels);
        }

        // <C>RUD
        [HttpGet]
        // TODO: Restrict access to PC anc QC (users that have an assigned unit)
        public IActionResult Create(string parentId)
        {
            TempData["ParentId"] = parentId;

            ViewBag.Models = _service.GetAllModels();

            return View();
        }

        [HttpPost]
        // TODO: Restrict access to PC anc QC (users that have an assigned unit)
        // TODO: Validate antiforgery token similar to account controler HttpPost methods
        public IActionResult Create(AircraftViewModel viewModel)
        {
            if (!ModelState.IsValid) return View();

            _service.SaveAircraft(viewModel);

            return RedirectToAction("List", new { parentId = viewModel.UnitId });
        }

        // C<R>UD
        // TODO: Modify view to remove edit option for all but PC and QC
        public IActionResult Details(string id)
        {
            var viewModel = _service.GetAircraft(id);

            return View(viewModel);
        }

        // CR<U>D
        [HttpGet]
        // TODO: Restrict access to PC anc QC (users that have an assigned unit)
        public IActionResult Edit(string id)
        {
            var viewModel = _service.GetAircraft(id);

            ViewBag.Models = _service.GetAllModels();

            //TODO: Add units to a viewbab so aircraft can be transfered to another unit

            return View(viewModel);
        }

        [HttpPost]
        // TODO: Restrict access to PC anc QC (users that have an assigned unit)
        // TODO: Validate antiforgery token similar to account controler HttpPost methods
        public IActionResult Edit(AircraftViewModel viewModel)
        {
            if (!ModelState.IsValid) return View();

            _service.UpdateAircraft(viewModel);

            return RedirectToAction("List", new { parentId = viewModel.UnitId });
        }

        // CRU<D>
        [HttpGet]
        // TODO: Restrict access to PC anc QC (users that have an assigned unit)
        public IActionResult Delete(string id)
        {
            var viewModel = _service.GetAircraft(id);

            return View(viewModel);
        }

        [HttpPost]
        // TODO: Restrict access to PC anc QC (users that have an assigned unit)
        // TODO: Validate antiforgery token similar to account controler HttpPost methods
        public IActionResult Delete(AircraftViewModel viewModel)
        {
            _service.DeleteAircraft(viewModel.Id);

            return RedirectToAction("List", new { parentId = viewModel.UnitId });
        }
    }
}
