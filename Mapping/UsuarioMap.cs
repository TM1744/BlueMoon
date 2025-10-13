// using BlueMoon.Models.Modelling;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;

// namespace BlueMoon.Mapping
// {
//     public sealed class UsuarioMap : IEntityTypeConfiguration<Usuario>
//     {
//         public void Configure(EntityTypeBuilder<Usuario> builder)
//         {
//             builder.ToTable("Usuarios");

//             builder.HasKey(x => x.Id);

//             builder.Property(x => x.Id).HasColumnName("id");
//             builder.Property(x => x.Login).HasColumnName("login").IsRequired();
//             builder.Property(x => x.Senha).HasColumnName("senha").IsRequired();
//             builder.Property(x => x.Cargo).HasColumnName("cargo").IsRequired();
//             builder.Property(x => x.Salario).HasColumnName("salario").IsRequired();
//             builder.Property(x => x.Admissao).HasColumnName("admissao").IsRequired();
//             builder.Property(x => x.HorarioInicioCargaHoraria).HasColumnName("horario_inicio_carga_horaria").IsRequired();
//             builder.Property(x => x.HorarioFimCargaHoraria).HasColumnName("horario_fim_carga_horaria").IsRequired();
//         }
//     }
// }