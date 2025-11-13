using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueMoon.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlueMoon.Mapping
{
    public class VendaMap : IEntityTypeConfiguration<Venda>
    {
        public void Configure(EntityTypeBuilder<Venda> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id").HasMaxLength(36);
            builder.Property(x => x.Codigo).HasColumnName("codigo");
            builder.Property(x => x.Situacao).HasColumnName("situacao");
            builder.Property(x => x.ValorTotal).HasColumnName("valor_total").HasColumnType("decimal(11,2)");
            builder.Property(x => x.DataAbertura).HasColumnName("data_abertura");
            builder.Property(x => x.DataFaturamento).HasColumnName("data_faturamento");
            builder.Property<Guid>("id_pessoa").HasColumnName("id_pessoa").IsRequired();
            builder.Property<Guid>("id_usuario").HasColumnName("id_usuario").IsRequired();

            builder
                .HasOne(x => x.Cliente)
                .WithMany()
                .HasForeignKey("id_pessoa")
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder
                .HasOne(x => x.Vendedor)
                .WithMany()
                .HasForeignKey("id_usuario")
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder
                .HasMany(x => x.Itens)
                .WithOne()
                .HasForeignKey("id_venda")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}