namespace CSUI_Teams_Sync.Models
{
    public class TeamsUserChat
    {
        public string ODataContext { get; set; }
        public int ODataCount { get; set; }
        public List<Chat> Value { get; set; }
    }
    public class Chat
    {
        public string Id { get; set; }
        public string Topic { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime LastUpdatedDateTime { get; set; }
        public string ChatType { get; set; }
        public string WebUrl { get; set; }
        public string TenantId { get; set; }
        public string Viewpoint { get; set; }
        public object OnlineMeetingInfo { get; set; }
    }
}
