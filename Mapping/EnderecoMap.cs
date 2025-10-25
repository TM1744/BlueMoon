
using BlueMoon.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlueMoon.Mapping
{
    public sealed class EnderecoMap : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id").HasMaxLength(36);
            builder.Property(x => x.CEP).HasColumnName("cep").HasMaxLength(8).IsRequired();
            builder.Property(x => x.Logradouro).HasColumnName("logradouro").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Numero).HasColumnName("numero").HasMaxLength(10).IsRequired();
            builder.Property(x => x.Complemento).HasColumnName("complemento").HasMaxLength(40);
            builder.Property(x => x.Bairro).HasColumnName("bairro").HasMaxLength(70).IsRequired();
            builder.Property(x => x.Cidade).HasColumnName("cidade").HasMaxLength(50).IsRequired();
            builder.Property(x => x.Estado).HasColumnName("estado").HasColumnType("int").IsRequired();
        }
    }
}