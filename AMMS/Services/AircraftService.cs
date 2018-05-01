using AMMS.Models;
using AMMS.Models.ViewModels;
using AMMS.Repository;
using System.Collections.Generic;
using System.Linq;

namespace AMMS.Services
{
    public class AircraftService : IAircraftService
    {
        private readonly IAircraftRepository _repository;

        public AircraftService(IAircraftRepository repository)
        {
            _repository = repository;
        }

        public AircraftViewModel GetAircraft(string id)
        {
            var model = _repository.GetAircraft(id);

            return MapToAircraftViewModel(model);
        }

        public IEnumerable<AircraftViewModel> GetAllAircraft(string parentId)
        {
            var models = _repository.GetAllAircraft(parentId);

            return models.Select(MapToAircraftViewModel).ToList();
        }

        public void SaveAircraft(AircraftViewModel viewModel)
        {
            _repository.SaveAircraft(MapToAircraft(viewModel));
        }

        public void UpdateAircraft(AircraftViewModel viewModel)
        {
            var aircraft = _repository.GetAircraft(viewModel.Id);
            CopyToAircraft(viewModel, aircraft);

            _repository.UpdateAircraft(aircraft);
        }

        public void DeleteAircraft(string id)
        {
            _repository.DeleteAircraft(id);
        }

        public string GetUnitId(string id)
        {
            return _repository.GetUnitId(id);
        }

        public UnitViewModel GetUnitById(string id)
        {
            return MapToUnitViewModel(_repository.GetUnitById(id));
        }

        public IEnumerable<UnitViewModel> GetAllUnits()
        {
            var units = _repository.GetAllUnits();
            return units.Select(MapToUnitViewModel).ToList();
        }

        public IEnumerable<AircraftModelViewModel> GetAllModels()
        {
            var models = _repository.GetAllModels();

            return models.Select(MapToAircraftModelViewModel).ToList();
        }

        //---------------------------------------------------------------------------//

        private static Aircraft MapToAircraft(AircraftViewModel viewModel)
        {
            return new Aircraft
            {
                Id = viewModel.Id,
                AcftHrs = viewModel.AcftHrs,
                SerialNumber = viewModel.SerialNumber,
                AircraftModelId = viewModel.AircraftModelId,
                UnitId = viewModel.UnitId
            };
        }

        private static AircraftViewModel MapToAircraftViewModel(Aircraft aircraft)
        {
            return new AircraftViewModel
            {
                Id = aircraft.Id,
                AcftHrs = aircraft.AcftHrs,
                SerialNumber = aircraft.SerialNumber,
                AircraftModelId = aircraft.AircraftModelId,
                UnitId = aircraft.UnitId
            };
        }

        private static void CopyToAircraft(AircraftViewModel viewModel, Aircraft aircraft)
        {
            aircraft.Id = viewModel.Id;
            aircraft.AcftHrs = viewModel.AcftHrs;
            aircraft.SerialNumber = viewModel.SerialNumber;
            aircraft.AircraftModelId = viewModel.AircraftModelId;
            aircraft.UnitId = viewModel.UnitId;
        }

        private static AircraftModelViewModel MapToAircraftModelViewModel(AircraftModel model)
        {
            return new AircraftModelViewModel
            {
                Id = model.Id,
                Eic = model.Eic,
                Mds = model.Mds,
                Name = model.Name,
                Nsn = model.Nsn
            };
        }

        private static UnitViewModel MapToUnitViewModel(Unit unit)
        {
            return new UnitViewModel
            {
                Id = unit.Id,
                UIC = unit.UIC,
                UnitName = unit.UnitName,
                CompanyName = unit.CompanyName,
                UnitPhone = unit.UnitPhone,
                Station = unit.Station
            };
        }
    }
}
