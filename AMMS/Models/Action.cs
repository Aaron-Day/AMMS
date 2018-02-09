using System.ComponentModel.DataAnnotations;

namespace AMMS.Models
{
    public class Action
    {
        /* DA PAM 738-751
         * Page 55
         * DA Form 2408-13-2, Actions
         * Related Maintenance Actions Record
         */

        [Key]
        public string Id { get; set; }


        /* 8. STA: The person entering related maintenance
         * actions on this form will enter the proper condition status
         * symbol according to the seriousness of each related
         * maintenance action (see para 1-8).
         */
        [Required]
        [Display(Name = "STA")]
        [RegularExpression(@"^[-/X+]{1}$")]
        public string Status { get; set; }

        /* RELATED MAINTENANCE ACTIONS: Enter a short
         * TODO: finish related maint description pg 58
         */
        [Required]
        [Display(Name = "RELATED MAINTENANCE ACTIONS")]
        public string RelatedMaintenanceAction { get; set; }

        public ActionCorrection CorectiveAction { get; set; }

        private string DA2408_13_2Id { get; set; }
        private DA2408_13_2 ParentAction { get; set; }
    }
}
