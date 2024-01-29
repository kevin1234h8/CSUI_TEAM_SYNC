namespace CSUI_Teams_Sync.Models
{
    public class APIResponse<T>
    {
        public string Message { get; set; } = "SUCCESS";
        public T Data {  get; set; }
    }
}
