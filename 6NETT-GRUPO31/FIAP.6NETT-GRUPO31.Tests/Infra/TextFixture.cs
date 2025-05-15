using FIAP._6NETT_GRUPO31.Infra.Data.Context;
using FIAP._6NETT_GRUPO31.API.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP._6NETT_GRUPO31.Tests.Infra
{    
    public class TextFixture
    {
        public IServiceCollection _servicesCollection { get; set; }
        public TextFixture()
        {
            var services = new ServiceCollection();
            services.CreateSQLLite();
            services.RegisterServices();
            _servicesCollection = services;
        }


        public void CreateContext(FIAPContext context)
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

    [CollectionDefinition("Test collection")]
    public class TestCollection : ICollectionFixture<TextFixture>
    {

    }
}
