using BlueMoon.DTO;
using BlueMoon.Entities.Enuns;
namespace BlueMoon.Entities.Models
{
    public sealed class Venda
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public ICollection<ItemVenda> Itens { get; private set; } = new List<ItemVenda>();
        public Pessoa Cliente { get; private set; }
        public Usuario Vendedor { get; private set; }
        public int Codigo { get; set; }
        public EnumSituacaoVenda Situacao { get; set; }
        public decimal ValorTotal { get; private set; } = 0.00m;
        public DateTime Data { get; private set; } = DateTime.Now;

        private Venda() { }
        public Venda (Usuario vendedor, Pessoa cliente)
        {
            Situacao = EnumSituacaoVenda.ABERTA;
            Vendedor = vendedor;
            Cliente = cliente;
        }

        public void AdicionarItens(IEnumerable<ItemVenda> itens)
        {
            foreach (var item in itens)
                Itens.Add(item);
            
            FecharVenda();
        }

        public void CancelarVenda() //Quando a venda estiver aberta ou fechada
        {
            if (Situacao == EnumSituacaoVenda.FECHADA)
                foreach (var item in Itens)
                    item.Produto.AdicionarEstoque(item.Quantidade);

            Situacao = EnumSituacaoVenda.CANCELADA;
        }

        public void EstornarVenda()
        {
            if (Situacao == EnumSituacaoVenda.ESTORNADA)
                throw new InvalidOperationException("Esta Venda já foi estornada!");
            
            if (Situacao != EnumSituacaoVenda.FECHADA)
                throw new InvalidOperationException("Só é possível estornar vendas fechadas.");

            Situacao = EnumSituacaoVenda.ESTORNADA;
        }

        private void FecharVenda() //Após a entrada de todos os produtos
        {
            CalcularValorTotal();
            Situacao = EnumSituacaoVenda.FECHADA;
        }

        public void FaturarVenda() => Situacao = EnumSituacaoVenda.FATURADA;
    
        private void CalcularValorTotal() => ValorTotal = decimal.Round(Itens.Sum(x => x.SubTotal), 2);
    }
}