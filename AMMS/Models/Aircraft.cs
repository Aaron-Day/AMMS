using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AMMS.Models
{
    public class Aircraft
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [RegularExpression(@"^\d{7}$", ErrorMessage = "Invalid Serial Number")]
        [Display(Name = "Serial Number")]
        public string SerialNumber { get; set; }

        [Required]
        public double AcftHrs { get; set; }

        public List<DA2408_5> Modifications { get; set; }
        public List<DA2408_12> Flights { get; set; }
        public DA2408_13 Status { get; set; }
        public List<DA2408_13_1> Faults { get; set; }
        public List<DA2408_14_1> UncorrectedFaults { get; set; }
        public List<DA2408_15> Historicals { get; set; }
        public List<DA2408_16> ComponentHistoricals { get; set; }
        public List<DA2408_16_1> HistoryRecorders { get; set; }
        public List<DA2408_17> Inventory { get; set; }
        public List<DA2408_18> Inspections { get; set; }
        public List<DA2408_19_2> EngineAnalysisChecks { get; set; }
        public List<DA2408_19_3> EngineComponentOpHrs { get; set; }
        public List<DA2408_20> OilAnalysisLogs { get; set; }
        public DA2408_31 IdCard { get; set; }

        public string AircraftModelId { get; set; }
        public AircraftModel AircraftModel { get; set; }

        public string UnitId { get; set; }
        public Unit Unit { get; set; }

        public void AddInspection(DA2408_18 inspection)
        {
            Inspections.Add(inspection);
        }
    }
}
