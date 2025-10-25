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
            builder.Property(x => x.Email).HasColumnName("email");
            builder.Property(x => x.Nome).HasColumnName("nome").IsRequired();
            builder.Property(x => x.Documento).HasColumnName("documento");
            builder.Property(x => x.InscricaoMunicipal).HasColumnName("inscricao_municipal");
            builder.Property(x => x.InscricaoEstadual).HasColumnName("inscricao_estadual");
            //gera uma foreign key de "id_pessoa" na tabela de telefones
            builder
                .HasMany(x => x.Telefones)
                .WithOne()
                .HasForeignKey("id_pessoa")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            //gera uma foreign key de "id_endereco" na tabela de pessoas
            builder
                .HasOne(x => x.Endereco)
                .WithOne()
                .HasForeignKey("id_endereco")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);
        }
    }
}