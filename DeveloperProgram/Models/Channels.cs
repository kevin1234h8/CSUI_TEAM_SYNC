using System.Xml.Linq;

namespace CSUI_Teams_Sync.Models
{
    public class Channels
    {
        public string ODataContext { get; set; }
        public int ODataCount { get; set; }
        public List<Channel> Value { get; set; }
    }
    public class Channel
    {
        public string id { get; set; }
        public string createdDateTime { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
    }
}
