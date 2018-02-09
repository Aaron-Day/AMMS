﻿using System.ComponentModel.DataAnnotations;

namespace AMMS.Models.ViewModels
{
    public class AircraftViewModel
    {
        public string Id { get; set; }

        [Required]
        [RegularExpression(@"^\d{7}$", ErrorMessage = "Invalid Serial Number")]
        [Display(Name = "Serial Number")]
        public string SerialNumber { get; set; }

        [Required]
        public double AcftHrs { get; set; }

        public string AircraftModelId { get; set; }
    }
}
