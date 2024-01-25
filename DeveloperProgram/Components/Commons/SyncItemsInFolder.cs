using CSUI_Teams_Sync.Library;
using CSUI_Teams_Sync.Models;

namespace CSUI_Teams_Sync.Components.Commons
{
    public class SyncItemsInFolder
    {
        public static async Task SyncItemsInFolderAsync(string path, Item item, string accessToken)
        {
            using HttpClient httpClient = new();
            try
            {
                var folderPath = $"{path}/{item.name}";
                Directory.CreateDirectory(folderPath);

                var items = await TeamsGraphAPIHandler.GetItemsByDriveIDAndItemID(accessToken, item!.parentReference!.driveId, item.id);

                foreach (var file in items)
                {
                    if (file.folder != null)
                    {
                        await SyncItemsInFolderAsync(folderPath, file, accessToken);
                    }
                    else
                    {
                        await DownloadFile.DownloadFileAsync(folderPath, file.downloadUrl, file.name);
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
