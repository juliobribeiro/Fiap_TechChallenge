using FIAP._6NETT_GRUPO31.Infra.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Prometheus;
using MassTransit;
using FIAP._6NETT_GRUPO31.Application.Consumers;
using Contact.Core.Dto;


namespace FIAP._6NETT_GRUPO31.API.Configuration
{
    public static class ApiConfig
    {

        public static void AddApConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FIAPContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


            services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
            });
            services.UseHttpClientMetrics();

            var rabbitMqConfig = configuration.GetSection("RabbitMq").Get<RabbitMqConnection>();

            services.AddMassTransit(x =>
            {
                x.AddConsumer<AddContactConsumer>();
                x.AddConsumer<UpdateContactConsumer>();
                x.AddConsumer<DeleteContactConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    //cfg.Host(new Uri("amqp://localhost:5672"), h => {
                    cfg.Host(new Uri($"amqp://{rabbitMqConfig.HostName}:{rabbitMqConfig.Port}"), h =>
                    {
                        h.Username(rabbitMqConfig.UserName);
                        h.Password(rabbitMqConfig.Password);
                    });                  

                    cfg.ConfigureEndpoints(context);                 
                });
            });


        }

        public static void UseApiConfiguration(this WebApplication app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseMetricServer();
            app.UseHttpMetrics();
            // app.UseHttpsRedirection();            
            app.MapControllers();
        }
    }
}
