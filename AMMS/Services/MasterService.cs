using AMMS.Models;
using AMMS.Models.ViewModels;
using AMMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AMMS.Services
{
    public class MasterService : IMasterService
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(MasterService));

        private readonly IMasterRepository _repository;

        public MasterService(IMasterRepository repository)
        {
            _repository = repository;
        }



        public AircraftModelViewModel GetAircraftModelById(string id)
        {
            try
            {
                return MapToAircraftModelViewModel(_repository.GetAircraftModelById(id));
            }
            catch (Exception e)
            {
                Log.Error("GetAircraftModelById: Failed!", e);
                throw;
            }
        }

        public AircraftModelViewModel GetAircraftModelByMds(string mds)
        {
            try
            {
                return MapToAircraftModelViewModel(_repository.GetAircraftModelByMds(mds));
            }
            catch (Exception e)
            {
                Log.Error("GetAircraftModelByMds: Failed!", e);
                throw;
            }
        }

        public IEnumerable<AircraftModelViewModel> GetAircraftModelsByUIC(string uic)
        {
            try
            {
                var models = _repository.GetAircraftModelsByUIC(uic);
                return models.Select(MapToAircraftModelViewModel).ToList();
            }
            catch (Exception e)
            {
                Log.Error("GetAircraftModelsByUIC: Failed!", e);
                throw;
            }
        }

        public IEnumerable<AircraftModelViewModel> GetAllAircraftModels()
        {
            try
            {
                var models = _repository.GetAllAircraftModels();
                return models.Select(MapToAircraftModelViewModel).ToList();
            }
            catch (Exception e)
            {
                Log.Error("GetAllAircraftModels: Failed!", e);
                throw;
            }
        }



        public void CreateAircraftModel(AircraftModelViewModel model)
        {
            try
            {
                _repository.CreateAircraftModel(MapToAircraftModel(model));
            }
            catch (Exception e)
            {
                Log.Error("CreateAircraftModel: Failed!", e);
                throw;
            }
        }

        public void UpdateAircraftModel(AircraftModelViewModel model)
        {
            try
            {
                var mod = _repository.GetAircraftModelById(model.Id);
                CopyToAircraftModel(model, mod);

                _repository.UpdateAircraftModel(mod);
            }
            catch (Exception e)
            {
                Log.Error("UpdateAircraftModel: Failed!", e);
                throw;
            }
        }

        public void DeleteAircraftModel(string id)
        {
            try
            {
                _repository.DeleteAircraftModel(id);
            }
            catch (Exception e)
            {
                Log.Error("DeleteAircraftModel: Failed!", e);
                throw;
            }
        }



        public AircraftViewModel GetAircraftById(string id)
        {
            try
            {
                return MapToAircraftViewModel(_repository.GetAircraftById(id));
            }
            catch (Exception e)
            {
                Log.Error("GetAircraftById: Failed!", e);
                throw;
            }
        }

        public IEnumerable<AircraftViewModel> GetAircraftByUnitId(string id)
        {
            try
            {
                var aircraft = _repository.GetAircraftByUnitId(id);
                return aircraft.Select(MapToAircraftViewModel).ToList();
            }
            catch (Exception e)
            {
                Log.Error("GetAircraftByUnitId: Failed!", e);
                throw;
            }
        }

        public IEnumerable<AircraftViewModel> GetAircraftByUIC(string uic)
        {
            try
            {
                var aircraft = _repository.GetAircraftByUIC(uic);
                return aircraft.Select(MapToAircraftViewModel).ToList();
            }
            catch (Exception e)
            {
                Log.Error("GetAircraftByUIC: Failed!", e);
                throw;
            }
        }

        public IEnumerable<AircraftViewModel> GetAllAircraft()
        {
            try
            {
                var aircraft = _repository.GetAllAircraft();
                return aircraft.Select(MapToAircraftViewModel).ToList();
            }
            catch (Exception e)
            {
                Log.Error("GetAllAircraft: Failed!", e);
                throw;
            }
        }



        public void CreateAircraft(AircraftViewModel aircraft)
        {
            try
            {
                _repository.CreateAircraft(MapToAircraft(aircraft));
            }
            catch (Exception e)
            {
                Log.Error("CreateAircraft: Failed!", e);
                throw;
            }
        }

        public void UpdateAircraft(AircraftViewModel aircraft)
        {
            try
            {
                var acft = _repository.GetAircraftById(aircraft.Id);
                CopyToAircraft(aircraft, acft);

                _repository.UpdateAircraft(acft);
            }
            catch (Exception e)
            {
                Log.Error("UpdateAircraft: Failed!", e);
                throw;
            }
        }

        public void DeleteAircraft(string id)
        {
            try
            {
                _repository.DeleteAircraft(id);
            }
            catch (Exception e)
            {
                Log.Error("DeleteAircraft: Failed!", e);
                throw;
            }
        }



        public string GetUnitId(string id)
        {
            try
            {
                return _repository.GetUnitId(id);
            }
            catch (Exception e)
            {
                Log.Error("GetUnitId: Failed!", e);
                throw;
            }
        }

        public UnitViewModel GetUnitById(string id)
        {
            try
            {
                return MapToUnitViewModel(_repository.GetUnitById(id));
            }
            catch (Exception e)
            {
                Log.Error("GetUnitById: Failed!", e);
                throw;
            }
        }

        public IEnumerable<UnitViewModel> GetAllUnits()
        {
            try
            {
                var units = _repository.GetAllUnits();
                return units.Select(MapToUnitViewModel).ToList();
            }
            catch (Exception e)
            {
                Log.Error("GetAllUnits: Failed!", e);
                throw;
            }
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

        private static AircraftModel MapToAircraftModel(AircraftModelViewModel model)
        {
            return new AircraftModel
            {
                Id = model.Id,
                Eic = model.Eic,
                Mds = model.Mds,
                Name = model.Name,
                Nsn = model.Nsn
            };
        }

        private static void CopyToAircraftModel(AircraftModelViewModel view, AircraftModel model)
        {
            model.Id = view.Id;
            model.Eic = view.Eic;
            model.Mds = view.Mds;
            model.Name = view.Name;
            model.Nsn = view.Nsn;
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

        private static Aircraft MapToAircraft(AircraftViewModel aircraft)
        {
            return new Aircraft
            {
                Id = aircraft.Id,
                AcftHrs = aircraft.AcftHrs,
                SerialNumber = aircraft.SerialNumber,
                AircraftModelId = aircraft.AircraftModelId,
                UnitId = aircraft.UnitId
            };
        }

        private static void CopyToAircraft(AircraftViewModel view, Aircraft aircraft)
        {
            aircraft.Id = view.Id;
            aircraft.AcftHrs = view.AcftHrs;
            aircraft.SerialNumber = view.SerialNumber;
            aircraft.AircraftModelId = view.AircraftModelId;
            aircraft.UnitId = view.UnitId;
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
