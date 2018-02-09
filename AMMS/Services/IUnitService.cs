using AMMS.Models.ViewModels;
using System.Collections.Generic;

namespace AMMS.Services
{
    public interface IUnitService
    {
        UnitViewModel GetUnit(string id);

        IEnumerable<UnitViewModel> GetUnits();

        void SaveUnit(UnitViewModel viewModel);

        void UpdateUnit(UnitViewModel viewModel);

        void DeleteUnit(string id);
    }
}
