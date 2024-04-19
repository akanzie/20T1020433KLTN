namespace KLTN20T1020433.Web.Configuration
{
    public static class DatabaseConfig
    {
        public const string CONNECTION_STRINGS = "ConnectionStrings";
        public static string SQLServerConnectionString { get; private set; } = "";
        public static void Initialize(string connectionString)
        {
            SQLServerConnectionString = connectionString;
        }
    }
}
