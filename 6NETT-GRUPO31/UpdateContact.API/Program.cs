using UpdateContact.Application.Interfaces;
using UpdateContact.Application.Services;
using Contact.Core.ServiceBus;
using Contact.WebApi.Core.Util;
using Contact.Core.Dto;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.UseHttpClientMetrics();

builder.Services.AddHttpClient<IUpdateContactApplication, UpdateContactApplication>(
    options => options.BaseAddress = new Uri(builder.Configuration.GetValue<string>("UrlGetContact")))
    .AddPolicyHandler(RetryExtensions.CreatePolicy(10));

builder.Services.Configure<RabbitMqConnection>(builder.Configuration.GetSection("RabbitMq"));
builder.Services.AddRabitMqConfiguration(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseMetricServer();
app.UseHttpMetrics();

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }