using System.ComponentModel.DataAnnotations;

namespace AMMS.Models
{
    public class DutyPosition
    {
        /* DA PAM 738-751
         * Page 41
         * DA Form 2408-12 Block 6c.
         * Army Aviator's Flight Record: DUTY SYMBOL/FLIGHT SYMBOL/HOURS/SEAT
         */

        [Key]
        public string Id { get; set; }

        /* DS: Enter the flying duty symbol, per AR 95-1, that
         * shows the type of duty completed by each crewmember
         * listed. If any crewmember changes their duty status,
         * during the flight, enter the new symbol in the next open
         * "DS column to the right.
         */
        [Required]
        [Display(Name = "DS")]
        [RegularExpression(@"\D{2}|\D{3}")]
        public string DS { get; set; }

        /* FS: Enter the flight condition symbol, per AR 95-1.
         */
        [Required]
        [Display(Name = "FS")]
        [RegularExpression(@"\D{1}|\D{2}")]
        public string FS { get; set; }

        /* HR: Enter the flying time, in hours and tenths, flown for
         * each duty and flight symbol. The sum of these times
         * must equal the total time in block 6a, FLT HRS.
         */
        [Display(Name = "HR")]
        public double HR { get; set; }

        /* S: Enter the seat each crewmember sat in for the flight,
         * "F" for Front, "B" for Back. This applies to tandem seat
         * aircraft only (AH-1, AH-64).
         */
        [Display(Name = "S")]
        [RegularExpression(@"^\D{1}$")]
        public string S { get; set; }

        private string PersonnelDataId { get; set; }
        private PersonnelData PersonnelData { get; set; }
    }
}
