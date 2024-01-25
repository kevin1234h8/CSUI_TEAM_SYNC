using Newtonsoft.Json;
using System.Xml.Linq;

namespace CSUI_Teams_Sync.Models
{
    public class Items
    {
        public List<Item> Value { get; set; }
    }
    public class Item
    {
        public string id { get; set; }
        public string name { get; set; }
        public ItemFolder? folder { get; set; }
        public ItemParentReference? parentReference { get; set; }

        [JsonProperty("@microsoft.graph.downloadUrl")]
        public string downloadUrl { get; set; }
    }
    public class ItemFolder
    {
        public int childCount { get; set; }
    }
    public class ItemParentReference
    {
        public string driveId { get; set; }
        public string driveType { get; set; }
    }
}
