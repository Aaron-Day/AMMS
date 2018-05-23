using System.ComponentModel.DataAnnotations;

namespace AMMS.Models.ViewModels
{
    public class RelatedMaintenanceViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Fault Status")]
        public string FaultStatus { get; set; }
        [Display(Name = "Serial Number")]
        public string SerialNumber { get; set; }
        [Display(Name = "SYS")]
        public string SystemCode { get; set; }
        [Display(Name = "Fault Date")]
        public string FaultDate { get; set; }
        [Display(Name = "Fault Number")]
        public int? FaultNumber { get; set; }
        [Display(Name = "Text")]
        public string FaultText { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        [Display(Name = "Related Maintenance Action")]
        public string RelatedMaintenanceAction { get; set; }
        [Display(Name = "Corrective Action")]
        public string CorrectiveAction { get; set; }
        public string PID { get; set; }
        [Display(Name = "CAT")]
        public string Category { get; set; }
        public double? MMH { get; set; }
        [Display(Name = "TI PID")]
        public string TIPID { get; set; }
        [Display(Name = "TI MMH")]
        public double? TIManHrs { get; set; }
        [Display(Name = "Fault Id")]
        public string FaultId { get; set; }

        public bool isTIReady()
        {
            var ready = true;
            ready = ready && !string.IsNullOrEmpty(CorrectiveAction);
            ready = ready && !string.IsNullOrEmpty(PID);
            ready = ready && !string.IsNullOrEmpty(Category);
            ready = ready && MMH != null;
            ready = ready && string.IsNullOrEmpty(TIPID);
            ready = ready && TIManHrs == null;
            ready = ready && (Status == "X" || Status == "+");
            return ready;
        }

        public bool isValidTI()
        {
            return (string.IsNullOrEmpty(TIPID) && TIManHrs == null) ||
                   (!string.IsNullOrEmpty(TIPID) && TIManHrs != null);
        }

        public bool isEmpty()
        {
            var empty = true;
            empty = empty && string.IsNullOrEmpty(CorrectiveAction);
            empty = empty && string.IsNullOrEmpty(PID);
            empty = empty && string.IsNullOrEmpty(Category);
            empty = empty && MMH == null;
            empty = empty && string.IsNullOrEmpty(TIPID);
            empty = empty && TIManHrs == null;
            return empty;
        }

        public bool isFull()
        {
            var full = true;
            full = full && !string.IsNullOrEmpty(CorrectiveAction);
            full = full && !string.IsNullOrEmpty(PID);
            full = full && !string.IsNullOrEmpty(Category);
            full = full && MMH != null;
            return full;
        }

        public bool isClosed()
        {
            if (Status == "X" || Status == "+")
            {
                return isFull() && TIManHrs != null;
            }
            return isFull();
        }

        public bool isOpen()
        {
            return !isClosed();
        }
    }
}
