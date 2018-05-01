using System;

namespace AMMS.Models
{
    public class Flight
    {
        public string Id { get; set; }

        public DateTime Date { get; set; }

        public int FlightNumber { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public double FlightHours { get; set; }

        public string AircraftId { get; set; }
        private Aircraft Aircraft { get; set; }
    }
}
