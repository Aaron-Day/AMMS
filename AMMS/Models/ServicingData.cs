using System.ComponentModel.DataAnnotations;

namespace AMMS.Models
{
    public class ServicingData
    {
        /* DA PAM 738-751
         * Page 41
         * DA Form 2408-12 Block 7
         * Army Aviator's Flight Record: Servicing Data
         */

        [Key]
        public string Id { get; set; }

        /* FUEL ADDED (GALLONS): The person servicing the
         * aircraft will enter the total quantity of fuel, in gallons,
         * added at one time on the next open line. If fuel was
         * removed, place a minus "-" sign in front of the quantity
         * removed. If no fuel was added or removed, leave blank.
         * 
         * Note. The person who serviced the aircraft or supervised
         * the servicing will make sure the DA Form 2408-12
         * entries match the amount and grade of fuel entries
         * recorded on the servicing issue slip.
         */
        [Display(Name = "FUEL ADDED (GALLONS)")]
        public int? FuelAdded { get; set; }

        /* GRADE: Enter the grade of fuel used to service the
         * aircraft.
         */
        [Display(Name = "GRADE")]
        [StringLength(3, MinimumLength = 3)]
        public string Grade { get; set; }

        /* OIL 1: Enter the amount of oil, in quarts, used to service
         * the No. 1 engine. If oil was removed place a minus "-"
         * sign in front of the quantity. If none, leave blank.
         */
        [Display(Name = "OIL 1")]
        public int? Oil1 { get; set; }

        /* GRADE: Enter the grade of oil used to service the No. 1
         * engine. If none, leave blank.
         */
        [Display(Name = "GRADE")]
        [StringLength(5, MinimumLength = 3)]
        public string Grade1 { get; set; }

        /* OIL 2: Enter the amount of oil, in quarts, used to service
         * the No. 2 engine. If oil was removed place a minus "-"
         * sign in front of the quantity. If none, leave blank.
         */
        [Display(Name = "OIL 2")]
        public int? Oil2 { get; set; }

        /* GRADE: Enter the grade of oil used to service the No. 2
         * engine. If none, leave blank.
         */
        [Display(Name = "GRADE")]
        [StringLength(5, MinimumLength = 3)]
        public string Grade2 { get; set; }

        /* OXYGEN: Enter the amount of oxygen, in pounds per
         * square inch (PSI), used to service the system. If none,
         * leave blank.
         */
        [Display(Name = "OXYGEN")]
        public int? Oxygen { get; set; }

        /* ANTI-ICING: Enter the amount of anti-icing fluid, in
         * gallons, used to service the system. If anti-icing fluid was
         * removed place a minus"-" sign in front of the quantity. If
         * none, leave blank.
         */
        [Display(Name = "ANTI-ICING")]
        public int? AntiIcing { get; set; }

        /* SERVICED BY: Enter the PID of the person doing the
         * servicing, or the person who supervised the servicing on
         * the same line with the service. If no servicing was
         * needed, the person who made the "TOTAL IN-TANKS"
         * check will enter his or her PID in this column.
         */
        [Required]
        [Display(Name = "SERVICED BY")]
        public string ServicedBy { get; set; }

        /* LOCATION: Enter the location where the aircraft was
         * serviced. The Federal Aviation Administration (FAA)
         * Identifier in CONUS or the International Civil Aviation
         * Organization (ICAO) Identifier for areas located
         * OCONUS may be used. (For example, HOP for
         * Campbell AAF, KY, HLR for Hood AAF, TX, GRK for
         * Gray AAF, TX, EDMA for Augsburg Heliport, GE, RKSL
         * for Seoul AAF Korea.) If an identifier is not assigned,
         * enter the location where the aircraft was serviced.
         * Authorized abbreviations are acceptable in this block.
         */
        [Required]
        [Display(Name = "LOCATION")]
        public string Location { get; set; }

        private string DA2408_12Id { get; set; }
        private DA2408_12 Flight { get; set; }
    }
}
