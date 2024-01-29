using Newtonsoft.Json;
using System.Xml.Linq;

namespace CSUI_Teams_Sync.Models
{
    public class DeltaItems
    {
        [JsonProperty("@odata.deltaLink")]
        public string DeltaLink { get; set; }
        
        [JsonProperty("value")]
        public List<DeltaItem> Value { get; set; }
    }
    public class DeltaItem
    {
        [JsonProperty("id")]
        public string ID { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("deleted")]
        public ItemDeleted Deleted { get; set; }
        
        [JsonProperty("size")]
        public long Size { get; set; }
        
        [JsonProperty("parentReference")]
        public DeltaItemParentReference ParentReference { get; set; }
    }
    public class DeltaItemParentReference
    {
        [JsonProperty("id")]
        public string ID { get; set; }
        
        [JsonProperty("driveId")]
        public string DriveId { get; set; }
    }
    public class ItemDeleted
    {
        [JsonProperty("state")]
        public string? State { get; set; }
    }
}
