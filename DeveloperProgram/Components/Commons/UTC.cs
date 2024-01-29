namespace CSUI_Teams_Sync.Components.Commons
{
    public class UTC
    {
        public static string CreateUTCByIncrementDays(int day = 1)
        {
            DateTime currentUtcTime = DateTime.UtcNow;
            DateTime newUtcTime = currentUtcTime.AddDays(day);
            string isoTimestamp = newUtcTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            return isoTimestamp;
        }
    }
}
