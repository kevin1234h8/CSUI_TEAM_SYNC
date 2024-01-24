namespace CSUI_Teams_Sync
{
    public class AppSettings
    {
        public static IConfiguration Build()
        {
            return new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }
    }
}
