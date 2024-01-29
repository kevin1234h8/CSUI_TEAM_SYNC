namespace CSUI_Teams_Sync.Models
{
    public class TeamsJoinChannel
    {
        public string id { get; set; }
        public string name { get; set; }
        public TeamsJoinChannelChannel channel { get; set; }
    }
    public class TeamsJoinChannelChannel
    {
        public string id { get; set; }
        public string name { get; set; }
        public List<TeamsJoinChannelChannelItems> items { get; set; }
    }
    public class TeamsJoinChannelChannelItems
    {
        public string value { get; set; }
        public string label { get; set; }
    }
}
