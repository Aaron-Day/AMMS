using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AMMS.Models
{
    public class DA2408_13
    {
        /* DA PAM 738-751
         * Page 45
         * DA Form 2408-13
         * Aircraft Status Information Record
         */

        [Key]
        public string Id { get; set; }

        /* DATE: The pilot will enter the date (dd mmm yy) for the
         * first flight of the mission day.
         */
        [Display(Name = "DATE")]
        public DateTime? Date { get; set; }

        /* NUMBER OF PAGES IN FLIGHT PACK: Enter the total
         * number of pages (black lead pencil) in the Flight Pack
         * (DA Forms 2408-13-1, 2408-13-2, and 2408-13-3.)
         * This entry will change when a DA Form 2408-13-1,
         * 2408-13-2 or 2408-13-3 is added to the Flight Pack.
         */
        [Display(Name = "NUMBER OF PAGES IN FLIGHT PACK")]
        public int? NumPages { get; set; }

        /* 1. AIRCRAFT SERIAL NUMBER: Enter the aircraft serial
         * number (seven numerics, no dashes). Computer
         * generated for ULLS-A/LAS users.
         */
        [Required]
        [Display(Name = "AIRCRAFT SERIAL NUMBER")]
        [RegularExpression(@"^\d{7}$", ErrorMessage = "Invalid Serial Number")]
        public string AircraftSerialNumber { get; set; }

        /* 2. MODEL: Enter the aircraft Mission Design Series.
         * Computer generated for ULLS-A/LAS users.
         */
        [Required]
        [Display(Name = "MODEL")]
        public string Model { get; set; }

        /* 3. UIC: Enter the unit identification code of the unit or
         * activity owning the aircraft. Computer generated for
         * ULLS-A/LAS users.
         */
        [Required]
        [Display(Name = "UIC")]
        [StringLength(6, MinimumLength = 6)]
        public string UIC { get; set; }

        /* 4. STATION: Enter the aircraft's home station.
         * Computer generated for ULLS-A/LAS users.
         */
        [Required]
        [Display(Name = "STATION")]
        public string Station { get; set; }

        /* 5. NAME OF CE/MECH: Enter the name of the
         * assigned crew chief or mechanic.
         */
        [Display(Name = "NAME OF CE/MECH")]
        public string CrewChief { get; set; }

        /* 6. ENGINE HIT READINGS:
         * 
         * Note. Leave these blocks blank when using the manual
         * method of recordkeeping. Computer generated when using
         * ULLS-A/LAS.
         */
        public List<EngineHitReading> HitReadings { get; set; }

        /* 7. APU HISTORY:
         * HOURS, CURRENT: Applies to aircraft without an hour
         * meter installed. Enter the total APU operating hours, in
         * hours and tenths, from the last mission day DA Form
         * 2408-13. Computer generated for users of ULLS-A/
         * LAS.
         */
        [Display(Name = "CURRENT HOURS")]
        public double? CurrentApuHours { get; set; }

        /* HOURS, TODAY: Enter the APU operating hours, in
         * hours and tenths, for the current mission day. Get this
         * information from the backside of the current DA Form
         * 2408-12, block 8, TOTALS, APU: HOURS.
         */
        [Display(Name = "TODAY HOURS")]
        public double? TodayApuHours { get; set; }

        /* HOURS, TOTAL: Add the cURRENT and TODAY operating
         * hours for the total operating hours.
         */
        [Display(Name = "TOTAL HOURS")]
        public double? TotalApuHours { get; set; }

        /* STARTS, CURRENT: Enter the total APU Starts from
         * the last mission day DA Form 2408-13. Computer
         * generated for users of ULLS-A/LAS.
         */
        [Display(Name = "CURRENT STARTS")]
        public int CurrentApuStarts { get; set; }

        /* STARTS, TODAY: Enter the number of APU starts for
         * the current mission day. Get this information from the
         * backside of the current DA Form 2408-12, block 8,
         * TOTALS, APU: STARTS. If the APU starts are shown on a
         * Start/Hour Meter, leave this block blank.
         */
        [Display(Name = "TODAY STARTS")]
        public int? TodayApuStarts { get; set; }

        /* STARTS, TOTAL: Add the CURRENT and TODAY
         * STARTS for the total starts. If starts are shown on a
         * Start/Hour Meter enter the number of starts on the
         * meter, or from the backside of current DA Form 2408-12,
         * block 8, TOTALS, APU: STARTS.
         */
        [Display(Name = "TOTAL STARTS")]
        public int? TotalApuStarts { get; set; }

        /* HR METER, CURRENT: Applies to aircraft with an hour
         * meter installed. Enter the total APU hour meter reading.
         * Get the information from the last mission day DA Form
         * 2408-13. Computer generated for users of ULLS-A/
         * LAS.
         */
        [Display(Name = "CURRENT HR METER")]
        public double? CurrentHrMeter { get; set; }

        /* HR METER, TOTAL: Enter the APU hour meter reading.
         * Get this information from the backside of the current
         * DA Form 2408-12, block 8. TOTALS, HOUR METER
         * HOURS.
         */
        [Display(Name = "TOTAL HR METER")]
        public double? TotalHrMeter { get; set; }

        /* 8. ROUNDS FIRED AIRFRAME: For AH-64A helicopters,
         * all others leave blank.
         * CURRENT: Enter the total rounds fired from the 30MM
         * cannon(s) presently and previously installed in the
         * aircraft. Get this information from the last mission day DA
         * Form 2408-13. Computer generated for users of ULLS-
         * A/LAS.
         */
        [Display(Name = "CURRENT ROUNDS")]
        public int? CurrentRounds { get; set; }

        /* TODAY: Enter the number of rounds fired from the
         * 30MM cannon installed. Get this information from the
         * backside of the current DA Form 2408-12, block 8.
         * TOTALS, ROUNDS, 30MM.
         */
        [Display(Name = "TODAY ROUNDS")]
        public int? TodayRounds { get; set; }

        /* TOTAL: Add the CURRENT and TODAY rounds fired
         * for the total rounds fired.
         */
        [Display(Name = "TOTAL ROUNDS")]
        public int? TotalRounds { get; set; }

        /* 9. ENGINE STARTS:
         * Note. For multiengine fixed wing aircraft.
         * NO 1, CURRENT: Enter the total number 1 engine
         * starts from the last mission day DA Form 2408-13.
         * Computer generated for users of ULLS-A/LAS.
         */
        [Display(Name = "CURRENT STARTS NO. 1")]
        public int? CurrentStarts1 { get; set; }

        /* NO 1, TODAY: Enter the total number 1 engine starts
         * for the current mission day. Get this information from the
         * backside of the current DA Form 2408-12, block 8,
         * TOTALS, STARTS #1.
         */
        [Display(Name = "TODAY STARTS NO. 1")]
        public int? TodayStarts1 { get; set; }

        /* NO 1, TOTAL: Add the CURRENT and TODAY engine
         * starts for the total number 1 engine starts.
         */
        [Display(Name = "TOTAL STARTS NO. 1")]
        public int? TotalStarts1 { get; set; }

        /* NO 2, CURRENT: Enter the total number 2 engine
         * starts from the last mission day DA Form 2408-13.
         * Computer generated for users of ULLS-A/LAS.
         */
        [Display(Name = "CURRENT STARTS NO. 2")]
        public int? CurrentStarts2 { get; set; }

        /* NO 2, TODAY: Enter the total number 2 engine starts
         * for the current mission day. Get this information from the
         * backside of the current DA Form 2408-12, block 8,
         * TOTALS, STARTS #2.
         */
        [Display(Name = "TODAY STARTS NO. 2")]
        public int? TodayStarts2 { get; set; }

        /* NO 2, TOTAL: Add the CURRENT and TODAY engine
         * starts for the total number 2 engine starts.
         */
        [Display(Name = "TOTAL STARTS NO. 1")]
        public int? TotalStarts2 { get; set; }

        /* 10. SYSTEM STATUS. In these boxes enter the status
         * symbols, for the most serious uncorrected faults for
         * aircraft, and mission related systems listed in the Fault
         * Information block on the DA Form 2408-13-1, DA Form
         * 2408-13-2, DA Form 2408-13-3, and the DA Form
         * 2408-14-1 starting in the top left box. When there are
         * no faults on the aircraft or mission related systems, the
         * mechanic or crew chief will enter their last name initial. If
         * a fault is corrected, or a new fault is added, and the
         * status changes, enter the new status symbol in the next
         * open box to the right (for aircraft, there are 14 boxes
         * that can be used). When all boxes are used, make out
         * another DA Form 2408-13 and enter the status symbol
         * in the top left box. Once you have entered a status
         * symbol in any column in the SYSTEM STATUS block,
         * you cannot erase, initial over, or change it. Record any
         * status changes during the mission day in the next open
         * box to the right. Clear any incorrect entries as shown in
         * paragraph 1-8.
         * ACFT: Enter the current condition status symbol for the
         * aircraft starting in the top left box. Computer generated
         * for users of ULLS-A/LAS.
         */
        [Display(Name = "SYSTEM STATUS ACFT")]
        [StringLength(14)]
        public string SystemStatusAcft { get; set; }

        /* ARM: Enter the current status symbol for mission
         * related armament systems starting in the left box.
         * Computer generated for users of ULLS-A/LAS.
         */
        [Display(Name = "ARM")]
        [StringLength(7)]
        public string SystemStatusArm { get; set; }

        /* ELECT: Enter the current status symbol for mission
         * related electronic equipment, such as, radar, camera,
         * infrared, and so on, starting in the left box. Computer
         * generated for users of ULLS-A/LAS.
         */
        [Display(Name = "ELECT")]
        [StringLength(7)]
        public string SystemStatusElect { get; set; }

        /* OTHER: Enter the current status symbol for any other
         * mission related aviation associated equipment, such as,
         * rescue hoist starting in the left box. Computer generated
         * for users of ULLS-A/LAS.
         */
        [Display(Name = "OTHER")]
        [StringLength(7)]
        public string SystemStatusOther { get; set; }

        /* 11. FLIGHT DATA:
         * AIRCRAFT HOURS:
         * CURRENT: Enter the current total aircraft operating
         * time, in hours and tenths. This time can be found on the
         * previous mission day DA Form 2408-13. Computer
         * generated for users of ULLS-A/LAS.
         */
        [Display(Name = "CURRENT HOURS")]
        public double CurrentHours { get; set; }

        /* TODAY: Enter the aircraft flying time, in hours and
         * tenths, for Today's mission. Get this time from the
         * backside of the DA Form 2408-12, block 8, TOTALS,
         * FLIGHT HRS.
         */
        [Display(Name = "TODAY HOURS")]
        public double TodayHours { get; set; }

        /* TOTAL: Add the CURRENT and TODAY time for the
         * total time, in hours and tenths.
         */
        [Display(Name = "TOTAL HOURS")]
        public double TotalHours { get; set; }

        /* LANDINGS, STD AUTO:
         * CURRENT: Enter the total standard landings and touchdown
         * autorotations since the first day of the reporting
         * period. Landings and autorotations can be found on the
         * previous mission day DA Form 2408-13. Computer
         * generated for users of the ULLS-A/LAS.
         */
        [Display(Name = "CURRENT LANDINGS STD")]
        public int CurrentLandingsStd { get; set; }
        [Display(Name = "CURRENT LANDINGS AUTO")]
        public int CurrentLandingsAuto { get; set; }

        /* TODAY: Enter the standard landings and touchdown
         * autorotations accomplished today. Get this information
         * from the backside of the DA Form 2408-12, block 8,
         * TOTALS, LANDINGS: STD, AUTO.
         */
        [Display(Name = "TODAY LANDINGS STD")]
        public int TodayLandingsStd { get; set; }
        [Display(Name = "TODAY LANDINGS AUTO")]
        public int TodayLandingsAuto { get; set; }

        /* TOTAL: Add the CURRENT to the TODAY for the total
         * landings and autorotations. If this form is for the last day
         * of the report period, do not reenter the total landings
         * and touchdown auto rotations to a new DA Form 2408-13.
         * 
         * Note. Before the first flight of a new 1352 Reporting
         * Period, line out the CURRENT Landings and enter "0."
         * DO NOT zero the landings if the aircraft TM-23 requires
         * component replacement or inspections based on total
         * cumulative landings.
         */
        [Display(Name = "TOTAL LANDINGS STD")]
        public int TotalLandingsStd { get; set; }
        [Display(Name = "TOTAL LANDINGS AUTO")]
        public int TotalLandingsAuto { get; set; }

        /* HSF/CYCLES:
         * CURRENT: For fixed wing aircraft with retractable landing
         * gear, enter the total cycles for the landing gear. For
         * OH-58 aircraft enter the total HSF counts. Cycles or
         * HSF counts can be found on the previous mission day
         * DA Form 2408-13. Computer generated for users of
         * ULLS-A/LAS.
         */
        [Display(Name = "CURRENT HSF/CYCLES")]
        public int? CurrentHsfCycles { get; set; }

        /* TODAY: For fixed wing aircraft with retractable landing
         * gear enter the total number of times the landing gear
         * was cycled today. Get this information from the backside
         * of the DA Form 2408-12, block 8, TOTALS, CYCLES.
         * Do not enter any HSF counts on this line.
         */
        [Display(Name = "TODAY CYCLES")]
        public int? TodayCycles { get; set; }

        /* TOTAL: For fixed wing aircraft with retractable landing
         * gear add the CURRENT to the TODAY for total cycles.
         * For the OH-58D helicopter enter the mission day's HSF
         * counts on this line. Get this count from the backside of
         * the DA Form 2408-12, block 8, TOTALS, HSF.
         * 
         * Note. Compare the HSF counts in this block to the readings
         * in the CURRENT Block to ensure proper operation
         * of the multifunction display. When an engine is replaced,
         * enter the total HSF counts on the DA Form 2408-16,
         * block 7, SIGNIFICANT HISTORICAL DATA and on DA
         * Form 2410, block 13a, LCF1. Line out the CURRENT
         * HSF counts on the DA Form 2408-13 when the replacement
         * engine is installed and enter the new HSF counts
         * for the replacement engine. Update the multifunction
         * display with the correct HSF counts for the replacement
         * engine.
         */
        [Display(Name = "TOTAL HSF/CYCLES")]
        public int? TotalHsfCycles { get; set; }

        /* 12. SCHEDULED INSPECTION INFORMATION:
         * a. HOURS OF OPERATION SINCE LAST GENERATION:
         * Leave blank when using the manual method of
         * recordkeeping. Computer generated for ULLS-A/LAS
         * users.
         */
        [Display(Name = "HOURS OF OPERATION SINCE LAST GENERATION")]
        public double? LastGeneration { get; set; }

        /* b. NEXT PHASE/SCHEDULED INSP (NO): Enter the
         * number of the next phase or scheduled inspection.
         * Computer generated by ULLS-A.
         */
        [Display(Name = "NEXT PHASE/SCHEDULED INSP (NO.)")]
        public int? NextPhaseNo { get; set; }

        /* c. NEXT PHASE/SCHEDULED INSP DUE AT: Enter
         * the aircraft hours the next phase or scheduled inspection
         * is due. Computer generated by ULLS-A.
         */
        [Display(Name = "NEXT PHASE/SCHEDULED INSP DUE AT")]
        public DateTime? NextPhaseDue { get; set; }

        /* d. HOURS OF OPERATION TO NEXT PHASE/
         * SCHEDULED INSPECTION: Leave blank when using
         * the manual method of recordkeeping. Computer
         * generated by ULLS-A.
         */
        [Display(Name = "HOURS OF OPERATION TO NEXT PHASE/SCHEDULED INSPECTION")]
        public double? NextPhaseHrsRemaining { get; set; }

        /* e. PMD DUE: This block is left blank for Non-PMD
         * aircraft.
         * DATE COMPLETED: Enter the date (dd mmm yy) the
         * preventative maintenance daily inspection was completed.
         * DO NOT erase. For aircraft under PMS maintenance
         * leave blank.
         */
        [Display(Name = "PMD DUE DATE COMPLETED")]
        public DateTime? PmdDueDate { get; set; }

        /* PID: Enter the PID of the individual who completed the
         * daily inspection.
         * 
         * Note. If any faults are found during the PMD they will be
         * entered on the DA Form 2408-13-1 in the first open
         * Fault Information block.
         */
        [Display(Name = "PID")]
        public string PmdPid { get; set; }

        /* 13. LOCAL USE: Enter the ground run time and starts
         * for APU's without hour meters that require operating
         * hour and start data per the TM 23 and TB
         * 1-1500-341-01 (For example, 10 JAN 95, 1 start, 5
         * hrs.) This data will be combined with the hours and start
         * data for the first flight of the next mission day and
         * entered on DA Form 2408-12, block 6.a., APU:
         * STARTS HOURS for that flight. Other data may be
         * entered in this block as directed as directed by unit commander.
         */
        [Display(Name = "LOCAL USE")]
        public string LocalUse { get; set; }

        private string AircraftId { get; set; }
        private Aircraft Aircraft { get; set; }
    }
}
