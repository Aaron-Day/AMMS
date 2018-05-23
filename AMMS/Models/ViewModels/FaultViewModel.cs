using System.ComponentModel.DataAnnotations;

namespace AMMS.Models.ViewModels
{
    public class FaultViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Serial Number")]
        public string AcftSerialNumber { get; set; }
        [Display(Name = "MDS")]
        public string AcftModel { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        [Display(Name = "SYS")]
        public string SystemCode { get; set; }
        [Required]
        [Display(Name = "Date")]
        public string FaultDate { get; set; }
        [Required]
        [Display(Name = "NO.")]
        public int? FaultNumber { get; set; }
        [Required]
        [Display(Name = "Time")]
        public string FaultTime { get; set; }
        [Required]
        [Display(Name = "PID")]
        public string DiscPID { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Text")]
        public string FaultText { get; set; }
        [Required]
        [Display(Name = "HRS")]
        public double DiscAcftHrs { get; set; }
        [Required]
        [Display(Name = "When Discovered")]
        public string WhenDisc { get; set; }
        [Required]
        [Display(Name = "How Recognized")]
        public string HowRecog { get; set; }
        [Required]
        [Display(Name = "Malfunction Effect")]
        public string MalEff { get; set; }
        public string Delay { get; set; }
        [Required]
        public string WUC { get; set; }
        [Display(Name = "Date")]
        public string CompDate { get; set; }
        [Display(Name = "Time")]
        public string CompTime { get; set; }
        [Display(Name = "HRS")]
        public double? CompAcftHrs { get; set; }
        public int? Rounds { get; set; }
        [Display(Name = "Action Code")]
        public string ActionCode { get; set; }
        [Display(Name = "UIC")]
        public string CompWUC { get; set; }
        [DataType(DataType.MultilineText)]
        public string Action { get; set; }
        [Display(Name = "PID")]
        public string CompPID { get; set; }
        [Display(Name = "CAT")]
        public string CompCat { get; set; }
        [Display(Name = "MMH")]
        public double? CompHrs { get; set; }
        [Display(Name = "TI PID")]
        public string TIPID { get; set; }
        [Display(Name = "TI MMH")]
        public double? TIManHrs { get; set; }
        [Display(Name = "Aircraft Id")]
        public string AircraftId { get; set; }

        public bool isEmpty()
        {
            var empty = true;
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
