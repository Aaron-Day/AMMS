using AMMS.Data;
using AMMS.Models;
using System.Collections.Generic;
using System.Linq;

namespace AMMS.Repository
{
    public class RecordRepository : IRecordRepository
    {
        private readonly ApplicationDbContext _context;

        public RecordRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Aircraft GetAircraftById(string id)
        {
            return _context.Aircraft.Find(id);
        }

        public Unit GetUnitById(string id)
        {
            return _context.Units.Find(id);
        }

        public AircraftModel GetAircraftModelById(string id)
        {
            return _context.AircraftModels.Find(id);
        }

        /**********************************************************************************/

        public Flight GetFlightById(string id)
        {
            return _context.Flights.Find(id);
        }

        public IEnumerable<Flight> GetFlightsByAircraftId(string aircraftId)
        {
            return _context.Flights.Where(flt => flt.AircraftId == aircraftId);
        }

        public Inspection GetInspectionById(string id)
        {
            return _context.Inspections.Find(id);
        }

        public IEnumerable<Inspection> GetInspectionsByAircraftId(string id)
        {
            return _context.Inspections.Where(insp => insp.AircraftId == id && insp.CompletedAt == null).ToList();
        }

        public Fault GetFaultById(string id)
        {
            return _context.Faults.Find(id);
        }

        public IEnumerable<Fault> GetFaultsByAircraftId(string id)
        {
            return _context.Faults.Where(fault => fault.AircraftId == id);
        }

        public RelatedMaintenance GetRelatedMaintenanceById(string id)
        {
            return _context.RelatedMaintenance.Find(id);
        }

        public IEnumerable<RelatedMaintenance> GetRelatedMaintenanceByFaultId(string id)
        {
            return _context.RelatedMaintenance.Where(maint => maint.FaultId == id);
        }

        /**********************************************************************************/

        public void CreateFlight(Flight flight)
        {
            _context.Flights.Add(flight);
            _context.SaveChanges();
        }

        public void UpdateFlight(Flight flight)
        {
            _context.SaveChanges();
        }

        public void DeleteFlight(string id)
        {
            var flight = _context.Flights.Find(id);

            if (flight == null) return;

            _context.Flights.Remove(flight);
            _context.SaveChanges();
        }

        public void CreateInspection(Inspection inspection)
        {
            _context.Inspections.Add(inspection);
            _context.SaveChanges();
        }

        public void UpdateInspection(Inspection inspection)
        {
            _context.SaveChanges();
        }

        public void DeleteInspection(string id)
        {
            var insp = _context.Inspections.Find(id);

            if (insp == null) return;

            _context.Inspections.Remove(insp);
            _context.SaveChanges();
        }

        public void CreateFault(Fault fault)
        {
            _context.Faults.Add(fault);
            _context.SaveChanges();
        }

        public void UpdateFault(Fault fault)
        {
            _context.SaveChanges();
        }

        public void DeleteFault(string id)
        {
            var f = _context.Faults.Find(id);

            if (f == null) return;

            _context.Faults.Remove(f);
            _context.SaveChanges();
        }

        public void CreateRelatedMaintenance(RelatedMaintenance related)
        {
            _context.RelatedMaintenance.Add(related);
            _context.SaveChanges();
        }

        public void UpdateRelatedMaintenance(RelatedMaintenance related)
        {
            _context.SaveChanges();
        }

        public void DeleteRelatedMaintenance(string id)
        {
            var related = _context.RelatedMaintenance.Find(id);

            if (related == null) return;

            _context.RelatedMaintenance.Remove(related);
            _context.SaveChanges();
        }
    }
}
