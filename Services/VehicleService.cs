using FlightTracker.Entities;
using Microsoft.EntityFrameworkCore;
using AppContext = FlightTracker.Entities.AppContext;

namespace FlightTracker.Services
{
    
    public class VehicleService : IVehicleService
    {
        private readonly AppContext _context;

        public VehicleService(AppContext context)
        {
            _context = context;
        }

        public async Task<List<Vehicle>> GetAllVehiclesAsync()
        {
            return await _context.Vehicles.Include(v => v.Missions).ToListAsync();
        }

        public async Task<Vehicle?> GetVehicleByIdAsync(int id)
        {
            return await _context.Vehicles.Include(v => v.Missions).FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<Vehicle> CreateVehicleAsync(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();
            return vehicle;
        }
    }
}