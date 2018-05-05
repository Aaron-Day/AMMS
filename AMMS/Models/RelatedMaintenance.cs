namespace AMMS.Models
{
    public class RelatedMaintenance
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public string RelatedMaintenanceAction { get; set; }
        public string CorrectiveAction { get; set; }
        public string PID { get; set; }
        public string Category { get; set; }
        public double? MMH { get; set; }
        public string TIPID { get; set; }
        public double? TIManHrs { get; set; }
        public string FaultId { get; set; }
        private Fault Fault { get; set; }
    }
}
