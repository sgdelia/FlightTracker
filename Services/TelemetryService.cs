using FlightTracker.Entities;
using Microsoft.EntityFrameworkCore;
using AppContext = FlightTracker.Entities.AppContext;

namespace FlightTracker.Services
{
    public class TelemetryService : ITelemetryService
    {
        private readonly AppContext _context;

        public TelemetryService(AppContext context)
        {
            _context = context;
        }

        public async Task<List<TelemetryReading>> IngestTelemReadingsAsync(int missionId, List<TelemetryReading> telemetryReadings)
        {
            var missionExists = await _context.Missions.AnyAsync(m => m.Id == missionId);
            if (!missionExists)
            {
                throw new InvalidOperationException($"Mission with ID {missionId} does not exist.");
            }

            foreach (var reading in telemetryReadings)
            {
                reading.MissionId = missionId;   
            }
            _context.TelemetryReadings.AddRange(telemetryReadings);
            await _context.SaveChangesAsync();
            return telemetryReadings;
        }

        public async Task<List<TelemetryReading>> GetTelemReadingsAsync(int missionId, DateTime? startTime = null, DateTime? endTime = null, int? limit = null)
        {
            var query = _context.TelemetryReadings.Where(tr => tr.MissionId == missionId);

            if (startTime.HasValue)
            {
                query = query.Where(tr => tr.Timestamp >= startTime.Value);
            }

            if (endTime.HasValue)
            {
                query = query.Where(tr => tr.Timestamp <= endTime.Value);
            }

            if (limit.HasValue)
            {
                query = query.Take(limit.Value);
            }

            return await query.OrderBy(tr => tr.Timestamp).Take(limit ?? int.MaxValue).ToListAsync();
        }
    }
}