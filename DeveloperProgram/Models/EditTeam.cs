using Newtonsoft.Json;

namespace CSUI_Teams_Sync.Models
{
    public class EditTeam
    {
        [JsonProperty("id")]
        public string ID { get; set; }
        
        [JsonProperty("channel")]
        public EditTeamChannel Channel { get; set; }
    }
    public class EditTeamChannel
    {
        [JsonProperty("id")]
        public string ID { get; set; }
        
        [JsonProperty("items")]
        public List<int> Items { get; set; }
    }
}
