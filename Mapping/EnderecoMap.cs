// using BlueMoon.Models.Modelling;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;

// namespace BlueMoon.Mapping
// {
//     public sealed class EnderecoMap : IEntityTypeConfiguration<Endereco>
//     {
//         public void Configure(EntityTypeBuilder<Endereco> builder)
//         {
//             builder.ToTable("Enderecos");

//             builder.HasKey(x => x.Id);

//             builder.Property(x => x.Id).HasColumnName("id");
//             builder.Property(x => x.CEP).HasColumnName("cep").IsRequired();
//             builder.Property(x => x.Logradouro).HasColumnName("logradouro").IsRequired();
//             builder.Property(x => x.Numero).HasColumnName("numero").IsRequired();
//             builder.Property(x => x.Complemento).HasColumnName("complemento");
//             builder.Property(x => x.Bairro).HasColumnName("bairro").IsRequired();
//             builder.Property(x => x.Cidade).HasColumnName("cidade").IsRequired();
//             builder.Property(x => x.Estado).HasColumnName("estado").IsRequired();
//         }
//     }
// }