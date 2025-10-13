using BlueMoon.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlueMoon.Mapping
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Codigo).HasColumnName("codigo");
            builder.Property(x => x.Situacao).HasColumnName("situacao").HasColumnType("int").IsRequired();
            builder.Property(x => x.Descricao).HasColumnName("descricao").IsRequired();
            builder.Property(x => x.Marca).HasColumnName("marca");
            builder.Property(x => x.QuantidadeEstoque).HasColumnName("quantidade_estoque").IsRequired();
            builder.Property(x => x.QuantidadeEstoqueMinimo).HasColumnName("quantidade_estoque_minimo");
            builder.Property(x => x.NCM).HasColumnName("ncm");
            builder.Property(x => x.CodigoBarras).HasColumnName("codigo_barras");
            builder.Property(x => x.ValorCusto).HasColumnName("valor_custo");
            builder.Property(x => x.ValorVenda).HasColumnName("valor_venda").IsRequired();
            builder.Property(x => x.MargemLucro).HasColumnName("margem_lucro");
        }
    }
}