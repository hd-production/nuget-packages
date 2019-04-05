using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HdProduction.App.Common
{
    public static class ApplicationExtensions
    {
        public static IApplicationBuilder UseCors(this IApplicationBuilder app, IConfigurationSection corsConfig)
        {
            return app.UseCors(pb => InitCorsOptions(corsConfig, pb));
        }

        private static void InitCorsOptions(IConfigurationSection corsConfig, CorsPolicyBuilder policyBuilder)
        {
            string origins = corsConfig.GetValue<string>("origins");
            string methods = corsConfig.GetValue<string>("methods");
            string headers = corsConfig.GetValue<string>("headers");
            if (origins == CorsConstants.AnyOrigin)
                policyBuilder.AllowAnyOrigin();
            else
                policyBuilder.WithOrigins(origins.Split(','));
            if (methods == "*")
                policyBuilder.AllowAnyMethod();
            else
                policyBuilder.WithMethods(methods.Split(','));
            if (headers == "*")
                policyBuilder.AllowAnyHeader();
            else
                policyBuilder.WithHeaders(headers.Split(','));
        }
        
        public static T ResolveService<T>(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                return serviceScope.ServiceProvider.GetService<T>();
            }
        }
    }
}