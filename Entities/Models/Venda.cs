using BlueMoon.Entities.Enuns;
namespace BlueMoon.Entities.Models
{
    public sealed class Venda
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public ICollection<ItemVenda> Itens { get; private set; } = [];
        public Pessoa Cliente { get; private set; }
        public Usuario Vendedor { get; private set; }
        public int Codigo { get; private set; }
        public Cupom Cupom { get; private set; }
        public ICollection<ProdutoCupom> ProdutosComCupom { get; private set; } = [];
        public EnumSituacaoVenda Situacao { get; set; } = EnumSituacaoVenda.ABERTA;
        public decimal ValorTotal { get; private set; }

        private Venda() { }

        public Venda
        (
            ICollection<ItemVenda> itens,
            Pessoa cliente,
            Usuario vendedor,
            Cupom cupom,
            ICollection<ProdutoCupom> produtosComCupom,
            EnumSituacaoVenda situacao
        )
        {
            Itens = itens;
            Cliente = cliente;
            Vendedor = vendedor;
            Cupom = cupom;
            ProdutosComCupom = produtosComCupom;
            Situacao = situacao;
            ValorTotal = CalcularValorTotal();
        }

        public void AdicionarItem(ItemVenda item) => Itens.Add(item);

        public void RemoverItem(ItemVenda item) => Itens.Remove(item);

        private decimal CalcularValorTotal()
        {
            var itens = CalcularDescontoDeProdutos();

            return itens.Sum(item => item.SubTotal);
        }

        private ICollection<ItemVenda> CalcularDescontoDeProdutos()
        {
            ICollection<ItemVenda> itens = [];
            foreach (ItemVenda item in Itens)
            {
                foreach (ProdutoCupom produtoCupom in ProdutosComCupom)
                {
                    if (item.Produto.Id.Equals(produtoCupom.Produto.Id) && produtoCupom.Cupom.Id.Equals(Cupom.Id))
                    {
                        itens.Add(DescontarValor(item));
                    }
                    else
                    {
                        itens.Add(item);
                    }
                }
            }
            return itens;
        }

        private ItemVenda DescontarValor(ItemVenda item)
        {
            if (Cupom.Tipo == TipoCupom.PORCENTAGEM)
                item.SetProdutoValorVenda(item.ProdutoValorVenda - (item.ProdutoValorVenda * (Cupom.ValorPorcentagem / 100)));

            if (Cupom.Tipo == TipoCupom.NUMERICO)
                item.SetProdutoValorVenda(item.ProdutoValorVenda - Cupom.ValorNumerico);

            return item;
        }

        public void CancelarVenda() //Quando a venda estiver aberta ou fechada
        {
            if (Situacao == EnumSituacaoVenda.CANCELADA)
            {
                throw new InvalidOperationException("Essa venda já foi cancelada!");
            }

            if (Situacao != EnumSituacaoVenda.ABERTA && Situacao != EnumSituacaoVenda.FECHADA)
            {
                throw new InvalidOperationException("Somente vendas abertas ou fechadas podem ser canceladas!");
            }

            Situacao = EnumSituacaoVenda.CANCELADA;
        }

        public void EstornarVenda()
        {
            if (Situacao == EnumSituacaoVenda.ESTORNADA)
            {
                throw new InvalidOperationException("Esta Venda já foi estornada!");
            }

            if (Situacao != EnumSituacaoVenda.FECHADA)
            {
                throw new InvalidOperationException("Só é possível estornar vendas fechadas.");
            }

            Situacao = EnumSituacaoVenda.ESTORNADA;
        }

        public void FecharVenda() //Após a entrada de todos os produtos
        {
            if (Situacao == EnumSituacaoVenda.FECHADA)
            {
                throw new InvalidOperationException("Essa venda já foi fechada!");
            }

            if (Situacao != EnumSituacaoVenda.ABERTA)
            {
                throw new InvalidOperationException("Somente vendas abertas podem ser fechadas.");
            }

            if (Itens.Count == 0)
            {
                throw new InvalidOperationException("Não é possível fechar uma venda sem itens.");
            }

            ValorTotal = CalcularValorTotal();

            Situacao = EnumSituacaoVenda.FECHADA;
        }

        public void FaturarVenda() //Após o pagamento
        {
            if (Situacao == EnumSituacaoVenda.FATURADA)
            {
                throw new InvalidOperationException("Venda já faturada!");
            }

            if (Situacao != EnumSituacaoVenda.FECHADA)
            {
                throw new InvalidOperationException("Somente vendas fechadas podem ser faturadas.");
            }

            Situacao = EnumSituacaoVenda.FATURADA;
        }
    }
}