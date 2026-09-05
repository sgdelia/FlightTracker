using FlightTracker.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightTracker.Services
{
    public interface ITelemetryService
    {
        Task<List<TelemetryReading>> IngestTelemReadingsAsync(int missionId, List<TelemetryReading> telemetryReadings);
        Task<List<TelemetryReading>> GetTelemReadingsAsync(int missionId, DateTime? startTime = null, DateTime? endTime = null, int? limit = null);
    }
}