using AMMS.Models;
using AMMS.Models.ViewModels;
using System.Collections.Generic;

namespace AMMS.Services
{
    public interface IRecordService
    {
        FlightViewModel GetFlightById(string id);
        IEnumerable<FlightViewModel> GetFlightsByAircraftId(string aircraftId);
        InspectionViewModel GetInspectionById(string id);
        IEnumerable<InspectionViewModel> GetInspectionsByAircraftId(string aircraftId);
        FaultViewModel GetFaultById(string id);
        IEnumerable<FaultViewModel> GetFaultsByAircraftId(string aircraftId);
        RelatedMaintenanceViewModel GetRelatedMaintenanceById(string id);
        IEnumerable<RelatedMaintenanceViewModel> GetRelatedMaintenanceByFaultId(string faultId);

        Aircraft GetAircraftById(string id);
        Unit GetUnitById(string id);
        AircraftModel GetAircraftModelById(string id);

        void CreateFlight(FlightViewModel flight);
        void UpdateFlight(FlightViewModel flight);
        void DeleteFlight(string id);

        void CreateInspection(InspectionViewModel inspection);
        void UpdateInspection(InspectionViewModel inspection);
        void DeleteInspection(string id);

        void CreateFault(FaultViewModel fault);
        void UpdateFault(FaultViewModel fault);
        void DeleteFault(string id);

        void CreateRelatedMaintenance(RelatedMaintenanceViewModel related);
        void UpdateRelatedMaintenance(RelatedMaintenanceViewModel related);
        void DeleteRelatedMaintenance(string id);
    }
}
