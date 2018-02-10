using AMMS.Models;
using System.Collections.Generic;

namespace AMMS.Repository
{
    public interface IAircraftModelRepository
    {
        AircraftModel GetModel(string id);

        IEnumerable<AircraftModel> GetModels(string uic);

        IEnumerable<AircraftModel> GetAllModels();

        void SaveModel(AircraftModel model);

        void UpdateModel(AircraftModel model);

        void DeleteModel(string id);
    }
}
