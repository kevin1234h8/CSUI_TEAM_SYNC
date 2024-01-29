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
        public static async Task<OTCSCreateNodeResponseProperties> DownloadFileAsync(string url, long parentID, string name, string ticket)
        {
            using HttpClient httpClient = new();
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
                RestResponse response = await client.ExecuteAsync<OTCSCreateNodeResponseProperties>(request);
                return JsonConvert.DeserializeObject<OTCSCreateNodeResponseProperties>(response.Content);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error downloading the file: {ex.Message}");
                return null;
            }
        }
    }
}
