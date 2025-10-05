using BlueMoon.Models.Enuns;
using Microsoft.Net.Http.Headers;

namespace BlueMoon.Models;

public sealed class Venda
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    private Pessoa _cliente;

    private Usuario _usuario;

    private Cupom? _cupom;

    private string _codigo;

    private EnumSituacaoVenda _situacao;

    private decimal _valorTotal;

    private List<ItemVenda> _itens = new List<ItemVenda>();

    public IReadOnlyList<ItemVenda> Itens => _itens.AsReadOnly();


    public Pessoa Cliente
    {
        get { return _cliente; }
        set
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value), "Cliente não pode ser nulo!");
            }

            _cliente = value;
        }
    }

    public Usuario Usuario
    {
        get { return _usuario; }
        set
        {

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value), "Usuário não pode ser nulo!");
            }

            _usuario = value;
        }
    }

    public Cupom? Cupom
    {
        get { return _cupom; }
        set { _cupom = value; }
    }

    public string Codigo
    {
        get { return _codigo; }
        set
        {

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Código não pode ser vazio");
            }

            if (value.Length > 6)
            {
                throw new ArgumentException("Código Inválido! O código deve conter no máximo 6 caracteres!");
            }

            _codigo = value.ToUpper();
        }
    }


    public EnumSituacaoVenda Situacao
    {
        get { return _situacao; }
        set
        {
            if (!Enum.IsDefined(typeof(EnumSituacaoVenda), value))
            {
                throw new ArgumentException("Situação inválida!");
            }

            _situacao = value;
        }
    }

    public decimal ValorTotal
    {
        get { return _valorTotal; }
        private set
        {
            if (value < 0)
            {
                throw new ArgumentException("Valor total não pode ser negativo!");
            }

            _valorTotal = value;
        }
    }

    private Venda() { }

    public Venda(Pessoa cliente, Usuario usuario, Cupom? cupom, string codigo)
    {
        ArgumentNullException.ThrowIfNull(cliente);
        ArgumentNullException.ThrowIfNull(usuario);


        Cliente = cliente;
        Usuario = usuario;
        Cupom = cupom;
        Codigo = codigo;
        Situacao = EnumSituacaoVenda.ABERTA;
        ValorTotal = 0;
    }

    public void AdicionarItem(ItemVenda item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        _itens.Add(item);
        AtualizarValorTotal();
    }

    public void RemoverItem(ItemVenda item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        _itens.Remove(item);
        AtualizarValorTotal();
    }

    public decimal AtualizarValorTotal()
    {
        return ValorTotal = _itens.Sum(ItemVenda => ItemVenda.SubTotal);
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

        if (_itens.Count == 0)
        {
            throw new InvalidOperationException("Não é possível fechar uma venda sem itens.");
        }

        _valorTotal = AtualizarValorTotal();

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