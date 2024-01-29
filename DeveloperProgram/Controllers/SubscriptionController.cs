using CSUI_Teams_Sync.Components.Commons;
using CSUI_Teams_Sync.Components.Configurations;
using CSUI_Teams_Sync.Library;
using CSUI_Teams_Sync.Models;
using CSUI_Teams_Sync.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CSUI_Teams_Sync.Controllers
{
    [ApiController]
    [Route("/api/v1/subscription")]
    public class SubscriptionController : ControllerBase
    {
        private readonly TeamsGraphAPIConfig _teamsGraphAPIConfig;
        private readonly SyncService _syncService;
        private readonly NLog.Logger _logger;
        public SubscriptionController(IOptions<TeamsGraphAPIConfig> teamsGraphAPIConfig, LogManagerCustom logManagerCustom, SyncService syncService)
        {
            _teamsGraphAPIConfig = teamsGraphAPIConfig.Value;
            _logger = logManagerCustom.logger;
            _syncService = syncService;
        }

        [HttpPost("")]
        public async Task<ActionResult<string>> Post(string? validationToken = null)
        {
            try
            {
                Console.WriteLine("Subs Got");
                if(!string.IsNullOrEmpty(validationToken))
                {
                    return Ok(validationToken);
                }

                using StreamReader reader = new(Request.Body);
                string content = await reader.ReadToEndAsync();

                DbService dbService = new();
                OTCSService otcsService = new();

                var link = dbService.GetRecentDeltaLink();

                string accessToken = await TeamsGraphAPIHandler.GetAccessToken(_teamsGraphAPIConfig.TenantId);

                var deltaItems = await TeamsGraphAPIHandler.GetDeltaItems(accessToken, link);
                dbService.CreateDeltaLink(deltaItems.DeltaLink);

                Console.WriteLine(deltaItems.Value.Count);

                if(deltaItems.Value.Count > 0)
                {
                    Console.WriteLine("Syncing File...");
                    var item = deltaItems.Value.Last();

                    if(item.Deleted != null)
                    {
                        if(item.Deleted.State == "deleted")
                        {
                            var node = dbService.GetItemNodeIDByDriveID(item.ID);
                            await otcsService.DeleteItem(await otcsService.GetTicket(), node.ToString());

                            Console.WriteLine("File Deleted");

                            return Ok();
                        }
                    } 
                    else
                    {
                        var downloadUrl = await TeamsGraphAPIHandler.GetItemDownloadUrl(accessToken, item.ID);
                        var parentID = dbService.GetItemNodeIDByDriveID(item.ParentReference.ID);

                        Console.WriteLine(parentID);

                        var node = await DownloadFile.DownloadFileAsync(downloadUrl, parentID, item.Name, await otcsService.GetTicket());
                        dbService.CreateItem(node.id, item.Name, item.ID);

                        Console.WriteLine("New File Synced");
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Ok();
            }
        }
    }
}
