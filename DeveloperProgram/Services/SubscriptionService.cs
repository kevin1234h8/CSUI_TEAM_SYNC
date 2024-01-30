using Azure.Core;
using CSUI_Teams_Sync.Components.Commons;
using CSUI_Teams_Sync.Models;
using Newtonsoft.Json;
using RestSharp;

namespace CSUI_Teams_Sync.Services
{
    public class SubscriptionService
    {
        private readonly string baseUrl = "https://3705-151-192-199-9.ngrok-free.app";
        private readonly string clientState = "super-secret-text";

        public async Task CreateDriveSubscription(string jwt)
        {
            var options = new RestClientOptions("https://graph.microsoft.com")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/v1.0/subscriptions", Method.Post);
                request.AddHeader("Content-Type", "application/json; charset=utf-8");
                request.AddHeader("Authorization", $"bearer {jwt}");
            var jsonPayload = new
            {
                changeType = "updated",
                clientState,
                lifecycleNotificationUrl = $"{baseUrl}/api/v1/subscription",
                notificationUrl = $"{baseUrl}/api/v1/subscription",
                resource = "drives/b!z0OTo9KIVUybZ4Xnw6nahGerlNEirJNMrxmczUFvWKqg2I5z7GdLQJSDBsLbk8At/root",
                expirationDateTime = TimeUtils.CreateUTCByIncrementDays(2),
            };

            request.AddJsonBody(jsonPayload);
            RestResponse response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content);
        }
        public async Task<List<Subscription>> GetSubscriptions(string jwt)
        {
            var options = new RestClientOptions("https://graph.microsoft.com")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/v1.0/subscriptions", Method.Get);
            request.AddHeader("Authorization", $"bearer {jwt}");

            RestResponse response = await client.ExecuteAsync(request);
            var result = JsonConvert.DeserializeObject<Subscriptions>(response.Content);
            return result.Value;
        }
        public async Task DeleteSubscription(string jwt, string id)
        {
            var options = new RestClientOptions("https://graph.microsoft.com")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest($"/v1.0/subscriptions/{id}", Method.Delete);
            request.AddHeader("Authorization", $"bearer {jwt}");

            await client.ExecuteAsync(request);
        }
        public async Task DeleteAllSubscriptions(string jwt)
        {
            var subs = await GetSubscriptions(jwt);

            foreach (var sub in subs)
            {
                await DeleteSubscription(jwt, sub.ID);
            }
        }
    }
}
