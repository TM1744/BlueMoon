using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueMoon.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlueMoon.Mapping
{
    public class TelefoneMap : IEntityTypeConfiguration<Telefone> 
    {
        public void Configure(EntityTypeBuilder<Telefone> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id").HasMaxLength(36);
            builder.Property(x => x.DDD).HasColumnName("ddd").HasColumnType("int").IsRequired();
            builder.Property(x => x.Numero).HasColumnName("numero").HasMaxLength(9).IsRequired();
        }
    }
}