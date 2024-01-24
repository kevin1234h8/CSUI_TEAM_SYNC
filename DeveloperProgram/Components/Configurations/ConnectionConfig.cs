namespace CSUI_Teams_Sync.Components.Configurations
{
    public class ConnectionConfig
    {
        //public const string Connection = "Connection";
        public string Destination_URL { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public string DB_Username { get; set; }
        public string DB_Password { get; set; }
        public bool UseSecureCredentials { get; set; }
        public Secure SecureInfo { get; set; }
        public string DB_ConnectionString_WOCredentials { get; set; }
        public bool IsDBOracle { get; set; }
        public bool UseDBIntegratedSecurity { get; set; }

        public Email Email { get; set; }
    }

    public class Secure
    {
        public string Path { get; set; }
        public FileName FileName { get; set; }
        public Secured Secured { get; set; }
    }


    public class FileName
    {
        public string Master_Name { get; set; }
    }

    public class Secured
    {
        public string CSUsername { get; set; }
        public string CSPassword { get; set; }
        public string CSDBUsername { get; set; }
        public string CSDBPassword { get; set; }


    }

    public class Email
    {
        public string Host { get; set; }
        public string From { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
    }
}
