using BlueMoon.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace BlueMoon.Mapping
{
    public sealed class PessoaMap : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id").HasMaxLength(36);
            builder.Property(x => x.Tipo).HasColumnName("tipo").IsRequired();
            builder.Property(x => x.Situacao).HasColumnName("situacao").IsRequired();
            builder.Property(x => x.Codigo).HasColumnName("codigo");
            builder.Property(x => x.Telefone).HasColumnName("telefone").HasMaxLength(11).IsRequired();
            builder.Property(x => x.Email).HasColumnName("email").HasMaxLength(100);
            builder.Property(x => x.Nome).HasColumnName("nome").IsRequired().HasMaxLength(100);
            builder.Property(x => x.Documento).HasColumnName("documento").HasMaxLength(14);
            builder.Property(x => x.InscricaoMunicipal).HasColumnName("inscricao_municipal").HasMaxLength(12);
            builder.Property(x => x.InscricaoEstadual).HasColumnName("inscricao_estadual").HasMaxLength(13);
            builder.Property(x => x.CEP).HasColumnName("cep").HasColumnType("char(8)");
            builder.Property(x => x.Logradouro).HasColumnName("logradouro").HasMaxLength(100);
            builder.Property(x => x.Numero).HasColumnName("numero").HasMaxLength(10);
            builder.Property(x => x.Complemento).HasColumnName("complemento").HasMaxLength(40);
            builder.Property(x => x.Bairro).HasColumnName("bairro").HasMaxLength(70);
            builder.Property(x => x.Cidade).HasColumnName("cidade").HasMaxLength(50);
            builder.Property(x => x.Estado).HasColumnName("estado").HasColumnType("int");
        }
    }
}