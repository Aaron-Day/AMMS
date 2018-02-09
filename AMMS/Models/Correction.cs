using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AMMS.Models
{
    public class Correction
    {
        /* DA PAM 738-751
         * Page 49
         * DA Form 2408-13-1, PART II
         * Aircraft Inspection and Maintenance Record: CORRECTING INFORMATION
         */

        [Key]
        public string Id { get; set; }

        /* PART II-CORRECTING INFORMATION:
         * a. When a fault, deficiency, condition, or incorrect entry
         * is corrected; directives (such as, SOF messages,
         * ASAMs, TBs, or MWO's) are applied; and components/
         * modules or other repair parts are removed and reinstalled,
         * or replaced the person making the corrective action
         * will enter the action taken in the CORRECTING INFORMATION
         * block, which is to the right of the FAULT INFORMATION
         * Block. Using words or phrases, such as
         * "applied," "tested," "installed," "serviced," "replaced,"
         * "repaired," "adjusted," or "erroneous entry," with other
         * brief information about the action, will be enough to
         * describe the corrective action. It will not be considered in
         * error if you use one or more words to describe action
         * taken. DO NOT use the word "corrected."
         * 
         * Note. A few examples of what can be entered in the
         * FAULT/REMARKS, and CORRECTING INFORMATION
         * sections are:
         * 
         * 1. "PMD due," "PMS due," "Phase Insp Due," "#2
         * Phase Insp due" - "completed," "PMD completed,"
         * "PMS completed," "Inspection completed," "#2 Phase
         * Insp completed," and so on.
         * 
         * 2. "Test flight due," "Test flight required," "MOC
         * required" - "completed," "Test flight completed," "Maintenance
         * test flight completed," "MTF completed," "MOC
         * completed," "Maintenance operational check OK," and
         * so on.
         * 
         * 3. "mag compass calibration due" - "completed," "mag
         * compass Cal. completed."
         * 
         * 4. "Copilot's door window crazed" - "Replaced,"
         * "Changed," "Window replaced," "Replaced window assy,"
         * "replaced copilot's door window," "Changed window."
         * 
         * 5. "Oxygen System low" - "Serviced," "Oxy. Sys Serviced,"
         * "system serviced."
         * 
         * 6. "Engine deck torn" - "Replaced," "Eng. deck repl.,"
         * "Deck replaced."
         * 
         * b. Upon completion of the corrective action, the person
         * making the corrective action will place his or her PID in
         * the PID block, and last name initial over the status symbol
         * in the FAULT INFORMATION block. To clear a red
         * "X" or circled red "X" symbol, the completed action must
         * be inspected by the commander or designated representative
         * per paragraph 1-9. If the action is found to be
         * satisfactory, the statement "Insp OK," and the inspector's
         * signature will be entered on the line following the
         * action taken. If there is no space. the signature may be
         * placed above the corrective action taken. The inspector 
         * ill then enter his or her PID in the TIPID Block, place
         * last name initial over the status symbol in the FAULT
         * INFORMATION block, and change the status symbol in
         * the SYSTEM STATUS Block on the DA Form 2408-13
         * if it applies.
         * 
         * Note. A technical inspector's stamp may be used instead
         * of the statement and signature. If more than one
         * technical inspector performed the technical inspections
         * to complete the fault correction, the senior technical
         * inspector performing the inspection will enter his or her
         * PID in the TIPID Block, enter "Insp OK," and signature
         * in Action section.
         * 
         * c. When corrective action is taken by other than the
         * parent unit or designated support activities, the person
         * taking the action will enter his or her unit or activity
         * designation in this block. The unit delegation orders are
         * official, and allow the delegated persons to sign off red
         * "X" or circled red "X" entries on aircraft belonging to
         * other units or activities. Qualified designated representatives
         * may also certify work performed, or do one time
         * inspections on aircraft belonging to other units or activites.
         * They will enter the unit or activity in this block, and
         * place his or her last name initial over the status symbol
         * in the FAULT INFORMATION block.
         * 
         * d. Entries on the DA Form 2408-13-1 that call for
         * entries on historical forms, and records (such as,
         * replacement of reportable items, ASAMs, SOF messages,
         * TBs, MWO applications, special inspections/services/
         * replacements performed, and so forth) is the responsibility
         * of the technical inspector that inspected the corrective
         * action.
         * 
         * Note. A checklist (table 3-9) is provided to help ensure
         * that entries are made on all forms and records that
         * apply, before releasing aircraft from maintenance. The
         * checklist is also provided to verify that a quality inspection
         * of forms, records, and files has been done. The use
         * of the checklist is not mandatory; however, it is
         * recommended for newly assigned maintenance and quality
         * control personnel.
         * 
         * DATE: Enter date (dd mmm yy) of the corrective action.
         */
        [Display(Name = "DATE")]
        public DateTime Date { get; set; }

        /* TIME: Enter the time of day (24-hour clock) the corrective
         * action was completed.
         */
        [Display(Name = "TIME")]
        public DateTime Time { get; set; }

        /* ACFT HRS: Enter the actual aircraft hours when the
         * corrective action, on a fault, condition, inspection, service,
         * MOC, or test flight, was completed.
         */
        [Display(Name = "ACFT HRS")]
        public double AcftHrs { get; set; }

        /* ROUNDS: Enter the cumulative rounds fired from DA
         * Form 2408-4-1, block 5e for armament system faults,
         * discrepancies and inspections.
         */
        [Display(Name = "ROUNDS")]
        public int? Rounds { get; set; }

        /* ACTION CODE. ULLS-A/LAS users: Enter the Action
         * Code from DA Form 2408 or table 1-9 that best
         * describes the action taken.
         */
        [Required]
        [Display(Name = "ACTION CODE")]
        [RegularExpression(@"^[ABCDEFGHJKLMNOPQRSTUWXY123456789]{1}$")]
        public string ActionCode { get; set; }

        /* WUC. ULLS-A/LAS users: Enter the Work Unit Code
         * that best describes which component/module or part the
         * corrective action pertains to. If a WUC has not been
         * established, that applies to the component/module, part
         * or area, enter the functional group code from DA Form
         * 2408 or table 1-10 that best fits the corrective action.
         * The corrective action WUC may be different from the
         * when discovered WUC.
         */
        [Required]
        [Display(Name = "WUC")]
        public string Wuc { get; set; }

        /* ACTION: Enter an abbreviated explanation of the action
         * taken.
         */
        [Required]
        [Display(Name = "ACTION")]
        public string Action { get; set; }

        /* INDIVIDUALS INVOLVED IN CORRECTING FAULT
         */
        public List<IndividualSignOff> Individuals { get; set; }

        /* TIPID: Enter the personnel identifier of the technical
         * inspector who inspected the maintenance action. If more
         * than one technical inspector performed the technical
         * inspections of the maintenance action or related maintenance
         * actions, the senior technical inspector, performing
         * the inspection, will enter his or her PID in this block.
         */
        [Display(Name = "TIPID")]
        [RegularExpression(@"^\D{2}\d{4}$|^\D{2}\d{6}$")]
        public string TiPid { get; set; }

        /* TI MANHOURS. ULLS-A/LAS users only: This space
         * is provided for the Technical Inspector(s) to enter the
         * manhours, in hours and thenths, it took to inspect the corrective action. This will include the time used for the
         * inspection, and to complete forms and records. If more
         * than one technical inspector performed the technical
         * inspections on the maintenance action or related maintenance
         * actions, the senior technical inspector, performing
         * the inspection will enter the total manhours used to
         * complete the inspection.
         */
        [Display(Name = "TI MANHOURS")]
        public double? TiManHours { get; set; }

        private string FaultId { get; set; }
        private Fault Fault { get; set; }
    }
}
