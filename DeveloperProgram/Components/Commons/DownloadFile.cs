using CSUI_Teams_Sync.Components.Configurations;
using CSUI_Teams_Sync.Models;
using CSUI_Teams_Sync.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.IO;
using System.Xml.Linq;

namespace CSUI_Teams_Sync.Components.Commons
{
    public class DownloadFile
    {
        private readonly static OTCSService _otcsService = new();
        public static async Task DownloadFileAsync(string url, long parentID, string name, string ticket)
        {
            using HttpClient httpClient = new HttpClient();
            try
            {
                byte[] fileContent = await httpClient.GetByteArrayAsync(url);
                string filePath = $"C:/work/temp_upload/{name}";
                File.WriteAllBytes(filePath, fileContent);

                var client = new RestClient("http://localhost");
                var request = new RestRequest("/otcs/cs.exe/api/v1/nodes", Method.Post);

                request.AddHeader("otcsticket", ticket);
                request.AddParameter("type", 144);
                request.AddParameter("parent_id", parentID);
                request.AddParameter("name", name);
                request.AddFile("file", filePath);

                var a = await client.ExecuteAsync(request);

                Console.WriteLine(a.Content);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error downloading the file: {ex.Message}");
            }
        }
    }
}
