using BlueMoon.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlueMoon.Mapping
{
    public sealed class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.Property<Guid>("PessoaId").HasColumnName("pessoa_id");

            builder.HasKey("PessoaId");

            builder.Property(x => x.Codigo).HasColumnName("codigo");
            builder.Property(x => x.Situacao).HasColumnName("situacao").IsRequired();
            builder.Property(x => x.Login).HasColumnName("login").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Senha).HasColumnName("senha").HasMaxLength(64).IsRequired();
            builder.Property(x => x.Cargo).HasColumnName("cargo").IsRequired();
            builder.Property(x => x.Salario).HasColumnName("salario");
            builder.Property(x => x.Admissao).HasColumnName("admissao");
            builder.Property(x => x.HorarioInicioCargaHoraria).HasColumnName("horario_inicio_carga_horaria");
            builder.Property(x => x.HorarioFimCargaHoraria).HasColumnName("horario_fim_carga_horaria");
            
            builder.HasOne(u => u.Pessoa)
                   .WithMany()
                   .HasForeignKey("PessoaId")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}