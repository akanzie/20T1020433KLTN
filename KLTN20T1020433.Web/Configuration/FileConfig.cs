namespace KLTN20T1020433.Web.Configuration
{
    public static class FileConfig
    {
        public const string FILE_STORAGE_PATHS = "FileStoragePaths";
        public static string ServerStoragePath { get; private set; } = "";
        public static void Initialize(string path)
        {
            FileConfig.ServerStoragePath = path;
        }
    }
}
