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

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Tipo).HasColumnName("tipo").IsRequired();
            builder.Property(x => x.Situacao).HasColumnName("situacao").IsRequired();
            builder.Property(x => x.Codigo).HasColumnName("codigo");
            builder.Property(x => x.Telefone).HasColumnName("telefone").HasMaxLength(11).IsRequired();
            builder.Property(x => x.Email).HasColumnName("email");
            builder.Property(x => x.Nome).HasColumnName("nome").IsRequired();
            builder.Property(x => x.Documento).HasColumnName("documento");
            builder.Property(x => x.InscricaoMunicipal).HasColumnName("inscricao_municipal");
            builder.Property(x => x.InscricaoEstadual).HasColumnName("inscricao_estadual");
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