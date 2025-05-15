using Contact.Core.Dto;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;


namespace Contact.Core.ServiceBus
{
    public static class  RabbitMqConfiguration
    {
        public static void AddRabitMqConfiguration(this IServiceCollection services,IConfiguration configuration)
        {

            var rabbitMqConfig = configuration.GetSection("RabbitMq").Get<RabbitMqConnection>();

            services.AddMassTransit(x =>
            {                
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri($"amqp://{rabbitMqConfig.HostName}:{rabbitMqConfig.Port}"), h => {
                        h.Username(rabbitMqConfig.UserName);
                        h.Password(rabbitMqConfig.Password);
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });            

        }              
    }
}
