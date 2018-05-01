using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AMMS.Models
{
    public class AircraftModel
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Mds { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^\d{13}|\d{4}-\d{2}-\d{3}-\d{4}$", ErrorMessage = "Invalid NSN")]
        public string Nsn { get; set; }

        public string Eic { get; set; }

        public List<Aircraft> AllThisModelAircraft { get; set; }
    }
}
