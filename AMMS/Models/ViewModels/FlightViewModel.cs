using System.ComponentModel.DataAnnotations;

namespace AMMS.Models.ViewModels
{
    public class FlightViewModel
    {
        public string Id { get; set; }

        public string Date { get; set; }

        [Display(Name = "Serial Number")]
        public string SerialNumber { get; set; }

        public string Model { get; set; }

        public string Organization { get; set; }

        public string Station { get; set; }

        [Display(Name = "Flight Number")]
        public int FlightNumber { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string Start { get; set; }

        public string End { get; set; }

        [Display(Name = "Flight Hours")]
        public double FlightHours { get; set; }

        [Display(Name = "Aircraft Id")]
        public string AircraftId { get; set; }
    }
}
