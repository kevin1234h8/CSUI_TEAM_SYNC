using CSUI_Teams_Sync.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Runtime.CompilerServices;

namespace CSUI_Teams_Sync.Services
{
    public class OTCSService
    {
        private static readonly string username = "Admin";
        private static readonly string password = "P@ssw0rd";
        public async Task<string> GetTicket()
        {
            OTCSLogin accessToken = new();

            try
            {
                var options = new RestClientOptions("http://localhost")
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest("/otcs/cs.exe/api/v1/auth", Method.Post);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("username", username);
                request.AddParameter("password", password);
                RestResponse response = await client.ExecuteAsync<OTCSLogin>(request);
                accessToken = JsonConvert.DeserializeObject<OTCSLogin>(response.Content);

                return accessToken.ticket;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<OTCSCreateNodeResponse> CreateFolder(string ticket, OTCSCreateNode body)
        {
            try
            {
                var options = new RestClientOptions("http://localhost")
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest("/otcs/cs.exe/api/v2/nodes", Method.Post);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddHeader("otcsticket", ticket);
                request.AddParameter("body", JsonConvert.SerializeObject(body));
                RestResponse response = await client.ExecuteAsync<OTCSCreateNodeResponse>(request);

                return JsonConvert.DeserializeObject<OTCSCreateNodeResponse>(response.Content);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw ex;
            }
        }
        public async Task DeleteItem(string ticket, string nodeID)
        {
            try
            {
                var options = new RestClientOptions("http://localhost")
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest($"/otcs/cs.exe/api/v2/nodes/{nodeID}", Method.Delete);
                request.AddHeader("otcsticket", ticket);
                await client.ExecuteAsync(request);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw ex;
            }
        }
    }
}
