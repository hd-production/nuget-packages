using System;
using System.Linq;
using System.Reflection;
using HdProduction.MessageQueue.RabbitMq;
using HdProduction.MessageQueue.RabbitMq.Stubs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HdProduction.App.Common
{
    public static class ConfigurationExtensions
    {
        public static IMvcCoreBuilder AddWebApi(this IServiceCollection services)
        {
            return services.AddMvcCore().AddAuthorization().AddFormatterMappings().AddJsonFormatters().AddCors().AddApiExplorer();
        }

        public static void AddMessageQueue<THandler>(this IServiceCollection services, 
            IConfigurationSection mqConfigurationSection)
        {
            if (mqConfigurationSection.GetValue<bool>("Enabled"))
            {
                services.AddSingleton<IRabbitMqConnection>(
                    new RabbitMqConnection(mqConfigurationSection.GetValue<string>("Uri"), "hd_production"));
                services.AddSingleton<IRabbitMqPublisher, RabbitMqPublisher>();
                services.AddSingleton<IRabbitMqConsumer, RabbitMqConsumer>(c => new RabbitMqConsumer(
                    mqConfigurationSection.GetValue<string>("ConsumerQueue"), c.GetService<IServiceProvider>(), c.GetService<IRabbitMqConnection>()));
            }
            else
            {
                services.AddSingleton<IRabbitMqPublisher, FakeMqPublisher>();
                services.AddSingleton<IRabbitMqConsumer, FakeMqConsumer>();
            }

            foreach (var eventHandler in typeof(THandler).GetTypeInfo().Assembly.GetTypes()
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition().IsAssignableFrom(typeof(IMessageHandler<>)))
                            && !t.IsInterface))
            {
                foreach (var @interface in eventHandler.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition().IsAssignableFrom(typeof(IMessageHandler<>))))
                {
                    services.AddTransient(@interface, eventHandler);
                }
            }
        }
    }
}