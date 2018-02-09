using System;
using System.ComponentModel.DataAnnotations;

namespace AMMS.Models
{
    public class DA2408_5
    {
        /* DA PAM 738-751
         * Page 166
         * DA Form 2408-5
         * Equipment Modification Record
         */

        [Key]
        public string Id { get; set; }

        [Display(Name = "Page")]
        public int? Page { get; set; }
        [Display(Name = "of")]
        public int? ofPage { get; set; }

        /* 1. NOMENCLATURE: Enter the item name.
         */
        [Required]
        [Display(Name = "NOMENCLATURE")]
        public string Nomenclature { get; set; }

        /* 2. MODEL: Enter the mission, design, and series of the
         * aircraft.
         */
        [Required]
        [Display(Name = "MODEL")]
        public string Model { get; set; }

        /* 3. AIRCRAFT SERIAL NUMBER: Enter the aircraft
         * serial number (seven numeric digits).
         */
        [Required]
        [Display(Name = "AIRCRAFT SERIAL NUMBER")]
        [RegularExpression(@"^\d{7}$", ErrorMessage = "Invalid Serial Number")]
        public string AircraftSerialNumber { get; set; }

        /* 4. MWO NUMBER AND DATE:
         * a) Enter the MWO number in the upper part of this
         * block. The activity applying the MWO will normally
         * complete this block. MWO's not applied will be entered by
         * the unit/activity that determines that MWO was not
         * applied.
         * 
         * b) Enter the MWO publication date (dd mmm yy), priority
         * for the MWO ("E" for Emergency, "U" for Urgent, or "R"
         * for Routine) and the maintenance level responsible for
         * application of the MWO ("O" for AVUM, "F" for AVIM or
         * "D" for Depot) in the bottom part of this block.
         */
        [Required]
        [Display(Name = "MWO NUMBER AND DATE")]
        public string MWONumber { get; set; }
        public DateTime MWODate { get; set; }
        [Required]
        [RegularExpression(@"^\D{1}$")]
        public string Priority { get; set; }
        [Required]
        [RegularExpression(@"^\D{1}$")]
        public string MaintenanceLevel { get; set; }

        /* 5. MWO TITLE: Enter the MWO abbreviated title.
         */
        [Required]
        [Display(Name = "MWO TITLE")]
        public string MWOTitle { get; set; }

        /* 6. ORGANIZATION APPLYING MWO: Enter the UIC,
         * or the name of the organization that applied the MWO or
         * determined that the MWO was previously applied. If a
         * MWO is issued but not applied, enter the date that the
         * MWO must be applied (black lead pencil). Erase the
         * date when the MWO is applied.
         * 
         * NOTE: Enter the MWO as overdue on DA Form
         * 2408-13-1 if the MWO has not been applied by the due
         * date.
         */
        [Display(Name = "ORGANIZATION APPLYING MWO")]
        public string OrganizationApplyingMWO { get; set; }
        public DateTime? ApplyBy { get; set; }

        /* 7. NAME OR PID: The person who does the technical
         * inspection, to certify the MWO application, will enter his
         * or her PID in this block.
         */
        [Display(Name = "NAME OR PID")]
        public string NameOrPID { get; set; }

        /* 8. DATE: Enter the date (dd mmm yy) that the MWO
         * was applied.
         */
        [Display(Name = "DATE")]
        public DateTime? Date { get; set; }

        /* 9. MANHOURS: Enter the actual manhours it took to
         * apply the MWO, to the nearest tenth of an hour,
         * including the technical inspection.
         */
        [Display(Name = "MAN-HRS")]
        public double? ManHours { get; set; }

        private string AircraftId { get; set; }
        private Aircraft Aircraft { get; set; }
    }
}
