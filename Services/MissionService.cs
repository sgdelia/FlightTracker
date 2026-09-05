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
    }
}