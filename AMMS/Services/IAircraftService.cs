using AMMS.Models.ViewModels;
using System.Collections.Generic;

namespace AMMS.Services
{
    public interface IAircraftService
    {
        AircraftViewModel GetAircraft(string id);

        IEnumerable<AircraftViewModel> GetAllAircraft(string parentId);

        void SaveAircraft(AircraftViewModel viewModel);

        void UpdateAircraft(AircraftViewModel viewModel);

        void DeleteAircraft(string id);

        string GetUnitId(string id);

        IEnumerable<AircraftModelViewModel> GetAllModels();
    }
}
