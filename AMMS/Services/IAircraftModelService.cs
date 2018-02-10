using AMMS.Models.ViewModels;
using System.Collections.Generic;

namespace AMMS.Services
{
    public interface IAircraftModelService
    {
        AircraftModelViewModel GetModel(string id);

        IEnumerable<AircraftModelViewModel> GetModels(string uic);

        IEnumerable<AircraftModelViewModel> GetAllModels();

        void SaveModel(AircraftModelViewModel viewModel);

        void UpdateModel(AircraftModelViewModel viewModel);

        void DeleteModel(string id);
    }
}
