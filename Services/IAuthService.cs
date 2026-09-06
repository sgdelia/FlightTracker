using FlightTracker.Entities;

namespace FlightTracker.Services
{
    public interface IAuthService
    {
        Task<Boolean> RegisterAsync(string username, string password);
        Task<string?> LoginAsync(string username, string password);
    }
}