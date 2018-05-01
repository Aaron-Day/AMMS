using System;

namespace AMMS.Models.ViewModels
{
    public class RelatedMaintenanceViewModel
    {
        public string Id { get; set; }
        public string FaultStatus { get; set; }
        public string SerialNumber { get; set; }
        public string SystemCode { get; set; }
        public DateTime? FaultDate { get; set; }
        public int? FaultNumber { get; set; }
        public string FaultText { get; set; }
        public string Status { get; set; }
        public string RelatedMaintenanceAction { get; set; }
        public string CorrectiveAction { get; set; }
        public string PID { get; set; }
        public string Category { get; set; }
        public double? MMH { get; set; }
        public string FaultId { get; set; }
    }
}
