using FlightTracker.Entities;

namespace FlightTracker.Services
{
    public interface IMissionService
    {
        Task<List<Mission>> GetAllMissionsAsync();
        Task<Mission?> GetMissionByIdAsync(int id);
        Task<Mission> CreateMissionAsync(Mission mission);
        Task<MissionSummaryDto?> GetMissionSummaryAsync(int id);
    }
}