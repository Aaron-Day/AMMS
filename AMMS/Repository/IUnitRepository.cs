using AMMS.Models;
using System.Collections.Generic;

namespace AMMS.Repository
{
    public interface IUnitRepository
    {
        Unit GetUnit(string id);

        IEnumerable<Unit> GetUnits();

        void SaveUnit(Unit unit);

        void UpdateUnit(Unit unit);

        void DeleteUnit(string id);
    }
}
