using AMMS.Models;
using AMMS.Models.ViewModels;
using AMMS.Repository;
using System.Collections.Generic;
using System.Linq;

namespace AMMS.Services
{
    public class AircraftModelService : IAircraftModelService
    {
        private readonly IAircraftModelRepository _repository;

        public AircraftModelService(IAircraftModelRepository repository)
        {
            _repository = repository;
        }

        public AircraftModelViewModel GetModel(string id)
        {
            var model = _repository.GetModel(id);

            return MapToAircraftModelViewModel(model);
        }

        public IEnumerable<AircraftModelViewModel> GetModels(string uic)
        {
            var models = _repository.GetModels(uic);

            return models.Select(MapToAircraftModelViewModel).ToList();
        }

        public IEnumerable<AircraftModelViewModel> GetAllModels()
        {
            var models = _repository.GetAllModels();

            return models.Select(MapToAircraftModelViewModel).ToList();
        }

        public void SaveModel(AircraftModelViewModel viewModel)
        {
            _repository.SaveModel(MapToAircraftModel(viewModel));
        }

        public void UpdateModel(AircraftModelViewModel viewModel)
        {
            var model = _repository.GetModel(viewModel.Id);
            CopyToAircraftModel(viewModel, model);

            _repository.UpdateModel(model);
        }

        public void DeleteModel(string id)
        {
            _repository.DeleteModel(id);
        }

        //---------------------------------------------------------------------------//

        private static AircraftModel MapToAircraftModel(AircraftModelViewModel viewModel)
        {
            return new AircraftModel
            {
                Id = viewModel.Id,
                Eic = viewModel.Eic,
                Mds = viewModel.Mds,
                Name = viewModel.Name,
                Nsn = viewModel.Nsn
            };
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

        private static void CopyToAircraftModel(AircraftModelViewModel viewModel, AircraftModel model)
        {
            model.Id = viewModel.Id;
            model.Eic = viewModel.Eic;
            model.Mds = viewModel.Mds;
            model.Name = viewModel.Name;
            model.Nsn = viewModel.Nsn;
        }
    }
}
