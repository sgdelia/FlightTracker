using FlightTracker.Entities;
using Microsoft.EntityFrameworkCore;
using AppContext = FlightTracker.Entities.AppContext;

namespace FlightTracker.Services
{
    
    public class MissionService : IMissionService
    {
        private readonly AppContext _context;

        public MissionService(AppContext context)
        {
            _context = context;
        }

        public async Task<List<Mission>> GetAllMissionsAsync()
        {
            return await _context.Missions.ToListAsync();
    }

        public async Task<Mission?> GetMissionByIdAsync(int id)
        {
            return await _context.Missions.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Mission> CreateMissionAsync(Mission mission)
        {
            var VehicleExists = await _context.Vehicles.AnyAsync(v => v.Id == mission.VehicleId);
            if (!VehicleExists)
            {
                throw new InvalidOperationException($"Vehicle with ID {mission.VehicleId} does not exist.");
            }
            _context.Missions.Add(mission);
            await _context.SaveChangesAsync();
            return mission;
        }

        public async Task<MissionSummaryDto?> GetMissionSummaryAsync(int id)
        {
            var missionExists = _context.Missions.Any(m => m.Id == id);
            if (!missionExists)
            {
                return null;
            }

            var readingsQuery = _context.TelemetryReadings.Where(t => t.MissionId == id);
            var count = await readingsQuery.CountAsync();
            if (count == 0)
            {
                return new MissionSummaryDto
                {
                    MissionId = id,
                    TelemetryCount = 0,
                };
            }
            var maxAltitude = await readingsQuery.MaxAsync(t => t.Altitude);
            var firstReading = await readingsQuery.OrderBy(t => t.Timestamp).FirstAsync();
            var lastReading = await readingsQuery.OrderByDescending(t => t.Timestamp).FirstAsync();

            var durationInHours = (lastReading.Timestamp - firstReading.Timestamp).TotalHours;
            var batteryDrop = firstReading.BatteryPct - lastReading.BatteryPct;
            var avgBatteryPctPerHour = durationInHours > 0 ? batteryDrop / durationInHours : 0;

            return new MissionSummaryDto
            {
                MissionId = id,
                MaxAltitude = maxAltitude,
                AvgBatteryPctPerHour = Math.Round(avgBatteryPctPerHour, 2),
                DurationInHours = Math.Round(durationInHours, 2),
                TelemetryCount = count
            };

        }
    }
}