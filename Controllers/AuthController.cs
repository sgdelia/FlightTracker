using Microsoft.AspNetCore.Mvc;
using FlightTracker.Entities;
using FlightTracker.Services;
using Microsoft.AspNetCore.Authorization;

namespace FlightTracker.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var success = await _authService.RegisterAsync(request.Username, request.Password);
            if (success)
            {
                return Ok(new { message = "User registered successfully" });
            }
            else
            {
                return BadRequest(new { message = "Username already exists" });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
        {
           var token = await _authService.LoginAsync(request.Username, request.Password);
           if (token == null)
           {
               return Unauthorized(new { message = "Invalid username or password" });
           }
           return Ok(new AuthResponse { Token = token });
        }

        
    }
}