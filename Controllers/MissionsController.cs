using Microsoft.AspNetCore.Mvc;
using FlightTracker.Entities;
using FlightTracker.Services;

namespace FlightTracker.Controllers
{
    [ApiController]
    [Route("missions")]
    public class MissionsController : ControllerBase
    {
        private readonly IMissionService _missionService;

        public MissionsController(IMissionService missionService)
        {
            _missionService = missionService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Mission>>> GetAllMissions()
        {
            var missions = await _missionService.GetAllMissionsAsync();
            return Ok(missions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Mission>> GetMissionById(int id)
        {
            var mission = await _missionService.GetMissionByIdAsync(id);
            if (mission == null)
            {
                return NotFound();
            }
            return Ok(mission);
        }

        [HttpPost]
        public async Task<ActionResult<Mission>> CreateMission(Mission mission)
        {
            try
            {
                var createdMission = await _missionService.CreateMissionAsync(mission);
                return CreatedAtAction(nameof(GetMissionById), new { id = createdMission.Id }, createdMission);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}