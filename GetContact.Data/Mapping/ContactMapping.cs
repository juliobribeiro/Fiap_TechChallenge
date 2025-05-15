using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GetContact.Domain.Entities;

namespace GetContact.Data.Mapping
{  
    public class ContactMapping : IEntityTypeConfiguration<Contatos>
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
