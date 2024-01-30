namespace CSUI_Teams_Sync.Components.Commons
{
    public class TimeUtils
    {
        public static string CreateUTCByIncrementDays(int day = 1)
        {
            DateTime currentUtcTime = DateTime.UtcNow;
            DateTime newUtcTime = currentUtcTime.AddDays(day);
            string isoTimestamp = newUtcTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            return isoTimestamp;
        }

        public static string FormatTeamsDate(string dateString)
        {
            DateTime parsedDate;

            if (DateTime.TryParse(dateString, out parsedDate))
            {
                var options = new System.Globalization.DateTimeFormatInfo();
                return parsedDate.ToString("MMMM d, yyyy HH:mm:ss", options);
            }
            else
            {
                return "Invalid date format";
            }
        }
    }
}
