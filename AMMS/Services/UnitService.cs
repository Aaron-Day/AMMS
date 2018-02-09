using AMMS.Models;
using AMMS.Models.ViewModels;
using AMMS.Repository;
using System.Collections.Generic;

namespace AMMS.Services
{
    public class UnitService : IUnitService
    {
        private readonly IUnitRepository _repository;

        public UnitService(IUnitRepository repository)
        {
            _repository = repository;
        }

        public UnitViewModel GetUnit(string id)
        {
            var unit = _repository.GetUnit(id);
            return MapToUnitViewModel(unit);
        }

        public IEnumerable<UnitViewModel> GetUnits()
        {
            var unitViewModels = new List<UnitViewModel>();

            var units = _repository.GetUnits();

            foreach (var unit in units)
            {
                unitViewModels.Add(MapToUnitViewModel(unit));
            }

            return unitViewModels;
        }

        public void SaveUnit(UnitViewModel viewModel)
        {
            var unit = MapToUnit(viewModel);

            _repository.SaveUnit(unit);
        }

        public void UpdateUnit(UnitViewModel viewModel)
        {
            var unit = _repository.GetUnit(viewModel.Id);

            CopyToUnit(viewModel, unit);

            _repository.UpdateUnit(unit);
        }

        public void DeleteUnit(string id)
        {
            _repository.DeleteUnit(id);
        }

        //---------------------------------------------------------//

        private Unit MapToUnit(UnitViewModel viewModel)
        {
            return new Unit
            {
                Id = viewModel.Id,
                CompanyName = viewModel.CompanyName,
                Station = viewModel.Station,
                UIC = viewModel.UIC,
                UnitName = viewModel.UnitName,
                UnitPhone = viewModel.UnitPhone
            };
        }

        public UnitViewModel MapToUnitViewModel(Unit unit)
        {
            return new UnitViewModel
            {
                Id = unit.Id,
                CompanyName = unit.CompanyName,
                Station = unit.Station,
                UIC = unit.UIC,
                UnitName = unit.UnitName,
                UnitPhone = unit.UnitPhone
            };
        }

        private void CopyToUnit(UnitViewModel viewModel, Unit unit)
        {
            unit.Id = viewModel.Id;
            unit.CompanyName = viewModel.CompanyName;
            unit.Station = viewModel.Station;
            unit.UIC = viewModel.UIC;
            unit.UnitName = viewModel.UnitName;
            unit.UnitPhone = viewModel.UnitPhone;
        }
    }
}
