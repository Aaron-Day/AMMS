using AMMS.Data;
using AMMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AMMS.Repository
{
    public class MasterRepository : IMasterRepository
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(MasterRepository));

        private readonly ApplicationDbContext _context;

        public MasterRepository(ApplicationDbContext context)
        {
            _context = context;
        }



        public AircraftModel GetAircraftModelById(string id)
        {
            try
            {
                return _context.AircraftModels.Find(id);
            }
            catch (Exception e)
            {
                Log.Error("GetAircraftModelById: Failed!", e);
                throw;
            }
        }

        public AircraftModel GetAircraftModelByMds(string mds)
        {
            try
            {
                return _context.AircraftModels.FirstOrDefault(m => m.Mds == mds);
            }
            catch (Exception e)
            {
                Log.Error("GetAircraftModelByMds: Failed!", e);
                throw;
            }
        }

        public IEnumerable<AircraftModel> GetAircraftModelsByUIC(string uic)
        {
            try
            {
                if (uic == null || uic == "ADMIN") return GetAllAircraftModels();

                var unitId = _context.Units.FirstOrDefault(u => u.UIC == uic)?.Id;
                var models = _context.Aircraft.Where(a => a.UnitId == unitId).GroupBy(x => x.AircraftModelId).ToList();
                return models.Select(acft => _context.AircraftModels.Find(acft)).ToList();
            }
            catch (Exception e)
            {
                Log.Error("GetAircraftModelsByUIC: Failed!", e);
                throw;
            }
        }

        public IEnumerable<AircraftModel> GetAllAircraftModels()
        {
            try
            {
                return _context.AircraftModels.ToList();
            }
            catch (Exception e)
            {
                Log.Error("GetAllAircraftModels: Failed!", e);
                throw;
            }
        }



        public void CreateAircraftModel(AircraftModel model)
        {
            _context.AircraftModels.Add(model);
            _context.SaveChanges();
        }

        public void UpdateAircraftModel(AircraftModel model)
        {
            _context.SaveChanges();
        }

        public void DeleteAircraftModel(string id)
        {
            var model = _context.AircraftModels.Find(id);

            if (model == null) return;

            _context.AircraftModels.Remove(model);
            _context.SaveChanges();
        }



        public Aircraft GetAircraftById(string id)
        {
            try
            {
                return _context.Aircraft.Find(id);
            }
            catch (Exception e)
            {
                Log.Error("GetAircraftById: Failed!", e);
                throw;
            }
        }

        public IEnumerable<Aircraft> GetAircraftByUnitId(string id)
        {
            try
            {
                return _context.Aircraft.Where(m => m.UnitId == id).ToList();
            }
            catch (Exception e)
            {
                Log.Error("GetAircraftByUnitId: Failed!", e);
                throw;
            }
        }

        public IEnumerable<Aircraft> GetAircraftByUIC(string uic)
        {
            try
            {
                var unitId = _context.Units.FirstOrDefault(u => u.UIC == uic)?.Id;
                return uic == "ADMIN"
                    ? GetAllAircraft()
                    : _context.Aircraft.Where(m => m.UnitId == unitId).ToList();
            }
            catch (Exception e)
            {
                Log.Error("GetAircraftByUIC: Failed!", e);
                throw;
            }
        }

        public IEnumerable<Aircraft> GetAllAircraft()
        {
            try
            {
                return _context.Aircraft.ToList();
            }
            catch (Exception e)
            {
                Log.Error("GetAllAircraft: Failed!", e);
                throw;
            }
        }



        public void CreateAircraft(Aircraft aircraft)
        {
            _context.Aircraft.Add(aircraft);
            _context.SaveChanges();
        }

        public void UpdateAircraft(Aircraft aircraft)
        {
            _context.SaveChanges();
        }

        public void DeleteAircraft(string id)
        {
            var aircraft = _context.Aircraft.Find(id);

            if (aircraft == null) return;

            _context.Aircraft.Remove(aircraft);
            _context.SaveChanges();
        }



        public string GetUnitId(string id)
        {
            // accepts unit, user or acft id
            try
            {
                if (_context.Units.Find(id) != null) return id;
                var acft = _context.Aircraft.Find(id);
                if (acft != null) return acft.UnitId;
                var user = _context.Users.Find(id);
                return user != null
                    ? _context.Units.SingleOrDefault(u => u.UIC == user.AssignedUnit)?.Id
                    : string.Empty;
            }
            catch (Exception e)
            {
                Log.Error("GetUnitId: Failed!", e);
                throw;
            }
        }

        public Unit GetUnitById(string id)
        {
            try
            {
                return _context.Units.Find(id);
            }
            catch (Exception e)
            {
                Log.Error("GetUnitById: Failed!", e);
                throw;
            }
        }

        public IEnumerable<Unit> GetAllUnits()
        {
            try
            {
                return _context.Units.ToList();
            }
            catch (Exception e)
            {
                Log.Error("GetAllUnits: Failed!", e);
                throw;
            }
        }
    }
}
