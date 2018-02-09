using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AMMS.Models.ViewModels
{
    public class AircraftModelViewModel
    {
        public string Id { get; set; }

        [Required]
        public string Mds { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^\d{13}|\d{4}-\d{2}-\d{3}-\d{4}$", ErrorMessage = "Invalid NSN")]
        public string Nsn { get; set; }

        public string Eic { get; set; }

        public List<DA2408_18> MasterInspectionList { get; set; }

        public List<Aircraft> AllThisModelAircraft { get; set; }

        public void AddInspection(DA2408_18 inspection)
        {
            MasterInspectionList.Add(inspection);
            foreach (var aircraft in AllThisModelAircraft)
            {
                aircraft.AddInspection(inspection);
            }
        }
    }
}
