using AMMS.Models.ViewModels;
using System.Collections.Generic;

namespace AMMS.Services
{
    public interface IMasterService
    {
        AircraftModelViewModel GetAircraftModelById(string id);
        AircraftModelViewModel GetAircraftModelByMds(string mds);
        IEnumerable<AircraftModelViewModel> GetAircraftModelsByUIC(string uic);
        IEnumerable<AircraftModelViewModel> GetAllAircraftModels();

        void CreateAircraftModel(AircraftModelViewModel model);
        void UpdateAircraftModel(AircraftModelViewModel model);
        void DeleteAircraftModel(string id);

        AircraftViewModel GetAircraftById(string id);
        IEnumerable<AircraftViewModel> GetAircraftByUnitId(string id);
        IEnumerable<AircraftViewModel> GetAircraftByUIC(string uic);
        IEnumerable<AircraftViewModel> GetAllAircraft();

        void CreateAircraft(AircraftViewModel aircraft);
        void UpdateAircraft(AircraftViewModel aircraft);
        void DeleteAircraft(string id);

        string GetUnitId(string id);
        UnitViewModel GetUnitById(string id);
        IEnumerable<UnitViewModel> GetAllUnits();
    }
}
