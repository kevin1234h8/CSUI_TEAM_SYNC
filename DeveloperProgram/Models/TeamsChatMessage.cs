namespace CSUI_Teams_Sync.Models
{
    public class TeamsChatMessage
    {
        public string ODataContext { get; set; }
        public int ODataCount { get; set; }
        public List<Message> Value { get; set; }
    }

    public class Message
    {
        public string Id { get; set; }
        public string ReplyToId { get; set; }
        public string ETag { get; set; }
        public string MessageType { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime LastModifiedDateTime { get; set; }
        public DateTime? LastEditedDateTime { get; set; }
        public DateTime? DeletedDateTime { get; set; }
        public string Subject { get; set; }
        public string Summary { get; set; }
        public string ChatId { get; set; }
        public string Importance { get; set; }
        public string Locale { get; set; }
        public string WebUrl { get; set; }
        public ChannelIdentity ChannelIdentity { get; set; }
        public PolicyViolation PolicyViolation { get; set; }
        public EventDetail EventDetail { get; set; }
        public From From { get; set; }
        public Body Body { get; set; }
        public List<Attachment> Attachments { get; set; }
        public List<Mention> Mentions { get; set; }
        public List<Reaction> Reactions { get; set; }
    }

    public class ChannelIdentity
    {
    }

    public class PolicyViolation
    {
    }

    public class EventDetail
    {
        public string ODataType { get; set; }
        public DateTime VisibleHistoryStartDateTime { get; set; }
        public List<Member> Members { get; set; }
        public Initiator Initiator { get; set; }
    }

    public class Initiator
    {
        public Application Application { get; set; }
        public Device Device { get; set; }
        public User User { get; set; }
    }

    public class Application
    {
    }

    public class Device
    {
    }

    /*  public class User
      {
          public string ODataType { get; set; }
          public string Id { get; set; }
          public string DisplayName { get; set; }
          public string UserIdentityType { get; set; }
          public string TenantId { get; set; }
      }
  */
    public class Member
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string UserIdentityType { get; set; }
        public string TenantId { get; set; }
    }

    public class From
    {
        public Application Application { get; set; }
        public Device Device { get; set; }
        public User User { get; set; }
    }

    public class Body
    {
        public string ContentType { get; set; }
        public string Content { get; set; }
    }

    public class Attachment
    {
    }

    public class Mention
    {
    }

    public class Reaction
    {
    }
}
