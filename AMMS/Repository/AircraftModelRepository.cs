using AMMS.Data;
using AMMS.Models;
using System.Collections.Generic;
using System.Linq;

namespace AMMS.Repository
{
    public class AircraftModelRepository : IAircraftModelRepository
    {
        private readonly ApplicationDbContext _context;

        public AircraftModelRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public AircraftModel GetModel(string id)
        {
            return _context.AircraftModels.Find(id);
        }

        public IEnumerable<AircraftModel> GetModels(string uic)
        {
            if (uic == null || uic == "ADMIN") return _context.AircraftModels.ToList();

            var unitId = _context.Units.FirstOrDefault(u => u.UIC == uic)?.Id;
            var aircraft = _context.Aircraft.Where(a => a.UnitId == unitId).ToList();
            var models = new HashSet<AircraftModel>();
            foreach (var acft in aircraft)
            {
                models.Add(_context.AircraftModels.Find(acft.AircraftModelId));
            }
            return models.ToList();
        }

        public IEnumerable<AircraftModel> GetAllModels()
        {
            return _context.AircraftModels.ToList();
        }

        public void SaveModel(AircraftModel model)
        {
            _context.AircraftModels.Add(model);
            _context.SaveChanges();
        }

        public void UpdateModel(AircraftModel model)
        {
            _context.SaveChanges();
        }

        public void DeleteModel(string id)
        {
            var model = _context.AircraftModels.Find(id);

            if (model == null) return;

            _context.AircraftModels.Remove(model);
            _context.SaveChanges();
        }
    }
}
