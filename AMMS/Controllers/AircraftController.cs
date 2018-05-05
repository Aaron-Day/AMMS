using AMMS.Models.ViewModels;
using AMMS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMMS.Controllers
{
    [Authorize]
    public class AircraftController : Controller
    {
        private readonly IMasterService _service;

        public AircraftController(IMasterService service)
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
            ViewBag.Unit = _service.GetUnitById(parentId);
            ViewBag.Models = _service.GetAllAircraftModels();

            TempData["ParentId"] = parentId;

            var viewModels = _service.GetAircraftByUnitId(parentId);

            return View(viewModels);
        }

        // <C>RUD
        [HttpGet]
        [Authorize(Roles = "PC, QC")]
        public IActionResult Create(string parentId)
        {
            TempData["ParentId"] = parentId;

            ViewBag.Models = _service.GetAllAircraftModels();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "PC, QC")]
        public IActionResult Create(AircraftViewModel aircraft)
        {
            if (!ModelState.IsValid) return View(aircraft);

            _service.CreateAircraft(aircraft);

            return RedirectToAction("List", new { parentId = aircraft.UnitId });
        }

        // C<R>UD
        // TODO: Modify view to remove edit option for all but PC and QC
        public IActionResult Details(string id)
        {
            var aircraft = _service.GetAircraftById(id);

            return View(aircraft);
        }

        // CR<U>D
        [HttpGet]
        [Authorize(Roles = "PC, QC")]
        public IActionResult Edit(string id)
        {
            var aircraft = _service.GetAircraftById(id);

            ViewBag.Models = _service.GetAllAircraftModels();

            //TODO: Add units to a viewbag so aircraft can be transfered to another unit

            return View(aircraft);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "PC, QC")]
        public IActionResult Edit(AircraftViewModel aircraft)
        {
            if (!ModelState.IsValid) return View(aircraft);

            _service.UpdateAircraft(aircraft);

            return RedirectToAction("List", new { parentId = aircraft.UnitId });
        }

        // CRU<D>
        [HttpGet]
        [Authorize(Roles = "PC, QC")]
        public IActionResult Delete(string id)
        {
            var aircraft = _service.GetAircraftById(id);

            return View(aircraft);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "PC, QC")]
        public IActionResult Delete(AircraftViewModel aircraft)
        {
            _service.DeleteAircraft(aircraft.Id);

            return RedirectToAction("List", new { parentId = aircraft.UnitId });
        }
    }
}
