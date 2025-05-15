using GetContact.Application.Interfaces;
using GetContact.Application.Services;
using GetContact.Data.Context;
using GetContact.Data.Repository;
using GetContact.Domain.Interfaces;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);

builder.Services.AddDbContext<GetContactContext>(options =>
              options.UseSqlServer(Environment.GetEnvironmentVariable("connectionString")));

#region Context
builder.Services.AddScoped<GetContactContext>();
#endregion

#region Repository
//repository
builder.Services.AddScoped<IGetContactRepository, GetContactRepository>();
#endregion


#region Application
builder.Services.AddScoped<IGetContactApplication, GetContactApplication>();
#endregion

builder.ConfigureFunctionsWebApplication();

// Application Insights isn't enabled by default. See https://aka.ms/AAt8mw4.
// builder.Services
//     .AddApplicationInsightsTelemetryWorkerService()
//     .ConfigureFunctionsApplicationInsights();

builder.Build().Run();
