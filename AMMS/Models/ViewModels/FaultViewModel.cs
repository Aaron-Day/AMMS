using System.ComponentModel.DataAnnotations;

namespace AMMS.Models.ViewModels
{
    public class FaultViewModel
    {
        public string Id { get; set; }
        public string AcftSerialNumber { get; set; }
        public string AcftModel { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string SystemCode { get; set; }
        [Required]
        public string FaultDate { get; set; }
        [Required]
        public int? FaultNumber { get; set; }
        [Required]
        public string FaultTime { get; set; }
        [Required]
        public string DiscPID { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string FaultText { get; set; }
        [Required]
        public double DiscAcftHrs { get; set; }
        [Required]
        public string WhenDisc { get; set; }
        [Required]
        public string HowRecog { get; set; }
        [Required]
        public string MalEff { get; set; }
        public string Delay { get; set; }
        [Required]
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
        public string AircraftId { get; set; }

        public bool isEmpty()
        {
            var empty = true;
            empty = empty && string.IsNullOrEmpty(WUC);
            empty = empty && string.IsNullOrEmpty(CompDate);
            empty = empty && string.IsNullOrEmpty(CompTime);
            empty = empty && CompAcftHrs == null;
            empty = empty && Rounds == null;
            empty = empty && string.IsNullOrEmpty(ActionCode);
            empty = empty && string.IsNullOrEmpty(CompWUC);
            empty = empty && string.IsNullOrEmpty(Action);
            empty = empty && string.IsNullOrEmpty(CompPID);
            empty = empty && string.IsNullOrEmpty(CompCat);
            empty = empty && CompHrs == null;
            empty = empty && string.IsNullOrEmpty(TIPID);
            empty = empty && TIManHrs == null;

            return empty;
        }

        public bool isFull()
        {
            var full = true;
            full = full && !string.IsNullOrEmpty(WUC);
            full = full && !string.IsNullOrEmpty(CompDate);
            full = full && !string.IsNullOrEmpty(CompTime);
            full = full && CompAcftHrs != null;
            full = full && !string.IsNullOrEmpty(ActionCode);
            full = full && !string.IsNullOrEmpty(CompWUC);
            full = full && !string.IsNullOrEmpty(Action);
            full = full && !string.IsNullOrEmpty(CompPID);
            full = full && !string.IsNullOrEmpty(CompCat);
            full = full && CompHrs != null;

            return full;
        }

        public bool isValidTI()
        {
            return (string.IsNullOrEmpty(TIPID) && TIManHrs == null)
                     || (!string.IsNullOrEmpty(TIPID) && TIManHrs != null);
        }

        public bool isClosed()
        {
            if (Status == "X" || Status == "+")
            {
                return isFull() && !string.IsNullOrEmpty(TIPID);
            }
            return isFull();
        }

        public bool isOpen()
        {
            return !isClosed();
        }

        public bool isTIReady()
        {
            return (Status == "X" || Status == "+") && !string.IsNullOrEmpty(CompPID) && TIManHrs == null;
        }
    }
}
