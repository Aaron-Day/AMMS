using AMMS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AMMS.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Request> Requests { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<AircraftModel> AircraftModels { get; set; }
        public DbSet<Aircraft> Aircraft { get; set; }
        public DbSet<DA2408_5> Modifications { get; set; }
        public DbSet<DA2408_12> Flights { get; set; }
        public DbSet<DA2408_13> Statuses { get; set; }
        public DbSet<DA2408_13_1> MaintenanceRecords { get; set; }
        public DbSet<DA2408_13_2> RelatedMaint { get; set; }
        public DbSet<DA2408_14_1> UncorrectedFaults { get; set; }
        public DbSet<DA2408_15> Historicals { get; set; }
        public DbSet<DA2408_16> CompHistoricals { get; set; }
        public DbSet<DA2408_16_1> HistoryRecorders { get; set; }
        public DbSet<DA2408_17> Inventory { get; set; }
        public DbSet<DA2408_18> Inspections { get; set; }
        public DbSet<DA2408_19_2> EngAnalysisCks { get; set; }
        public DbSet<DA2408_19_3> EngCompOpHrs { get; set; }
        public DbSet<DA2408_20> OilAnalysisLogs { get; set; }
        public DbSet<DA2408_31> IdCards { get; set; }
        public DbSet<FlightData> FlightData { get; set; }
        public DbSet<PersonnelData> PersonnelData { get; set; }
        public DbSet<DutyPosition> DutyPositions { get; set; }
        public DbSet<ServicingData> ServicingData { get; set; }
        public DbSet<EngineHitReading> EngineHitReadings { get; set; }
        public DbSet<Fault> Faults { get; set; }
        public DbSet<Correction> Corrections { get; set; }
        public DbSet<IndividualSignOff> Individuals { get; set; }
        public DbSet<Action> Actions { get; set; }
        public DbSet<ActionCorrection> ActionCorrections { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
