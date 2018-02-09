using System.ComponentModel.DataAnnotations;

namespace AMMS.Models
{
    public class DA2408_31
    {
        /* DA PAM 738-751
         * Page 36
         * DA Form 2408-31
         * Aircraft Identification Card
         */

        [Key]
        public string Id { get; set; }

        /* 1. MDS: Enter the mission, design, and series for the
         * aircraft or other aviation equipment
         */
        [Required]
        public string MDS { get; set; }

        /* 2. AIRCRAFT SERIAL NO.: Enter the serial number for
         * the aircraft (seven numeric digits) or other aviation
         * equipment.
         */
        [Required]
        [Display(Name = "AIRCRAFT SERIAL NO.")]
        [RegularExpression(@"^\d{7}$", ErrorMessage = "Invalid Serial Number")]
        public string AircraftSerialNumber { get; set; }

        /* 3. UNIT: Enter the name of the unit or organization the
         * aircraft or other aviation equipment is assigned to.
         */
        [Required]
        [Display(Name = "UNIT")]
        public string Unit { get; set; }

        /* 4. CREW CHIEF'S NAME: Enter the rank and name of
         * the crew chief or mechanic assigned to the aircraft or
         * other aviation equipment. Leave blank when used to
         * mark the historical binder.
         */
        [Display(Name = "CREW CHIEF'S NAME")]
        public string CrewChief { get; set; }

        /* 5. SUPERVISOR'S NAME: Enter the rank and name of
         * the supervisor (Section Sergeant, platoon Sergeant, or
         * Platoon Leader) or person responsible for the aircraft or
         * other aviation equipment. Leave blank when used to
         * mark the historical binder.
         */
        [Display(Name = "SUPERVISOR'S NAME")]
        public string Supervisor { get; set; }

        private string AircraftId { get; set; }
        private Aircraft Aircraft { get; set; }
    }
}
