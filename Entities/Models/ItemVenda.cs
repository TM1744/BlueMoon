using BlueMoon.DTO;

namespace BlueMoon.Entities.Models
{
    public class ItemVenda
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Produto Produto { get; private set; }
        public string ProdutoNome { get; private set; } = string.Empty;
        public string ProdutoMarca { get; private set; } = string.Empty;
        public int ProdutoCodigo { get; private set; }
        public decimal ProdutoValorVenda { get; private set; }
        public int Quantidade { get; private set; }
        public decimal SubTotal { get; private set; } = decimal.Zero;

        private ItemVenda() { }

        public ItemVenda(Produto produto, int quantidade)
        {
            Produto = produto;
            Quantidade = quantidade;
            ProdutoValorVenda = decimal.Round(produto.ValorVenda, 2);
            SubTotal = CalcularSubtotal();
            ProdutoNome = produto.Nome;
            ProdutoMarca = produto.Marca;
            ProdutoCodigo = produto.Codigo;

            Produto.RemoverEstoque(quantidade);
        }

        private decimal CalcularSubtotal() => SubTotal = Produto.ValorVenda * Quantidade;
    }
}