using BlueMoon.DTO;
using BlueMoon.Entities.Enuns;
namespace BlueMoon.Entities.Models
{
    public sealed class Venda
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public ICollection<ItemVenda> Itens { get; private set; } = [];
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
            if (Situacao == EnumSituacaoVenda.CANCELADA)
                throw new InvalidOperationException("Essa venda já foi cancelada!");

            if (Situacao != EnumSituacaoVenda.ABERTA && Situacao != EnumSituacaoVenda.FECHADA)
                throw new InvalidOperationException("Somente vendas abertas ou fechadas podem ser canceladas!");

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
            if (Situacao == EnumSituacaoVenda.FECHADA)
                throw new InvalidOperationException("Essa venda já foi fechada!");
            
            if (Situacao != EnumSituacaoVenda.ABERTA)
                throw new InvalidOperationException("Somente vendas abertas podem ser fechadas.");
            
            if (Itens.Count == 0)
                throw new InvalidOperationException("Não é possível fechar uma venda sem itens.");
            
            CalcularValorTotal();

            Situacao = EnumSituacaoVenda.FECHADA;
        }

        public void FaturarVenda() //Após o pagamento
        {
            if (Situacao == EnumSituacaoVenda.FATURADA)
                throw new InvalidOperationException("Venda já faturada!");


            if (Situacao != EnumSituacaoVenda.FECHADA)
                throw new InvalidOperationException("Somente vendas fechadas podem ser faturadas.");

            Situacao = EnumSituacaoVenda.FATURADA;
        }
    
        private void CalcularValorTotal()
        {
            ValorTotal = Itens.Sum(x => x.SubTotal);
        }
    }
}