using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AMMS.Models
{
    public class DA2408_12
    {
        /* DA PAM 738-751
         * Page 41
         * DA Form 2408-12
         * Army Aviator's Flight Record
         */

        [Key]
        public string Id { get; set; }

        /* Page ___ of ___: Enter page number "1" in the first space
         * after the word Page, leave the second space empty, the
         * pilot will fill this in at completion of the mission.
         */
        [Display(Name = "Page")]
        public int? Page { get; set; }
        [Display(Name = "of")]
        public int? ofPage { get; set; }

        /* 1. DATE: Leave blank, the pilot will enter the date at the
         * beginning of the first flight of the mission day.
         */
        [Display(Name = "DATE")]
        public DateTime Date { get; set; }

        /* 2. SERIAL NUMBER: Enter the aircraft serial number
         * (seven numeric digits).
         */
        [Required]
        [Display(Name = "SERIAL NUMBER")]
        [RegularExpression(@"^\d{7}$", ErrorMessage = "Invalid Serial Number")]
        public string AircraftSerialNumber { get; set; }

        /* 3. MODEL: Enter the aircraft mission design series.
         */
        [Required]
        [Display(Name = "MODEL")]
        public string Model { get; set; }

        /* 4. ORGANIZATION: Enter the unit or activity the aircraft
         * is assigned to.
         */
        [Required]
        [Display(Name = "ORGANIZATION")]
        public string Organization { get; set; }

        /* 5. STATION: Enter the aircraft's home station.
         */
        [Required]
        [Display(Name = "STATION")]
        public string Station { get; set; }

        /* 6. Blocks a. FLIGHT DATA, b. PERSONNEL DATA,
         * and c. DUTY SYMBOL/FLIGHT SYMBOL/HOURS/
         * SEAT will be filled out by the pilot.
         */
        public List<FlightData> FlightData { get; set; }

        /* 7. SERVICING DATA: At the start of the mission day,
         * the first line will show the grade of fuel and oil, and the
         * results of the in-tank checks. The crew chief or
         * mechanic will enter their PID in the "SERVICED BY" block
         * and enter the location of the aircraft where the
         * inspection was performed in the "LOCATION" block. The
         * person servicing the aircraft will enter the following---
         */
        public List<ServicingData> ServicingData { get; set; }

        /* TOTALS: Enter totals of all consumables used for the
         * mission day.
         * 
         * Note. Line one of the servicing data is the "TOTAL IN-
         * TANKS" at the start of the mission day, and will not be
         * added to the totals at the end of the mission day.
         */
        [Display(Name = "FUEL ADDED (GALLONS)")]
        public int? TotalFuelAdded { get; set; }

        [Display(Name = "OIL 1")]
        public int? TotalOil1 { get; set; }

        [Display(Name = "OIL 2")]
        public int? TotalOil2 { get; set; }

        [Display(Name = "APU")]
        public int? TotalAPU { get; set; }

        [Display(Name = "OXYGEN")]
        public int? TotalOxygen { get; set; }

        [Display(Name = "ANTI-ICING")]
        public int? TotalAntiIcing { get; set; }

        /* 8. TOTALS: If more than one DA Form 2408-12 is used
         * for the mission day, enter the totals, in block 8, on the
         * last page. Units using ULLS-A or LAS will enter,
         * whenever possible, all flight data into the data base at the
         * end of each mission day.
         */

        /* FLIGHT HRS: Enter the total flying hours, in hours and
         * tenths, for the mission day. Reenter total time to the DA
         * Form 2408-13, block 11, CURRENT AIRCRAFT
         * HOURS.
         */
        [Display(Name = "FLIGHT HRS")]
        public double TotalFlightHrs { get; set; }

        /* LANDINGS: STD. AUTO: Enter the total number of
         * landings and touchdown auto rotations made for the
         * mission day. Reenter total landings and touchdown
         * autorotations to the DA Form 2408-13, block 11,
         * LANDINGS.
         */
        [Display(Name = "LANDINGS STD")]
        public int? TotalSTD { get; set; }
        [Display(Name = "AUTO")]
        public int? TotalAUTO { get; set; }

        /* APU: STARTS. HOURS: Enter the total APU starts and
         * hours, in hours and tenths, for the mission day. Reenter
         * total APU starts/hours to the DA Form 2408-13, block 7,
         * APU HISTORY STARTS and HOURS TODAY. If the
         * APU has a start/hour meter use the total starts shown
         * on the start/hour eter at the end of the mission day.
         * Reenter total starts to DA Form 2408-13, block 7, APU
         * HISTORY STARTS TOTAL.
         */
        [Display(Name = "APU STARTS")]
        public int TotalAPUStarts { get; set; }
        [Display(Name = "HOURS")]
        public double TotalAPUHours { get; set; }

        /* HOUR METER HOURS: If the APU has an hour meter,
         * enter the operating hours on the meter at the end of the
         * mission day. Reenter total APU hours to the DA Form
         * 2408-13, block 7, APU HISTORY HR METER TOTAL.
         */
        [Display(Name = "HOUR METER HOURS")]
        public double TotalAPUHourMeterHours { get; set; }

        /* STARTS: #1 #2: For multiengine fixed wing aircraft enter
         * the number of engine starts for the number 1 and
         * number 2 engines. Engine starts while the aircraft is on the
         * ground, or in the air will be recorded for each flight,
         * when required by the applicable aircraft maintenance
         * manual. Reenter the number of engine starts to the DA
         * Form 2408-13, block 9, ENGINE STARTS.
         */
        [Display(Name = "STARTS #1")]
        public int? TotalStarts1 { get; set; }
        [Display(Name = "#2")]
        public int? TotalStarts2 { get; set; }

        /* CYCLES: For fixed wing aircraft enter the total number
         * of times the landing gear was cycled up and down for
         * the mission day. Reenter total cycles to the DA Form
         * 2408-13, block 11, FLIGHT DATA, HSF/CYCLES.
         */
        [Display(Name = "CYCLES")]
        public int? TotalCycles { get; set; }

        /* HSF: For the OH-58D helicopter enter the total Hot
         * Section Factor (HSF) counts for the mission day. Reenter
         * total HSF counts to the DA Form 2408-13, block 11,
         * FLIGHT DATA, on the Today line in HSF/CYCLES
         * column.
         */
        [Display(Name = "HSF")]
        public int? TotalHSF { get; set; }

        /* ROUNDS: 7.62 20MM 30MM 40MM ROCKET TOW
         * HELLFIRE: Enter the total number of rounds fired for
         * each weapon system at the end of the mission day.
         * Reenter the totals to the DA Form 2408-4-1 as
         * required.
         * 
         * Note. For AH-64A Helicopters only, reenter the total
         * number of 30MM cannon rounds fired today to the DA
         * Form 2408-13, block 8, ROUNDS FIRED AIRFRAME
         * TODAY.
         */
        [Display(Name = "ROUNDS 7.62")]
        public int? Total7_62 { get; set; }
        [Display(Name = "20mm")]
        public int? Total20MM { get; set; }
        [Display(Name = "30mm")]
        public int? Total30MM { get; set; }
        [Display(Name = "40mm")]
        public int? Total40MM { get; set; }
        [Display(Name = "ROCKET")]
        public int? TotalRocket { get; set; }
        [Display(Name = "TOW")]
        public int? TotalTow { get; set; }
        [Display(Name = "HELLFIRE")]
        public int? TotalHellfire2 { get; set; }

        private string AircraftId { get; set; }
        private Aircraft Aircraft { get; set; }
    }
}
