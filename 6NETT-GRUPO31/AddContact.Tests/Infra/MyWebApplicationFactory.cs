using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AddContact.Tests.Infra
{
    public class MyWebApplicationFactory<T,TContext> : IClassFixture<WebApplicationFactory<T>> where T : class where TContext: DbContext
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
