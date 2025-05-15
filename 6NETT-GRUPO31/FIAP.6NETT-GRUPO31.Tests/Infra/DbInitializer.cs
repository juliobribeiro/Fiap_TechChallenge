using FIAP._6NETT_GRUPO31.Domain.Entities;
using FIAP._6NETT_GRUPO31.Infra.Data.Context;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace FIAP._6NETT_GRUPO31.Tests.Infra
{
    public static class DbInitializer
    {
        public static void InitializerSqlite(FIAPContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            Seed(context);
        }

        private static void Seed(FIAPContext context)
        {
            var contatosJson = GetJson("contatos.json");

            List<Contatos> contatos = JsonConvert.DeserializeObject<List<Contatos>>(contatosJson);
            context.Contatos.AddRange(contatos);


            context.SaveChanges();
        }

        private static string GetJson(string fileName)
        {
            return File.ReadAllText($"./Seeds/{fileName}");
        }
    }
}
