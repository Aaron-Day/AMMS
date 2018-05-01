using System;

namespace AMMS.Models.ViewModels
{
    public class InspectionViewModel
    {
        public string Id { get; set; }
        public string Nomenclature { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public string InspectionNumber { get; set; }
        public string ItemToBeInspected { get; set; }
        public string Reference { get; set; }
        public string Frequency { get; set; }
        public string NextDue { get; set; }
        public DateTime? CompletedAt { get; set; }
        public string AircraftId { get; set; }
    }
}
