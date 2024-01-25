namespace CSUI_Teams_Sync.Components.Commons
{
    public class DownloadFile
    {
        public static async Task DownloadFileAsync(string path, string url, string fileName)
        {
            using HttpClient httpClient = new HttpClient();
            try
            {
                byte[] fileContent = await httpClient.GetByteArrayAsync(url);

                string filePath = $"{path}/{fileName}";
                File.WriteAllBytes(filePath, fileContent);

                Console.WriteLine($"File '{fileName}' has been saved to '{filePath}'.");
                Console.WriteLine("");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error downloading the file: {ex.Message}");
            }
        }
    }
}
