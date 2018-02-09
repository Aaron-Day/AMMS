using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AMMS.Models
{
    public class PersonnelData
    {
        /* DA PAM 738-751
         * Page 41
         * DA Form 2408-12 Block 6b.
         * Army Aviator's Flight Record: Personnel Data
         */

        [Key]
        public string Id { get; set; }

        /* NAME: Enter last name and first name initial of each
         * crewmember of each flight.
         */
        [Required]
        [Display(Name = "NAME")]
        public string Name { get; set; }

        /* RANK: Enter rank of each crewmember for each flight.
         * For example, CPT., MAJ., LTC., WO1., CW2., SPC.,
         * SGT., SSG., SFC., MSG., and so on.
         */
        [Required]
        [Display(Name = "RANK")]
        [StringLength(3, MinimumLength = 2)]
        public string Rank { get; set; }

        /* PID/SSAN: Enter the PID for each crewmember.
         */
        [Required]
        [Display(Name = "PID/SSAN")]
        [RegularExpression(@"^\D{2}\d{4}$|^\D{2}\d{6}$")]
        public string PID { get; set; }

        /* 6c. DUTY SYMBOL/FLIGHT SYMBOL/HOURS/SEAT
         * 
         * Note. Symbols can be found on the DA Form 2408 in
         * front of the logbook.
         */
        public List<DutyPosition> DutyPositions { get; set; }

        /* Note. After all crewmembers for the mission have been
         * listed in block 6b, PERSONNEL DATA, enter "LAST
         * ENTRY" on the next open line. If the number of
         * crewmembers exceeds the space available for the flight,
         * continue recording the names in the next open block 6b,
         * PERSONNEL DATA. List only the persons on flying status.
         * Any changes made in the PERSONNEL DATA,
         * DUTY SYMBOL, FLIGHT SYMBOL, and HOURS blocks
         * must be initialed by the pilot in command. Enter any
         * other information needed locally.
         */

        private string FlightDataId { get; set; }
        private FlightData FlightData { get; set; }
    }
}
