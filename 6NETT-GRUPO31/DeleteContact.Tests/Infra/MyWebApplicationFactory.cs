using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeleteContact.Tests.Infra
{
    public class MyWebApplicationFactory<T, TContext> : IClassFixture<WebApplicationFactory<T>> where T : class where TContext : DbContext
    {
        protected readonly WebApplicationFactory<T> Factory;

        public MyWebApplicationFactory(WebApplicationFactory<T> factory)
        {
            Factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {

                });
            });
        }
    }
}
