using BlueMoon.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlueMoon.Mapping
{
    public sealed class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id").HasMaxLength(36).IsRequired();
            builder.Property(x => x.Codigo).HasColumnName("codigo").IsRequired();
            builder.Property(x => x.Situacao).HasColumnName("situacao").IsRequired();
            builder.Property(x => x.Login).HasColumnName("login").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Senha).HasColumnName("senha").HasColumnType("char(64)").IsRequired();
            builder.Property(x => x.Cargo).HasColumnName("cargo").IsRequired();
            builder.Property(x => x.Salario).HasColumnName("salario").HasColumnType("decimal(11,2)").IsRequired();
            builder.Property(x => x.Admissao).HasColumnName("admissao").IsRequired();
            builder.Property(x => x.HorarioInicioCargaHoraria).HasColumnName("horario_inicio_carga_horaria").IsRequired();
            builder.Property(x => x.HorarioFimCargaHoraria).HasColumnName("horario_fim_carga_horaria").IsRequired();
            builder.Property<Guid>("id_pessoa").HasColumnName("id_pessoa").IsRequired();

            builder.HasOne(x => x.Pessoa)
                   .WithMany()
                   .HasForeignKey("id_pessoa")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}