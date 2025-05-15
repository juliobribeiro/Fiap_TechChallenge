using FIAP._6NETT_GRUPO31.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP._6NETT_GRUPO31.Infra.Data.Mapping
{
    public class ContatoMapping : IEntityTypeConfiguration<Contatos>
    {
        public void Configure(EntityTypeBuilder<Contatos> builder)
        {
            builder.HasKey(x => x.IdContato);

            builder.HasIndex(X => X.Email).IsUnique();

            builder.Property(x => x.Nome).HasColumnType("varchar(500)");         

            builder.ToTable("TAB_CONTATOS");
        }
    }
}
