using FlightTracker.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightTracker.Entities
{
    public class MissionSummaryDto
    {
        public int MissionId { get; set; }
        public double MaxAltitude { get; set; }
        public double AvgBatteryPctPerHour { get; set; }
        public double DurationInHours { get; set; }
        public double TelemetryCount { get; set; }
    }
}