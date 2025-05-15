using FIAP._6NETT_GRUPO31.Infra.Data.Context;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP._6NETT_GRUPO31.Tests.Infra
{
    public static class CreateDB
    {
        public static void CreateSQLLite(this IServiceCollection services)
        {
            var connectionString = "Data source=TesteContext;Mode=Memory;Cache=Shared";
            var connection = new SqliteConnection(connectionString);

            connection.Open();

            services.AddDbContext<FIAPContext>(options =>
            options.UseSqlite(connection));

        }
    }
}
