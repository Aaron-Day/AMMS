using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AMMS.Models
{
    public class FlightData
    {
        /* DA PAM 738-751
         * Page 41
         * DA Form 2408-12 Block 6a.
         * Army Aviator's Flight Record: Flight Data
         */

        [Key]
        public string Id { get; set; }

        /* 6. a. FLIGHT DATA: Enter the number of the flight (For
         * example, if it is the first flight of the mission day enter a
         * 1 here).
         * 
         * Note. The FAA identifier in CONUS or the ICAO identifier
         * for areas OCONUS may be used instead of the
         * airfield and place in the FROM and TO blocks.
         */
        [Display(Name = "FLIGHT DATA")]
        public int Flight { get; set; }

        /* FROM: Enter the airfield or place that you flew from.
         * The word "LOCAL" may be used in this block for flights
         * within the local flying area.
         */
        [Required]
        [Display(Name = "FROM")]
        public string From { get; set; }

        /* TO: Enter any intermediate stops during the flight (will
         * be left blank if "LOCAL" was used in the FROM section).
         */
        [Display(Name = "TO")]
        public string To1 { get; set; }

        /* TO: Enter the airfield or place you flew to (will be left
         * blank if "LOCAL" was used in the FROM section).
         */
        [Display(Name = "TO")]
        public string To2 { get; set; }

        /* TIME: FROM. Enter the time (24-hour clock) of take off.
         */
        [Display(Name = "TIME FROM")]
        public DateTime FromTime { get; set; }

        /* TO: Enter the time (24-hour clock) of intermediate landing.
         */
        [Display(Name = "TO")]
        public DateTime? ToTime1 { get; set; }

        /* TO: Enter the time (24-hour clock) of landing.
         * 
         * Note. When flying between time zones, the FROM and
         * TO entries will be recorded for the time zone at point of
         * takeoff.
         */
        [Display(Name = "TO")]
        public DateTime ToTime2 { get; set; }

        /* FLT HRS: Enter the total time, in hours and tenths, for
         * this flight.
         */
        [Display(Name = "FLT HRS")]
        public double FltHrs { get; set; }

        /* LDG: STD AUTO: Enter the total number of standard
         * landings, and touchdown auto rotations made during
         * this flight. For example, if you landed three times and
         * two of the landings were touchdown auto rotations, your
         * entry would be "1" standard landing and "2" touchdown
         * auto rotations. The number of touchdown auto rotations
         * may exceed the number of standard landings.
         */
        [Display(Name = "LDG: STD")]
        public int STD { get; set; }
        [Display(Name = "AUTO")]
        public int AUTO { get; set; }

        /* STARTS #1 #1: For multiengine fixed wing aircraft enter
         * the number of engine starts for the number 1 engine
         * and number 2 engine. Engine starts while the aircraft is
         * on the ground, or in the air will be recorded for each
         * flight when required by the aircraft maintenance manual.
         */
        [Display(Name = "STARTS #1")]
        public int? Starts1 { get; set; }
        [Display(Name = "#2")]
        public int? Starts2 { get; set; }

        /* MISSION ID. STD: Enter the mission symbol that
         * describes the purpose of the flight per AR 95-1. (Mission
         * Symbols can be found on the DA Form 2408 in front of
         * the logbook.) For flight simulators, leave blank or as
         * prescribed by the Commander of the Synthetic Flight
         * Training System (SFTS) facility.
         */
        [Display(Name = "MISSION ID STD")]
        [RegularExpression(@"^\D{1}$")]
        public string MissionIdStd { get; set; }

        /* CONFIG: Leave blank.
         */
        [Display(Name = "CONFIG")]
        [RegularExpression(@"^\D{1}$")]
        public string Config { get; set; }

        /* LOADS: INTERNAL, EXTERNAL, PASSENGERS:
         * When carrying internal cargo type loads, enter the total
         * number of pounds of the internal load; when carrying
         * external loads, enter the total number of pounds of the
         * external load; when carrying passengers, enter number
         * of passengers aboard aircraft during flight. Leave blank
         * when there are no passengers, or internal or external
         * cargo loaded on the aircraft.
         */
        [Display(Name = "LOADS INTERNAL")]
        public int? Internal { get; set; }
        [Display(Name = "EXTERNAL")]
        public int? External { get; set; }
        [Display(Name = "PASSENGERS")]
        public int? Passengers { get; set; }

        /* CYC: For fixed wing aircraft enter the total number of
         * times the landing gear was cycled up and down for this
         * flight.
         */
        [Display(Name = "CYC")]
        public int? CYC { get; set; }

        /* HSF: For OH-58D helicopters enter Hot Section Factor
         * counts displayed on the engine monitor page displayed
         * on the multifunction display (MFD).
         */
        [Display(Name = "HSF")]
        public int? HSF { get; set; }

        /* ROUNDS: 7.62 20MM 30MM 40MM ROCKET TOW
         * HELLFIRE. Enter the number rounds fired from each
         * weapon system for this flight. When a block is not present
         * for the type of weapon installed on the aircraft, line
         * out the title on an unused block, and enter the weapon
         * type and number of rounds fired. If none, or it does not
         * apply leave blank.
         */
        [Display(Name = "ROUNDS 7.62")]
        public int? _7_62 { get; set; }
        [Display(Name = "20mm")]
        public int? _20MM { get; set; }
        [Display(Name = "30mm")]
        public int? _30MM { get; set; }
        [Display(Name = "40mm")]
        public int? _40MM { get; set; }
        [Display(Name = "ROCKET")]
        public int? Rocket { get; set; }
        [Display(Name = "TOW")]
        public int? Tow { get; set; }
        [Display(Name = "HELLFIRE")]
        public int? Hellfire { get; set; }

        /* STATUS: 7.62 20MM 30MM 40MM ROCKET TOW
         * HELLFIRE. Leave these blocks blank.
         */
        [Display(Name = "STATUS 7.62")]
        [RegularExpression(@"^\D{1}$")]
        public string Stat7_62 { get; set; }
        [Display(Name = "20mm")]
        [RegularExpression(@"^\D{1}$")]
        public string Stat20MM { get; set; }
        [Display(Name = "30mm")]
        [RegularExpression(@"^\D{1}$")]
        public string Stat30MM { get; set; }
        [Display(Name = "40mm")]
        [RegularExpression(@"^\D{1}$")]
        public string Stat40MM { get; set; }
        [Display(Name = "RCKT")]
        [RegularExpression(@"^\D{1}$")]
        public string StatRocket { get; set; }
        [Display(Name = "TOW")]
        [RegularExpression(@"^\D{1}$")]
        public string StatTow { get; set; }
        [Display(Name = "HF")]
        [RegularExpression(@"^\D{1}$")]
        public string StatHellfire { get; set; }

        /* HIT CHECK NO. 1 ENGINE: Record the HIT check
         * deviation from the established baseline for No. 1 Engine
         * (first flight of the mission day). If it does not apply, leave
         * blank.
         */
        [Display(Name = "HIT CHECK NO. 1 ENGINE")]
        [RegularExpression(@"[+-]\d{1}|[+-]\d{2}$")]
        public string HIT1 { get; set; }

        /* HIT CHECK NO. 2 ENGINE: Record the HIT check
         * deviation from the established baseline for No. 2 Engine
         * (first flight of the mission day). If it does not apply, leave
         * blank.
         */
        [Display(Name = "NO. 2 ENGINE")]
        [RegularExpression(@"[+-]\d{1}|[+-]\d{2}$")]
        public string HIT2 { get; set; }

        /* APU. STARTS. HOURS: Enter the number of starts for
         * the APU, if required by the TM-23 and/or TB
         * 1-1500-341-01. For APUs without an hour meter, enter
         * the time the APU was operated, in hours and tenths, if
         * required by the TM-23 and/or TB 1-1500-341-01. If
         * APU starts are shown on the Start/Hour Meter, read the
         * meter after the last flight of the mission day and enter
         * the number of starts shown on the meter in block 8,
         * TOTALS, APU STARTS portion. If it does not apply,
         * leave blank.
         */
        [Display(Name = "APU STARTS")]
        public int? APUStarts { get; set; }
        [Display(Name = "HOURS")]
        public double? APUHours { get; set; }

        /* HOUR METER HRS: Leave blank.
         */
        [Display(Name = "HOUR METER HRS")]
        public double? HourMeterHrs { get; set; }

        /* 6b. PERSONNEL DATA: If your mission day has flights
         * that overlap into the next calendar day, and you need to
         * know the actual date of takeoff, for each flight, you can
         * enter the date (dd mmm yy) of takeoff in the title bar of 
         * block 6b. PERSONNEL DATA, preceding the word
         * "PERSONNEL."
         */
        public List<PersonnelData> PersonnelData { get; set; }

        private string DA2408_12Id { get; set; }
        private DA2408_12 FlightRecord { get; set; }
    }
}
