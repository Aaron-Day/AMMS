using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AMMS.Models.ViewModels
{
    public class AircraftModelViewModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "MDS")]
        public string Mds { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "NSN")]
        [RegularExpression(@"^\d{13}|\d{4}-\d{2}-\d{3}-\d{4}$", ErrorMessage = "Invalid NSN")]
        public string Nsn { get; set; }

        [Display(Name = "EIC")]
        public string Eic { get; set; }

        public List<Aircraft> AllThisModelAircraft { get; set; }
    }
}
