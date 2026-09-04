using Microsoft.EntityFrameworkCore;

namespace FlightTracker.Entities
{
    public class Vehicle{}
    public class Mission{}
    public class TelemetryReading{}
    public class User{}

    public class AppContext: DbContext
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options) { }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Mission> Missions { get; set; }
        public DbSet<TelemetryReading> TelemetryReadings { get; set; }
        public DbSet<User> Users { get; set; }
    }
}