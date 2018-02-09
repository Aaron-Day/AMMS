using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AMMS.Models
{
    public class DA2408_13_1
    {
        /* DA PAM 738-751
         * Page 49
         * DA Form 2408-13-1
         * Aircraft Inspection and Maintenance Record
         */

        [Key]
        public string Id { get; set; }

        /* 1. AIRCRAFT SERIAL NUMBER: Enter the complete
         * aircraft serial number (seven numerical digits).
         */
        [Required]
        [Display(Name = "AIRCRAFT SERIAL NUMBER")]
        [RegularExpression(@"^\d{7}$", ErrorMessage = "Invalid Serial Number")]
        public string AircraftSerialNumber { get; set; }

        /* 2. MODEL: Enter the aircraft mission, design, and
         * series.
         */
        [Required]
        [Display(Name = "MODEL")]
        public string Model { get; set; }

        /* 3. DATE: The pilot will enter the date (dd mmm yy) of
         * the first takeoff of the mission day.
         */
        [Display(Name = "DATE")]
        public DateTime? Date { get; set; }

        /* 4. PAGE: Pages are to be numbered by the mechanic,
         * crew chief or pilot at the beginning of each mission day.
         * Enter the page number (black lead pencil) beginning
         * with "1." as additional DA Form 2408-13-1 are added,
         * enter the next page number.
         */
        [Display(Name = "PAGE")]
        public int? Page { get; set; }

        /* FAULTS:
         */
        public List<Fault> Faults { get; set; }

        private string AircraftId { get; set; }
        private Aircraft Aircraft { get; set; }
    }
}
