using CSUI_Teams_Sync.Library;
using CSUI_Teams_Sync.Models;
using CSUI_Teams_Sync.Services;
using System.Threading.Channels;

namespace CSUI_Teams_Sync.Components.Commons
{
    public class SyncItemsInFolder
    {
        private readonly static OTCSService _otcsService = new();

        public static async Task SyncItemsInFolderAsync(long nodeID, Item item, string accessToken, string ticket)
        {
            using HttpClient httpClient = new();
            try
            {
                var bodyChannel = new OTCSCreateNode()
                {
                    parent_id = nodeID,
                    name = item.name,
                    type = 0
                };

                var teamChannel = await _otcsService.CreateFolder(ticket, bodyChannel);

                var items = await TeamsGraphAPIHandler.GetItemsByDriveIDAndItemID(accessToken, item!.parentReference!.driveId, item.id);

                foreach (var file in items)
                {
                    if (file.folder != null)
                    {
                        await SyncItemsInFolderAsync(teamChannel.results.data.properties.id, file, accessToken, ticket);
                    }
                    else
                    {
                        await DownloadFile.DownloadFileAsync(file.downloadUrl, teamChannel.results.data.properties.id, file.name, ticket);
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error downloading the file: {ex.Message}");
            }
        }
    }
}
