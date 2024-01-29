using Newtonsoft.Json;

namespace CSUI_Teams_Sync.Models
{
    public class Subscriptions
    {
        [JsonProperty("value")]
        public List<Subscription> Value { get; set; }
    }
    public class Subscription
    {
        [JsonProperty("changeType")]
        public string ChangeType { get; set; }

        [JsonProperty("notificationUrl")]
        public string NotificationUrl { get; set; } 
        
        [JsonProperty("expirationDateTime")]
        public string ExpirationDateTime { get; set; }
        
        [JsonProperty("resource")]
        public string Resource { get; set; }

        [JsonProperty("id")]
        public string ID { get; set; }
    }
}
