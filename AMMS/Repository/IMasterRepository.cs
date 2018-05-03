using AMMS.Models;
using System.Collections.Generic;

namespace AMMS.Repository
{
    public interface IMasterRepository
    {
        AircraftModel GetAircraftModelById(string id);
        AircraftModel GetAircraftModelByMds(string mds);
        IEnumerable<AircraftModel> GetAircraftModelsByUIC(string uic);
        IEnumerable<AircraftModel> GetAllAircraftModels();

        void CreateAircraftModel(AircraftModel model);
        void UpdateAircraftModel(AircraftModel model);
        void DeleteAircraftModel(string id);

        Aircraft GetAircraftById(string id);
        IEnumerable<Aircraft> GetAircraftByUnitId(string id);
        IEnumerable<Aircraft> GetAircraftByUIC(string uic);
        IEnumerable<Aircraft> GetAllAircraft();

        void CreateAircraft(Aircraft aircraft);
        void UpdateAircraft(Aircraft aircraft);
        void DeleteAircraft(string id);

        string GetUnitId(string id);
        Unit GetUnitById(string id);
        IEnumerable<Unit> GetAllUnits();
    }
}
