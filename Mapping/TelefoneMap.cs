// using BlueMoon.Models.Modelling;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;

// namespace BlueMoon.Mapping
// {
//     public sealed class TelefoneMap : IEntityTypeConfiguration<Telefone>
//     {
//         public void Configure(EntityTypeBuilder<Telefone> builder)
//         {
//             builder.ToTable("Telefones");

//             builder.HasKey(x => x.Id);

//             builder.Property(x => x.Id).HasColumnName("id");
//             builder.Property(x => x.DDD).HasColumnName("ddd").IsRequired();
//             builder.Property(x => x.Numero).HasColumnName("numero").IsRequired();
//         }
//     }
// }