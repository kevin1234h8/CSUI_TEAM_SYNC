using CSUI_Teams_Sync.Components.Commons;
using CSUI_Teams_Sync.Services;
using Microsoft.AspNetCore.Mvc;

namespace CSUI_Teams_Sync.Controllers
{
    [ApiController]
    [Route("/api/v1/sync")]
    public class SyncController : ControllerBase
    {
        private readonly SyncService _syncService;
        private readonly NLog.Logger _logger;
        public SyncController(LogManagerCustom logManagerCustom, SyncService syncService)
        {
            _logger = logManagerCustom.logger;
            _syncService = syncService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetTeams()
        {
            try
            {
                var teams = await _syncService.GetTeamsService();
                return Ok(teams);
            }
            catch (Exception ex)
            {
                _logger.Error($"TeamsController |GetAccessToken | error message : {ex.Message}");
                _logger.Trace(ex.StackTrace);
                return StatusCode(500, new { Error = "Internal Server Error", Message = ex.Message });
            }
        }
    }
}
