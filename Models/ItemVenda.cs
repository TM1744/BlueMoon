namespace BlueMoon.Models;

public sealed class ItemVenda
{
    public Guid Id { get; private set; }

    private Venda _venda;

    private Produto _produto;

    private int _quantidade = 0;

    private decimal _subtotal;

    public Venda Venda
    {
        get
        {
            return _venda;
        }
        set
        {
            if (value == null)
            {
                throw new ArgumentException(nameof(value), "Venda não pode ser nulo!");
            }

            _venda = value;
        }
    }


    public Produto Produto
    {
        get
        {
            return _produto;
        }
        set
        {
            if (value == null)
            {
                throw new ArgumentException(nameof(value), "Produto não pode ser nulo!");
            }

            _produto = value;
        }
    }

    public int Quantidade
    {
        get
        {
            return _quantidade;
        }
        set
        {

            if (value <= 0)
            {
                throw new ArgumentException("A quantidade não deve ser menor ou igual a zero!");
            }

            _quantidade = value;
            AtualizarSubtotal();
        }
    }


    public decimal SubTotal
    {
        get
        {
            return _subtotal;
        }
        private set
        {
            if (value <= 0)
            {
                throw new ArgumentException("O valor não pode ser menor ou igual a zero");
            }
            else if (value > 100000000m)
            {
                throw new ArgumentException("Valor limite excedido!");
            }

            _subtotal = value;
        }
    }


    private ItemVenda() { }

    public ItemVenda(Venda venda, Produto produto, int quantidade)
    {
        ArgumentNullException.ThrowIfNull(venda);
        ArgumentNullException.ThrowIfNull(produto);
        ArgumentNullException.ThrowIfNull(quantidade);

        Venda = venda;
        Produto = produto;
        Quantidade = quantidade;
        AtualizarSubtotal();
    }

    public void AtualizarSubtotal()
    {
       SubTotal = Produto.ValorVenda * Quantidade; 
    }

    public void AlterarQuantidade(int novaQuantidade)
    {
        if (novaQuantidade <= 0)
        { 
            throw new ArgumentException("A quantidade deve ser maior que zero.");
        }

        Quantidade = novaQuantidade;
    }
}