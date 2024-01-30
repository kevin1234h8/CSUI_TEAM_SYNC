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
        public string? subject { get; set; }
        public string? createdDateTime { get; set; }
        public PostEventDetail? eventDetail { get; set; }
        public PostChannelIdentity channelIdentity { get; set; }
        public PostFrom from { get; set; }
        public PostBody body { get; set; }
    }

    public class PostChannelIdentity
    {
        public string channelId { get; set; }
        public string teamId { get; set; }
    }
    public class PostFrom
    {
        public PostFromUser user { get; set; }
    }
    public class PostBody
    {
        public string content { get; set; }
    }
    public class PostFromUser
    {
        public string id { get; set; }
        public string displayName { get; set; }
    }
    public class PostEventDetail
    {
        public string? callEventType { get; set; }
    }
}
