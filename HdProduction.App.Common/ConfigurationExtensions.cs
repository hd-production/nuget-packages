using Microsoft.Extensions.DependencyInjection;

namespace HdProduction.App.Common
{
    public static class ConfigurationExtensions
    {
        public static IMvcCoreBuilder AddWebApi(this IServiceCollection services)
        {
            return services.AddMvcCore()
                .AddAuthorization()
                .AddFormatterMappings()
                .AddJsonFormatters()
                .AddCors()
                .AddApiExplorer();
        }
    }
}