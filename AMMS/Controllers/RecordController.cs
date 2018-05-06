using AMMS.Models.ViewModels;
using AMMS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AMMS.Controllers
{
    [Authorize]
    public class RecordController : Controller
    {
        private readonly IMasterService _service;

        public RecordController(IMasterService service)
        {
            _service = service;
        }

        public IActionResult ListFlight(string parentId)
        {
            ViewBag.ParentId = parentId;
            ViewBag.SerialNumber = _service.GetAircraftById(parentId).SerialNumber;
            var viewModels = _service.GetFlightsByAircraftId(parentId);
            return View(viewModels);
        }

        [HttpGet]
        public IActionResult CreateFlight(string parentId)
        {
            var aircraft = _service.GetAircraftById(parentId);
            var unit = _service.GetUnitById(aircraft.UnitId);
            var model = _service.GetAircraftModelById(aircraft.AircraftModelId);

            ViewBag.Aircraft = aircraft;
            ViewBag.Unit = unit;
            ViewBag.AcftModel = model;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateFlight(FlightViewModel flight)
        {
            if (!ModelState.IsValid) return View(flight);

            _service.CreateFlight(flight);

            return RedirectToAction("ListFlight", new { parentId = flight.AircraftId });
        }

        public IActionResult DetailsFlight(string id)
        {
            var flight = _service.GetFlightById(id);

            return View(flight);
        }

        [HttpGet]
        public IActionResult EditFlight(string id)
        {
            var flight = _service.GetFlightById(id);

            return View(flight);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditFlight(FlightViewModel flight)
        {
            if (!ModelState.IsValid) return View(flight);

            _service.UpdateFlight(flight);

            return RedirectToAction("ListFlight", new { parentId = flight.AircraftId });
        }

        [HttpGet]
        public IActionResult DeleteFlight(string id)
        {
            var flight = _service.GetFlightById(id);

            return View(flight);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteFlight(FlightViewModel flight)
        {
            var parent = _service.GetFlightById(flight.Id).AircraftId;

            _service.DeleteFlight(flight.Id);

            return RedirectToAction("ListFlight", new { parentId = parent });
        }

        public IActionResult ListInspection(string parentId)
        {
            ViewBag.ParentId = parentId;
            ViewBag.SerialNumber = _service.GetAircraftById(parentId).SerialNumber;

            var viewModels = _service.GetInspectionsByAircraftId(parentId);
            return View(viewModels);
        }

        [HttpGet]
        public IActionResult CreateInspection(string parentId)
        {
            var aircraft = _service.GetAircraftById(parentId);
            var acftModel = _service.GetAircraftModelById(aircraft.AircraftModelId);

            ViewBag.Aircraft = aircraft;
            ViewBag.AcftModel = acftModel;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateInspection(InspectionViewModel inspection)
        {
            if (!ModelState.IsValid) return View(inspection);

            _service.CreateInspection(inspection);

            return RedirectToAction("ListInspection", new { parentId = inspection.AircraftId });
        }

        public IActionResult DetailsInspection(string id)
        {
            var inspection = _service.GetInspectionById(id);

            return View(inspection);
        }

        public IActionResult RefreshInspection(string id)
        {
            var insp = _service.GetInspectionById(id);
            var acft = _service.GetAircraftById(insp.AircraftId);

            insp.Update(acft.AcftHrs);
            insp.Id = null;

            _service.CreateInspection(insp);
            _service.DeleteInspection(id);

            return RedirectToAction("ListInspection", new { parentId = acft.Id });
        }

        [HttpGet]
        public IActionResult EditInspection(string id)
        {
            var inspection = _service.GetInspectionById(id);

            return View(inspection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditInspection(InspectionViewModel inspection)
        {
            if (!ModelState.IsValid) return View(inspection);

            _service.UpdateInspection(inspection);

            return RedirectToAction("ListInspection", new { parentId = inspection.AircraftId });
        }

        [HttpGet]
        public IActionResult DeleteInspection(string id)
        {
            var inspection = _service.GetInspectionById(id);

            return View(inspection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteInspection(InspectionViewModel inspection)
        {
            var parent = _service.GetInspectionById(inspection.Id).AircraftId;

            _service.DeleteInspection(inspection.Id);

            return RedirectToAction("ListInspection", new { parentId = parent });
        }

        public IActionResult ListOpenFault(string parentId)
        {
            ViewBag.ParentId = parentId;
            var aircraft = _service.GetAircraftById(parentId);
            var model = _service.GetAircraftModelById(aircraft.AircraftModelId);
            ViewBag.SerialNumber = aircraft.SerialNumber;
            ViewBag.Model = model.Mds;
            var faults = _service.GetFaultsByAircraftId(parentId).Where(f => f.isOpen());

            return View(faults);
        }

        public IActionResult ListClosedFault(string parentId)
        {
            ViewBag.ParentId = parentId;
            var aircraft = _service.GetAircraftById(parentId);
            var model = _service.GetAircraftModelById(aircraft.AircraftModelId);
            ViewBag.SerialNumber = aircraft.SerialNumber;
            ViewBag.Model = model.Mds;
            var faults = _service.GetFaultsByAircraftId(parentId).Where(f => f.isClosed());

            return View(faults);
        }

        [HttpGet]
        public IActionResult CreateFault(string parentId)
        {
            var aircraft = _service.GetAircraftById(parentId);
            var acftModel = _service.GetAircraftModelById(aircraft.AircraftModelId);

            ViewBag.Aircraft = aircraft;
            ViewBag.AcftModel = acftModel;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateFault(FaultViewModel fault)
        {
            if (!ModelState.IsValid) return View(fault);

            _service.CreateFault(fault);

            return RedirectToAction("ListOpenFault", new { parentId = fault.AircraftId });
        }

        public IActionResult DetailsFault(string id)
        {
            var fault = _service.GetFaultById(id);

            return View(fault);
        }

        [HttpGet]
        public IActionResult EditFault(string id)
        {
            var fault = _service.GetFaultById(id);
            var userId = _service.GetCurrentUser(User).Id;
            ViewBag.PID = _service.GetUserById(userId).Pid;
            ViewBag.Lock = _service.GetRelatedMaintenanceByFaultId(id).Any(r => r.isOpen());

            return View(fault);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditFault(FaultViewModel fault)
        {
            if (!ModelState.IsValid) return View(fault);
            if ((!fault.isFull() && !fault.isEmpty()) || !fault.isValidTI())
            {
                ModelState.AddModelError("Incomplete", "Incomplete Form");
                return View(fault);
            }

            _service.UpdateFault(fault);

            return RedirectToAction("ListOpenFault", new { parentId = fault.AircraftId });
        }

        [HttpGet]
        public IActionResult DeleteFault(string id)
        {
            var fault = _service.GetFaultById(id);

            return View(fault);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteFault(FaultViewModel fault)
        {
            var parent = _service.GetFaultById(fault.Id).AircraftId;

            _service.DeleteFault(fault.Id);

            return RedirectToAction("ListOpenFault", new { parentId = parent });
        }

        public IActionResult ListRelatedMaintenance(string parentId)
        {
            ViewBag.ParentId = parentId;
            ViewBag.FaultText = _service.GetFaultById(parentId).FaultText;
            ViewBag.Lock = _service.GetFaultById(parentId).isFull();
            var related = _service.GetRelatedMaintenanceByFaultId(parentId);

            return View(related);
        }

        [HttpGet]
        public IActionResult CreateRelatedMaintenance(string parentId)
        {
            var fault = _service.GetFaultById(parentId);
            var aircraft = _service.GetAircraftById(fault.AircraftId);
            ViewBag.Fault = fault;
            ViewBag.Aircraft = aircraft;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateRelatedMaintenance(RelatedMaintenanceViewModel related)
        {
            if (!ModelState.IsValid) return View(related);

            _service.CreateRelatedMaintenance(related);

            return RedirectToAction("ListRelatedMaintenance", new { parentId = related.FaultId });
        }

        public IActionResult DetailsRelatedMaintenance(string id)
        {
            var related = _service.GetRelatedMaintenanceById(id);

            return View(related);
        }

        [HttpGet]
        public IActionResult EditRelatedMaintenance(string id)
        {
            var related = _service.GetRelatedMaintenanceById(id);
            var userId = _service.GetCurrentUser(User).Id;
            ViewBag.PID = _service.GetUserById(userId).Pid;

            return View(related);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditRelatedMaintenance(RelatedMaintenanceViewModel related)
        {
            if (!ModelState.IsValid) return View(related);
            if ((!related.isFull() && !related.isEmpty()) || !related.isValidTI())
            {
                ModelState.AddModelError("Incomplete", "Incomplete Form");
                return View(related);
            }

            _service.UpdateRelatedMaintenance(related);

            return RedirectToAction("ListRelatedMaintenance", new { parentId = related.FaultId });
        }

        [HttpGet]
        public IActionResult DeleteRelatedMaintenance(string id)
        {
            var related = _service.GetRelatedMaintenanceById(id);

            return View(related);
        }

        //TODO: Recursive delete from fault
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteRelatedMaintenance(RelatedMaintenanceViewModel related)
        {
            var parent = _service.GetRelatedMaintenanceById(related.Id).FaultId;

            _service.DeleteRelatedMaintenance(related.Id);

            return RedirectToAction("ListRelatedMaintenance", new { parentId = parent });
        }
    }
}
