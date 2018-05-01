using AMMS.Models;
using System.Collections.Generic;

namespace AMMS.Repository
{
    public interface IRecordRepository
    {
        Aircraft GetAircraftById(string id);
        Unit GetUnitById(string id);
        AircraftModel GetAircraftModelById(string id);

        Flight GetFlightById(string id);
        IEnumerable<Flight> GetFlightsByAircraftId(string id);
        Inspection GetInspectionById(string id);
        IEnumerable<Inspection> GetInspectionsByAircraftId(string id);
        Fault GetFaultById(string id);
        IEnumerable<Fault> GetFaultsByAircraftId(string id);
        RelatedMaintenance GetRelatedMaintenanceById(string id);
        IEnumerable<RelatedMaintenance> GetRelatedMaintenanceByFaultId(string id);

        void CreateFlight(Flight flight);
        void UpdateFlight(Flight flight);
        void DeleteFlight(string id);

        void CreateInspection(Inspection inspection);
        void UpdateInspection(Inspection inspection);
        void DeleteInspection(string id);

        void CreateFault(Fault fault);
        void UpdateFault(Fault fault);
        void DeleteFault(string id);

        void CreateRelatedMaintenance(RelatedMaintenance related);
        void UpdateRelatedMaintenance(RelatedMaintenance related);
        void DeleteRelatedMaintenance(string id);
    }
}
