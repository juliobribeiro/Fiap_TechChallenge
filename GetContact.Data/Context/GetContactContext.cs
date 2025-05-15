using GetContact.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace GetContact.Data.Context
{
    public class GetContactContext : DbContext
    {
        public DbSet<Contatos> Contatos { get; set; }
        public GetContactContext(DbContextOptions<GetContactContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
               e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");


            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GetContactContext).Assembly);
        }

    }
}
