using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AMMS.Models
{
    public class DA2408_13_2
    {
        /* DA PAM 738-751
         * Page 55
         * DA Form 2408-13-2
         * Related Maintenance Actions Record
         */

        [Key]
        public string Id { get; set; }

        /* DATE: Enter the date (dd mmm yy) the DA Form
         * 2408-13-2 was started.
         */
        [Display(Name = "DATE")]
        public DateTime Date { get; set; }

        /* Page: Each form in the Flight Pack will be assigned a
         * page number starting with page 1 for the first DA Form
         * 2408-13-1 in the logbook. When a new DA Form
         * 2408-13-2 is added to the logbook it will be assigned a
         * page number. If there are three DA Forms 2408-13-1 in
         * the logbook, when the 2408-13-2 is added, the page
         * number of the DA Form 2408-13-2 will be 4. When this
         * form is used to document related maintenance actions
         * for AVIM or Depot component repair, the DA Form
         * 2408-13-3 used to document initial inspection of the
         * component will be page 1. The first DA Form
         * 2408-13-2 will be page 2, the second page will be 3,
         * and so on.
         */
        [Display(Name = "Page")]
        public int Page { get; set; }

        /* 1. STATUS: Enter the status symbol assigned to the
         * fault or deficiency listed on the DA Form 2408-13-1 or
         * 2408-13-3. DO NOT initial over the status symbol in
         * this block. When the fault, in block 7, is corrected, clear
         * the status for the fault on the DA Form 2408-13-1 or
         * 2408-13-3.
         */
        [Display(Name = "STATUS")]
        [RegularExpression(@"^[-/X+]{1}$")]
        public string Status { get; set; }

        /* 2. SERIAL NUMBER: Enter the serial number (seven
         * numeric characters) of the aircraft. When this form is
         * used to document AVIM or Depot level component
         * repair, enter the component serial number.
         */
        [Required]
        [Display(Name = "SERIAL NUMBER")]
        [RegularExpression(@"^\d{7}$", ErrorMessage = "Invalid Serial Number")]
        public string SerialNumber { get; set; }

        /* 3. SYSTEM CODE: Enter the system code of the fault
         * from the DA Form 2408-13-1 or 2408-13-3, FAULD
         * INFORMATION block.
         */
        [Required]
        [Display(Name = "SYS")]
        [RegularExpression(@"^\D{1}$")]
        public string SystemCode { get; set; }

        /* 4. TIME: Leave blank.
         */
        [Display(Name = "TIME")]
        public DateTime? Time { get; set; }

        /* 5. FAULT DATE: Enter the date (dd mmm yy) of the
         * fault from the DA Form 2408-13-1 or 2408-13-3,
         * FAULT INFORMATION block.
         */
        [Display(Name = "FAULT DATE")]
        public DateTime FaultDate { get; set; }

        /* 6. FAULT NUMBER. ULLS-A/LAS user: If a fault number
         * has been assigned by the computer, enter the fault
         * number. Leave blank if no fault number has been
         * assigned.
         */
        [Display(Name = "FAULT NUMBER")]
        public int FaultNumber { get; set; }

        /* 7. FAULT: Enter the fault exactly as it is written on the
         * DA Form 2408-13-1 or 2408-13-3. Only one fault will
         * be written in this block.
         * 
         * Note. When using DA Form 2408-13-2, NOV 91, do not
         * enter a fault in block 7 on the reverse side of the form.
         * Only one fault will be entered on each form.
         */
        [Required]
        [Display(Name = "FAULT")]
        public string Fault { get; set; }

        /* RELATED MAINTENANCE ACTIONS
         */
        public List<Action> Actions { get; set; }

        private string FaultId { get; set; }
        private Fault ParentFault { get; set; }
    }
}
