using AMMS.Models;
using AMMS.Models.ViewModels;
using AMMS.Repository;
using System.Collections.Generic;

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
            var viewModels = new List<AircraftViewModel>();

            var models = _repository.GetAllAircraft(parentId);
            foreach (var model in models)
            {
                viewModels.Add(MapToAircraftViewModel(model));
            }

            return viewModels;
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

        //---------------------------------------------------------------------------//

        private Aircraft MapToAircraft(AircraftViewModel viewModel)
        {
            return new Aircraft
            {
                Id = viewModel.Id,
                AcftHrs = viewModel.AcftHrs,
                SerialNumber = viewModel.SerialNumber,
                AircraftModelId = viewModel.AircraftModelId
            };
        }

        private AircraftViewModel MapToAircraftViewModel(Aircraft aircraft)
        {
            return new AircraftViewModel
            {
                Id = aircraft.Id,
                AcftHrs = aircraft.AcftHrs,
                SerialNumber = aircraft.SerialNumber,
                AircraftModelId = aircraft.AircraftModelId
            };
        }

        private void CopyToAircraft(AircraftViewModel viewModel, Aircraft aircraft)
        {
            aircraft.Id = viewModel.Id;
            aircraft.AcftHrs = viewModel.AcftHrs;
            aircraft.SerialNumber = viewModel.SerialNumber;
            aircraft.AircraftModelId = viewModel.AircraftModelId;
        }
    }
}
