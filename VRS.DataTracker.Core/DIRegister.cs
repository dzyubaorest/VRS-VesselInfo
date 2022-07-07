using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VRS.DataTracker.Abstractions;
using VRS.DataTracker.Abstractions.Dto.Settings;

namespace VRS.DataTracker.Core
{
    public static class DIRegister
    {
        public static IServiceCollection RegisterCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ITransmissionConsumer, TransmissionConsumer>();
            services.AddScoped<ITransmissionDataMessagePublisher, TransmissionDataMessagePublisher>();
            AddRabbitMQSettings(services, configuration);

            return services;
        }

        private static void AddRabbitMQSettings(IServiceCollection services, IConfiguration configuration)
        {
            var settings = new RabbitMQSettings();
            configuration.Bind(nameof(RabbitMQSettings), settings);
            services.AddSingleton(settings);
        }
    }
}