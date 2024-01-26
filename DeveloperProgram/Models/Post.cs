namespace CSUI_Teams_Sync.Models
{
    public class Posts
    {
        public List<Post> value { get; set; }
    }
    public class Post
    {
        public string id { get; set; }
        public string? replyToId { get; set; }
        public PostEventDetail? eventDetail { get; set; }
        public PostChannelIdentity channelIdentity { get; set; }
    }

    public class PostChannelIdentity
    {
        public string channelId { get; set; }
        public string teamId { get; set; }
    }
    public class PostEventDetail
    {
        public string? callEventType { get; set; }
    }
}
