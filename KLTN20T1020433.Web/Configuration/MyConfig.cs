

using KLTN20T1020433.Application.Configuration;

namespace KLTN20T1020433.Web.Configuration
{

    public static class MyConfig
    {
        public static IServiceCollection AddConfig(
             this IServiceCollection services, IConfiguration config)
        {
            services.Configure<FileConfig>(
                config.GetSection(FileConfig.FILE_CONFIG)
                );
            services.Configure<ApiConfig>(
                config.GetSection(ApiConfig.API_CONFIG)
                );
            return services;
        }
    }
}
