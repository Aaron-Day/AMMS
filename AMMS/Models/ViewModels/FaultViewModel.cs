using System.ComponentModel.DataAnnotations;

namespace AMMS.Models.ViewModels
{
    public class FaultViewModel
    {
        public string Id { get; set; }
        public string AcftSerialNumber { get; set; }
        public string AcftModel { get; set; }
        public string Status { get; set; }
        public string SystemCode { get; set; }
        public string FaultDate { get; set; }
        public int? FaultNumber { get; set; }
        public string FaultTime { get; set; }
        public string DiscPID { get; set; }
        [DataType(DataType.MultilineText)]
        public string FaultText { get; set; }
        public double? DiscAcftHrs { get; set; }
        public string WhenDisc { get; set; }
        public string HowRecog { get; set; }
        public string MalEff { get; set; }
        public string Delay { get; set; }
        public string WUC { get; set; }
        public string CompDate { get; set; }
        public string CompTime { get; set; }
        public double? CompAcftHrs { get; set; }
        public int? Rounds { get; set; }
        public string ActionCode { get; set; }
        public string CompWUC { get; set; }
        [DataType(DataType.MultilineText)]
        public string Action { get; set; }
        public string CompPID { get; set; }
        public string CompCat { get; set; }
        public double? CompHrs { get; set; }
        public string TIPID { get; set; }
        public double? TIManHrs { get; set; }
        public string TIPassword { get; set; }
        public string AircraftId { get; set; }
    }
}
