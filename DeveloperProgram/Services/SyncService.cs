using CSUI_Teams_Sync.Components.Commons;
using CSUI_Teams_Sync.Components.Configurations;
using CSUI_Teams_Sync.Library;
using CSUI_Teams_Sync.Models;
using Microsoft.Extensions.Options;

namespace CSUI_Teams_Sync.Services
{
    public class SyncService
    {
        private readonly TeamsGraphAPIConfig _teamsGraphAPIConfig;
        private readonly string enterprisePath = "C:/Work/Enterprise/Team Sync";
        private readonly NLog.Logger _logger;
        public SyncService(IOptions<TeamsGraphAPIConfig> teamsGraphAPIConfig, LogManagerCustom logManagerCustom)
        {
            _teamsGraphAPIConfig = teamsGraphAPIConfig.Value;
            _logger = logManagerCustom.logger;
        }
        public async Task<List<Team>> GetTeamsService()
        {
            try
            {
                string accessToken = await TeamsGraphAPIHandler.GetAccessToken(_teamsGraphAPIConfig.TenantId);
                var teams = await TeamsGraphAPIHandler.GetTeams(accessToken);

                foreach (Team team in teams)
                {
                    string teamPath = $"{enterprisePath}/{team.displayName}";
                    Directory.CreateDirectory(teamPath);

                    string filesPath = $"{teamPath}/Files & Folders";
                    Directory.CreateDirectory(filesPath);

                    var channels = await TeamsGraphAPIHandler.GetChannelsByTeamID(accessToken, team.id);

                    foreach (var channel in channels) 
                    { 
                        var filesFolders = await TeamsGraphAPIHandler.GetFileFolderByTeamIDAndChannelID(accessToken, team.id, channel.id);
                        var items = await TeamsGraphAPIHandler.GetItemsByDriveIDAndItemID(accessToken, filesFolders.parentReference.driveId, filesFolders.id);

                        var channelPath = $"{filesPath}/{filesFolders.name}";
                        Directory.CreateDirectory(channelPath);

                        foreach (var item in items)
                        {
                            if(item.folder != null)
                            {
                                await SyncItemsInFolder.SyncItemsInFolderAsync(channelPath, item, accessToken);
                            }
                            else
                            {
                                await DownloadFile.DownloadFileAsync(channelPath, item.downloadUrl, item.name);
                            }
                        }
                    }
                }

                Console.WriteLine("Job Done");

                return teams;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
