using Microsoft.EntityFrameworkCore;

namespace FlightTracker.Entities
{
    public class Vehicle
    {
        
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
    public class Mission
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; } = string.Empty;
    }
    public class TelemetryReading
    {
        public int Id { get; set; }
        public int MissionId { get; set; }
        public DateTime Timestamp { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
        public double BatteryPct { get; set; }
        public double SignalStrength { get; set; }
    }
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