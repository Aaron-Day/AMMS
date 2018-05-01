using AMMS.Models;
using System.Collections.Generic;

namespace AMMS.Repository
{
    public interface IAircraftRepository
    {
        Aircraft GetAircraft(string id);

        IEnumerable<Aircraft> GetAllAircraft(string parentId);

        void SaveAircraft(Aircraft aircraft);

        void UpdateAircraft(Aircraft aircraft);

        void DeleteAircraft(string id);

        string GetUnitId(string id);

        Unit GetUnitById(string id);

        IEnumerable<Unit> GetAllUnits();

        IEnumerable<AircraftModel> GetAllModels();
    }
}
