using KLTN20T1020433.Infrastructure.Configuration;

namespace KLTN20T1020433.Web.Configuration
{
    
    public static class MyConfig
    {
        public static IServiceCollection AddConfig(
             this IServiceCollection services, IConfiguration config)
        {
            services.Configure<FileConfig>(
                config.GetSection(FileConfig.FILE_CONFIG));
            return services;
        }
    }
}
