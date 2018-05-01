using System.ComponentModel.DataAnnotations;

namespace AMMS.Models
{
    public class Aircraft
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [RegularExpression(@"^\d{7}$", ErrorMessage = "Invalid Serial Number")]
        [Display(Name = "Serial Number")]
        public string SerialNumber { get; set; }

        [Required]
        public double AcftHrs { get; set; }

        public string AircraftModelId { get; set; }
        public AircraftModel AircraftModel { get; set; }

        public string UnitId { get; set; }
        public Unit Unit { get; set; }
    }
}
