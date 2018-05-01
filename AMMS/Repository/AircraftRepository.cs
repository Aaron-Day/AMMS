using AMMS.Data;
using AMMS.Models;
using System.Collections.Generic;
using System.Linq;

namespace AMMS.Repository
{
    public class AircraftRepository : IAircraftRepository
    {
        private readonly ApplicationDbContext _context;

        public AircraftRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Aircraft GetAircraft(string id)
        {
            return _context.Aircraft.Find(id);
        }

        public IEnumerable<Aircraft> GetAllAircraft(string parentId)
        {
            return parentId == "ADMIN"
                ? _context.Aircraft.ToList()
                : _context.Aircraft.Where(m => m.UnitId == parentId).ToList();
        }

        public void SaveAircraft(Aircraft aircraft)
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
            if (_context.Units.Find(id) != null) return id;
            var acft = _context.Aircraft.Find(id);
            if (acft != null) return acft.UnitId;
            var user = _context.Users.Find(id);
            return user != null
                ? _context.Units.SingleOrDefault(u => u.UIC == user.AssignedUnit)?.Id
                : id;
        }

        public Unit GetUnitById(string id)
        {
            return _context.Units.Find(id);
        }

        public IEnumerable<Unit> GetAllUnits()
        {
            return _context.Units.ToList();
        }

        public IEnumerable<AircraftModel> GetAllModels()
        {
            return _context.AircraftModels.ToList();
        }
    }
}
