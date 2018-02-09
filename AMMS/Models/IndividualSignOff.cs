using System.ComponentModel.DataAnnotations;

namespace AMMS.Models
{
    public class IndividualSignOff
    {
        /* DA PAM 738-751
         * Page 49
         * DA Form 2408-13-1, PART II, Sign Off
         * Aircraft Inspection and Maintenance Record: CORRECTING INFORMATION, SIGN OFF
         */

        [Key]
        public string Id { get; set; }

        /* PID: Enter the personnel identifier of the person(s)
         * accomplishing all or part of the corrective action. If a DA
         * Form 2408-13-2 was used, all personnel completing
         * actions on that form will enter their PID on this form.
         * When a scheduled inspection (such as, phase, periodic,
         * or progressive phase maintenance) is completed, DO
         * NOT reenter the PIDs from the maintenance checklist.
         * The maintenance supervisor will enter his or her PID
         * certifying completion of the inspection.
         */
        [Required]
        [Display(Name = "PID")]
        [RegularExpression(@"^\D{2}\d{4}$|^\D{2}\d{6}$")]
        public string PID { get; set; }

        /* CAT. ULLS-A/LAS users: Enter the level of maintenance
         * for the action taken ("C" for crew level, "O" for
         * AVUM level, "F" for AVIM level, or "D" for depot level). If
         * an individual performs both crew level and AVUM level
         * work to perform the corrective action, they will enter
         * their PID twice, once for crew level maintenance hours
         * and once for AVUM level maintenance hours.
         */
        [Required]
        [Display(Name = "CAT")]
        [RegularExpression(@"^[COFD]{1}$")]
        public string Cat { get; set; }

        /* HRS. ULLS-A/LAS users only: Enter the direct "hands
         * on" manhours, in hours and tenths, expended during the
         * corrective action. This time does not include the time
         * expended chasing parts or tools, but does include time
         * expended filling out forms and records. All personnel
         * completing related maintenance actions on a DA Form
         * 2408-13-2 will enter manhours expended in this block.
         * At the completion of a scheduled inspection (such as,
         * phase, periodic or progressive phase maintenance) the
         * supervisor will enter the total number of manhours expended
         * to complete the visual inspection including any
         * required disassembly. The supervisor will separate the
         * manhours by category of maintenance. When extensive
         * maintenance is performed, such as an engine replacement,
         * and there are not enough HRS blocks available
         * for all the mechanics performing maintenance to enter
         * their manhours, the maintenance supervisor may
         * consolidate the manhours by category and use his or her
         * PID to enter the manhours.
         * 
         * Note. When DA Form 2408-13-1, Oct 91, or DA Form
         * 2408-13-3, Nov 91, is used the CMH, OMH, FMH, and
         * DMH blocks will be left blank.
         */
        [Display(Name = "HRS")]
        public double Hrs { get; set; }

        private string CorrectionId { get; set; }
        private Correction Correction { get; set; }
    }
}
