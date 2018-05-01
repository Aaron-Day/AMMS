using System;

namespace AMMS.Models.ViewModels
{
    public class FlightViewModel
    {
        public string Id { get; set; }

        public DateTime Date { get; set; }

        public string SerialNumber { get; set; }

        public string Model { get; set; }

        public string Organization { get; set; }

        public string Station { get; set; }

        public int FlightNumber { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public double FlightHours { get; set; }

        public string AircraftId { get; set; }
    }
}
