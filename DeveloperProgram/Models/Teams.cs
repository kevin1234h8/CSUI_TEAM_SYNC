using System.Xml.Linq;

namespace CSUI_Teams_Sync.Models
{
    public class Teams
    {
        public string ODataContext { get; set; }
        public int ODataCount { get; set; }
        public List<Team> Value { get; set; }
    }
    public class Team
    {
        public string id { get; set; }
        public string createdDateTime { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
    }
}
