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
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Inspection> Inspections { get; set; }
        public DbSet<Fault> Faults { get; set; }
        public DbSet<RelatedMaintenance> RelatedMaintenance { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
