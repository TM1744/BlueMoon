using BlueMoon.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlueMoon.Mapping
{
    public class ItemVendaMap : IEntityTypeConfiguration<ItemVenda>
    {
        public void Configure(EntityTypeBuilder<ItemVenda> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id").HasMaxLength(36);
            builder.Property(x => x.ProdutoNome).HasColumnName("produto_nome").HasMaxLength(70).IsRequired();
            builder.Property(x => x.ProdutoMarca).HasColumnName("produto_marca").HasMaxLength(50);
            builder.Property(x => x.ProdutoCodigo).HasColumnName("produto_codigo");
            builder.Property(x => x.ProdutoValorVenda).HasColumnName("produto_valor_venda").HasColumnType("decimal(11,2)").IsRequired();
            builder.Property(x => x.Quantidade).HasColumnName("quantidade").IsRequired();
            builder.Property(x => x.SubTotal).HasColumnName("subtotal").HasColumnType("decimal(11,2)").IsRequired();
            builder.Property<Guid>("id_produto").HasColumnName("id_produto").IsRequired();
            builder.Property<Guid>("id_venda").HasColumnName("id_venda").IsRequired();


            builder
                .HasOne(x => x.Produto)
                .WithMany()
                .HasForeignKey("id_produto")
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}