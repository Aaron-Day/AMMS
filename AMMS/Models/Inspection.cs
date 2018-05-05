namespace AMMS.Models
{
    public class Inspection
    {
        public string Id { get; set; }
        public string InspectionNumber { get; set; }
        public string ItemToBeInspected { get; set; }
        public string Reference { get; set; }
        public string Frequency { get; set; }
        public string NextDue { get; set; }
        public string CompletedAt { get; set; }
        public string AircraftId { get; set; }
        private Aircraft Aircraft { get; set; }
    }
}
