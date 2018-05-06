using System;
using System.ComponentModel.DataAnnotations;

namespace AMMS.Models.ViewModels
{
    public class InspectionViewModel
    {
        public string Id { get; set; }
        [Required]
        public string Nomenclature { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        [Display(Name = "Serial Number")]
        public string SerialNumber { get; set; }
        [Required]
        [Display(Name = "Inspection Number")]
        public string InspectionNumber { get; set; }
        [Required]
        [Display(Name = "Items To Be Inspected")]
        public string ItemToBeInspected { get; set; }
        [Required]
        public string Reference { get; set; }
        [Required]
        [RegularExpression(@"^\d{1,2}[HDMY]{1}$")]
        public string Frequency { get; set; }
        [Required]
        [Display(Name = "Next Due")]
        public string NextDue { get; set; }
        [Required]
        public string AircraftId { get; set; }

        public void Update(double hours = 0)
        {
            switch (FreqUnits())
            {
                case "H":
                    NextDue = (hours + FreqValue()).ToString();
                    break;
                case "D":
                    NextDue = DateTime.Today.AddDays(FreqValue()).ToString("dd MMM yy").ToUpper();
                    break;
                case "M":
                    NextDue = DateTime.Today.AddMonths(FreqValue()).ToString("dd MMM yy").ToUpper();
                    break;
                case "Y":
                    NextDue = DateTime.Today.AddYears(FreqValue()).ToString("dd MMM yy").ToUpper();
                    break;
            }
        }

        private int FreqValue()
        {
            return Convert.ToInt32(Frequency.Substring(0, Frequency.Length - 1));
        }

        private string FreqUnits()
        {
            return Frequency.Substring(Frequency.Length - 1);
        }
    }
}
