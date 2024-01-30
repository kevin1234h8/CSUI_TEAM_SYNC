using CSUI_Teams_Sync.Components.Commons;
using CSUI_Teams_Sync.Components.Configurations;
using CSUI_Teams_Sync.Library;
using CSUI_Teams_Sync.Models;
using CSUI_Teams_Sync.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CSUI_Teams_Sync.Controllers
{
    [ApiController]
    [Route("/api/v1/teams")]
    public class TeamsController : ControllerBase
    {
        private readonly DbService _dbService;
        private readonly TeamsGraphAPIConfig _teamsGraphAPIConfig;
        private readonly NLog.Logger _logger;
        public TeamsController(IOptions<TeamsGraphAPIConfig> teamsGraphAPIConfig, LogManagerCustom logManagerCustom, DbService dbService)
        {
            _teamsGraphAPIConfig = teamsGraphAPIConfig.Value;
            _logger = logManagerCustom.logger;
            _dbService = dbService;
        }

        [HttpGet("accesstoken")]
        public async Task<IActionResult> GetAccessToken()
        {
            try
            {
                var accessToken = await TeamsGraphAPIHandler.GetAccessToken(_teamsGraphAPIConfig.TenantId);
                return Ok(new { accessToken });
            }
            catch (Exception ex)
            {
                _logger.Error($"TeamsController |GetAccessToken | error message : {ex.Message}");
                _logger.Trace(ex.StackTrace);
                return StatusCode(500, new { Error = "Internal Server Error", Message = ex.Message });
            }
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                string accessToken = await TeamsGraphAPIHandler.GetAccessToken(_teamsGraphAPIConfig.TenantId);
                var users = await TeamsGraphAPIHandler.GetUsers(accessToken);
                return Ok(new { users });
            }
            catch (Exception ex)
            {
                _logger.Error($"TeamsController |GetUsers | error message : {ex.Message}");
                _logger.Trace(ex.StackTrace);
                return StatusCode(500, new { Error = "Internal Server Error", Message = ex.Message });
            }
        }

        [HttpGet("users/{displayName}")]
        public async Task<IActionResult> GetUserByDisplayName(string displayName)
        {
            try
            {
                string accessToken = await TeamsGraphAPIHandler.GetAccessToken(_teamsGraphAPIConfig.TenantId);
                var userId = await TeamsGraphAPIHandler.GetUserByDisplayName(accessToken, displayName);
                return Ok(new { userId });
            }
            catch (Exception ex)
            {
                _logger.Error($"TeamsController | GetUserByDisplayName | error message : {ex.Message}");
                _logger.Trace(ex.StackTrace);

                return StatusCode(500, new { Error = "Internal Server Error", Message = ex.Message });
            }
        }
        [HttpGet("users/{displayName}/id")]
        public async Task<IActionResult> GetUserIdByDisplayName(string displayName)
        {
            try
            {
                string accessToken = await TeamsGraphAPIHandler.GetAccessToken(_teamsGraphAPIConfig.TenantId);
                var user = await TeamsGraphAPIHandler.GetUserIdByDisplayName(accessToken, displayName);
                return Ok(new { user });
            }
            catch (Exception ex)
            {
                _logger.Error($"TeamsController | GetUserIdByDisplayName | error message : {ex.Message}");
                _logger.Trace(ex.StackTrace);
                return StatusCode(500, new { Error = "Internal Server Error", Message = ex.Message });
            }
        }

        [HttpGet("users/{userId}/chats")]
        public async Task<IActionResult> GetUserChats(string userId)
        {
            try
            {
                string accessToken = await TeamsGraphAPIHandler.GetAccessToken(_teamsGraphAPIConfig.TenantId);
                var chats = await TeamsGraphAPIHandler.GetUserChats(accessToken, userId);
                return Ok(new { userId = userId, chats });
            }
            catch (Exception ex)
            {
                _logger.Error($"TeamsController | GetUserChats | error message : {ex.Message}");
                _logger.Trace(ex.StackTrace);
                return StatusCode(500, new { Error = "Internal Server Error", Message = ex.Message });
            }
        }

        [HttpGet("allTeams")]
        public async Task<IActionResult> AllTeams()
        {
            try
            {
                var result = _dbService.GetTeamsJoinChannel();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Internal Server Error", Message = ex.Message });
            }
        }

        [HttpPut("team")]
        public async Task<IActionResult> EditTeam([FromBody] EditTeam body)
        {
            APIResponse<EditTeam> result = new()
            {
                Data = body
            };

            try
            {
                var existingConfigs = _dbService.GetChannelConfiguration(body.Channel.ID);

                _dbService.DeleteChannelConfigurationByChannelID(body.Channel.ID);

                foreach (var configID in body.Channel.Items)
                {
                    _dbService.CreateChannelConfiguration(body.Channel.ID, configID);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                result.Message = "INTERNAL_SERVER_ERROR";
                return Ok(result);
            }
        }
        
        [HttpGet("post/channel/{channelID}")]
        public async Task<IActionResult> GetPosts(string channelID)
        {
            APIResponse<string> result = new()
            {
                Data = "<div>COMING SOON</div>"
            };

            try
            {
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                result.Message = "INTERNAL_SERVER_ERROR";
                return Ok(result);
            }
        }
    }
}
