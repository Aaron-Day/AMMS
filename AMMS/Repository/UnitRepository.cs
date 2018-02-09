using AMMS.Data;
using AMMS.Models;
using System.Collections.Generic;
using System.Linq;

namespace AMMS.Repository
{
    public class UnitRepository : IUnitRepository
    {
        private readonly ApplicationDbContext _context;

        public UnitRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Unit GetUnit(string id)
        {
            return _context.Units.Find(id);
        }

        public IEnumerable<Unit> GetUnits()
        {
            return _context.Units.ToList();
        }

        public void SaveUnit(Unit unit)
        {
            _context.Units.Add(unit);
            _context.SaveChanges();
        }

        public void UpdateUnit(Unit unit)
        {
            _context.SaveChanges();
        }

        public void DeleteUnit(string id)
        {
            var unit = _context.Units.Find(id);

            if (unit == null) return;

            _context.Units.Remove(unit);
            _context.SaveChanges();
        }
    }
}
