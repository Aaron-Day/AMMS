using System.ComponentModel.DataAnnotations;

namespace AMMS.Models
{
    public class ActionCorrection
    {
        /* DA PAM 738-751
         * Page 55
         * DA Form 2408-13-2, Actions
         * Related Maintenance Actions Record
         */

        [Key]
        public string Id { get; set; }

        /* 10. ACTION: Enter the corrective maintenance action
         * taken. DO NOT use the word "corrected." A few examples
         * of what can be entered are:
         * 
         * TODO: finish action documentation pg 58
         */
        [Required]
        [Display(Name = "ACTION")]
        public string Action { get; set; }

        /* 11. PID: The person completing the related maintenance
         * action will place his or her PID in this block.
         */
        [Required]
        [Display(Name = "PID")]
        [RegularExpression(@"^\D{2}\d{4}$|^\D{2}\d{6}$")]
        public string PID { get; set; }

        /* 12. CAT. ULLS-A/LAS users: Enter the level of maintenance
         * for the action taken ("C" for crew level, "O" for
         * AVUM, "F" for AVIM, and "D" for depot).
         * 
         * TODO: add note from text pg 58
         */
        [Required]
        [Display(Name = "CAT")]
        [RegularExpression(@"^[COFD]{1}$")]
        public string Cat { get; set; }

        /* 13. MMH. ULLS-A/LAS users: Enter the direct "hands
         * 
         * TODO: add rest of description pg 58
         */
        [Display(Name = "MMH")]
        public double Mmh { get; set; }

        // Custom added
        [Display(Name = "TIPID")]
        [RegularExpression(@"^\D{2}\d{4}$|^\D{2}\d{6}$")]
        public string TiPid { get; set; }

        private string ActionId { get; set; }
        private Action ParentAction { get; set; }
    }
}
