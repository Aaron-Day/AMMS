using System.ComponentModel.DataAnnotations;

namespace AMMS.Models.ViewModels
{
    public class UnitViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(6, MinimumLength = 6)]
        public string UIC { get; set; }

        [Required]
        [Display(Name = "Unit Name")]
        public string UnitName { get; set; }

        [Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Phone]
        [Display(Name = "Unit Phone Number")]
        public string UnitPhone { get; set; }

        public string Station { get; set; }
    }
}
