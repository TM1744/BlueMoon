// using BlueMoon.Models.Modelling;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;

// namespace BlueMoon.Mapping
// {
//     public sealed class PessoaMap : IEntityTypeConfiguration<Pessoa>
//     {
//         public void Configure(EntityTypeBuilder<Pessoa> builder)
//         {
//             builder.ToTable("Pessoas");

//             builder.HasKey(x => x.Id);

//             builder.Property(x => x.Id).HasColumnName("id");
//             builder.Property(x => x.Tipo).HasColumnName("tipo").IsRequired();
//             builder.Property(x => x.Situacao).HasColumnName("situacao").IsRequired();
//             builder.Property(x => x.Codigo).HasColumnName("codigo").ValueGeneratedOnAdd();
//             builder.Property(x => x.Email).HasColumnName("email");
//             builder.Property(x => x.Nome).HasColumnName("nome");
//             builder.Property(x => x.CPF_CNPJ).HasColumnName("cpf_cnpj").IsRequired();
//             builder.Property(x => x.RazaoSocial).HasColumnName("razao_social");
//             builder.Property(x => x.NomeFantasia).HasColumnName("nome_fantasia");
//             builder.Property(x => x.InscricaoMunicipal).HasColumnName("inscricao_municipal");

//             //gera uma foreign key de "id_pessoa" na tabela de telefones
//             builder
//                 .HasMany(x => x.Telefones)
//                 .WithOne()
//                 .HasForeignKey("id_pessoa")
//                 .OnDelete(DeleteBehavior.Cascade);

//             //gera uma foreign key de "id_endereco" na tabela de pessoas
//             builder
//                 .HasOne(x => x.Endereco)
//                 .WithOne()
//                 .HasForeignKey("id_endereco")
//                 .OnDelete(DeleteBehavior.Cascade);
//         }
//     }
// }