namespace CSUI_Teams_Sync.Components.Configurations
{
    public class TeamsGraphAPIConfig
    {
        public Url Url { get; set; }
        public string TenantId { get; set; }
    }

    public class Url
    {
        public string GetAccessTokenApiUrl { get; set; }
    }
}
