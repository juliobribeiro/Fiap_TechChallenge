using FIAP._6NETT_GRUPO31.API.Configuration;
using FIAP._6NETT_GRUPO31.Infra.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP._6NETT_GRUPO31.Tests.Infra
{
    public class IntegrationTeste : IClassFixture<WebApplicationFactory<Program>>
    {
        protected readonly HttpClient Client;
        protected readonly WebApplicationFactory<Program> Factory;
        protected IServiceCollection _servicesCollection { get; set; }

        public IntegrationTeste(WebApplicationFactory<Program> factory)
        {
            Factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<FIAPContext>));                 
                    if (descriptor != null)
                        services.Remove(descriptor);

                    services.CreateSQLLite();

                    _servicesCollection = services;

                });
            });



            Client = Factory.CreateClient();
        }

        protected void CreateContext(FIAPContext context)
        {
            try
            {
                DbInitializer.InitializerSqlite(context);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
