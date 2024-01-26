namespace CSUI_Teams_Sync.Models
{
    public class OTCSCreateNode
    {
        public long parent_id { get; set; }
        public string name { get; set; }
        public int type { get; set; }
    }
    public class OTCSCreateNodeResponse
    {
        public OTCSCreateNodeResponseResult results { get; set; }
    }
    public class OTCSCreateNodeResponseResult
    {
        public OTCSCreateNodeResponseData data { get; set; }
    }
    public class OTCSCreateNodeResponseData
    {
        public OTCSCreateNodeResponseProperties properties { get; set; }
    }
    public class OTCSCreateNodeResponseProperties
    {
        public long id { get; set; }
    }
}
