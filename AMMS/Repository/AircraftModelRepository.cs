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
