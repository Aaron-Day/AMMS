using System.ComponentModel.DataAnnotations;

namespace AMMS.Models.ViewModels
{
    public class RelatedMaintenanceViewModel
    {
        public string Id { get; set; }
        public string FaultStatus { get; set; }
        public string SerialNumber { get; set; }
        public string SystemCode { get; set; }
        public string FaultDate { get; set; }
        public int? FaultNumber { get; set; }
        public string FaultText { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string RelatedMaintenanceAction { get; set; }
        public string CorrectiveAction { get; set; }
        public string PID { get; set; }
        public string Category { get; set; }
        public double? MMH { get; set; }
        public string TIPID { get; set; }
        public double? TIManHrs { get; set; }
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
