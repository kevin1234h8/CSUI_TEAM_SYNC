using Newtonsoft.Json;
using System.Xml.Linq;

namespace CSUI_Teams_Sync.Models
{
    public class DriveDelta
    {
        [JsonProperty("@odata.deltaLink")]
        public string DeltaLink { get; set; }
    }
}
