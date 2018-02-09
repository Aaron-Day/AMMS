using System;
using System.ComponentModel.DataAnnotations;

namespace AMMS.Models
{
    public class EngineHitReading
    {
        /* DA PAM 738-751
         * Page 45
         * DA Form 2408-13, block 6.
         * Aircraft Status Information Record: ENGINE HIT READINGS
         */

        [Key]
        public string Id { get; set; }

        /* DATE: Dates of the last five HIT readings.
         */
        [Display(Name = "DATE")]
        public DateTime Date { get; set; }

        /* NO 1: Last five HIT readings for NO 1 engine.
         */
        [Display(Name = "NO. 1")]
        [RegularExpression(@"[+-]\d{1}|[+-]\d{2}$")]
        public string HIT1 { get; set; }

        /* NO 2: Last five HIT readings for NO 2 engine.
         */
        [Display(Name = "NO. 2")]
        [RegularExpression(@"[+-]\d{1}|[+-]\d{2}$")]
        public string HIT2 { get; set; }

        private string DA2408_13Id { get; set; }
        private DA2408_13 AircraftStatus { get; set; }
    }
}
