using AutoMapper;
using KLTN20T1020433.Web.Mappings;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace KLTN20T1020433.Web.Configuration
{
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            var configurationExpression = new MapperConfigurationExpression();
            AutoMapperConfig.RegisterMappings().ForEach(p => configurationExpression.AddProfile(p));
            var automapperConfig = new MapperConfiguration(configurationExpression);
            services.TryAddSingleton(automapperConfig.CreateMapper());
        }
    }
}
}
