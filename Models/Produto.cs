using BlueMoon.Models.Enuns;

namespace BlueMoon.Models;

public sealed class Produto
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    private string _descricao = "";

    private string _marca = "";

    private string _fornecedor = "";

    private string _codigo = "";

    private int _quantidadeEstoque = 0;

    private int _quantidadeEstoqueMinimo = 1;

    private string _ncm = "";

    private string _codigoBarras = "";

    private SituacaoProduto _situacao = SituacaoProduto.ATIVO;

    private decimal _valorCusto;

    private decimal _valorVenda;


    public string Descricao
    {
        get
        {
            return _descricao;
        }
        set
        {

            if (value.Length > 70)
            {
                throw new ArgumentException("Descrição excedeu o limite máximo de caracteres permitido");
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Descrição não pode ser vazia");
            }

            _descricao = value;
        }
    }

    public string Marca
    {
        get
        {
            return _marca;
        }
        set
        {
            if (value.Length > 50)
            {
                throw new ArgumentException("Marca excedeu o limite máximo de caracteres permitido!");
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                _marca = "Marca não Informada".ToUpper();
            }
            else
            {
                _marca = value.ToUpper();
            }
        }
    }

    public string Fornecedor
    {
        get
        {
            return _fornecedor;
        }
        set
        {
            if (value.Length > 70)
            {
                throw new ArgumentException("Fornecedor excedeu o limite máximo de caracteres permitido");
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                _fornecedor = "Fornecedor não Informado".ToUpper();
            }
            else
            {
                _fornecedor = value.ToUpper();
            }
        }
    }

    public string Codigo
    {
        get
        {
            return _codigo;
        }
        set
        {
            if (value.Length > 6)
            {
                throw new ArgumentException("Código Inválido! O código deve conter no máximo 6 caracteres!");
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Código não pode ser vazio");
            }

            _codigo = value.ToUpper();
        }
    }

    public int QuantidadeEstoque
    {
        get
        {
            return _quantidadeEstoque;
        }
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Quantidade estoque não pode ser negativa");
            }

            _quantidadeEstoque = value;
        }
    }

    public int QuantidadeEstoqueMinimo
    {
        get
        {
            return _quantidadeEstoqueMinimo;
        }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Quantidade de Estoque mínima não pode ser menor ou igual a zero");
            }

            _quantidadeEstoqueMinimo = value;
        }
    }

    public string NCM
    {
        get
        {
            return _ncm;
        }
        set
        {
            value = new string(value.Where(char.IsDigit).ToArray());

            if (!System.Text.RegularExpressions.Regex.IsMatch(value, @"^\d{8}$"))
            {
                throw new ArgumentException("NCM inválido! O NCM deve conter 8 dígitos numéricos!");
            }
               
            _ncm = value;
        }
    }

    public string CodigoBarras
    {
        get
        {
            return _codigoBarras;
        }
        set
        {
            if (value.Trim().Length > 128)
            {
                throw new ArgumentException("Código de Barras Inválido!");
            }

            _codigoBarras = value.ToUpper();
        }
    }

    public string Situacao
    {
        get { return _situacao.ToString(); }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Situação não pode ser vazia.");
            }
            
            if (!Enum.TryParse<SituacaoProduto>(value.ToUpper(), out var situacaoConvertida))
            {
                throw new ArgumentException("Situação inválida!");
            }

            _situacao = situacaoConvertida;
        }
    }

    public decimal ValorCusto
    {
        get
        {
            return _valorCusto;
        }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Valor de Custo não pode ser menor ou igual a zero");
            }

            if (value >= 100000000m)
            {
                throw new ArgumentException("Valor de Custo excede o tamanho permitido");
            }

            _valorCusto = Math.Round(value, 2);
        }
    }

    public decimal ValorVenda
    {
        get
        {
            return _valorVenda;
        }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Valor da Venda não pode ser menor ou igual a zero");
            }

            if (value >= 100000000m)
            {
                throw new ArgumentException("Valor de Venda excede o tamanho permitido");
            }

            _valorVenda = Math.Round(value, 2);
        }
    }

    private Produto() { }

    public Produto(string descricao, string marca, string codigo, int quantidadeEstoque, int quantidadeEstoqueMinimo, string ncm, string codigoBarras, SituacaoProduto situacaoProduto, decimal valorCusto, decimal valorVenda)
    {
        Descricao = descricao;
        Marca = marca;
        Codigo = codigo;
        QuantidadeEstoque = quantidadeEstoque;
        QuantidadeEstoqueMinimo = quantidadeEstoqueMinimo;
        NCM = ncm;
        CodigoBarras = codigoBarras;
        Situacao = situacaoProduto.ToString();
        ValorCusto = valorCusto;
        ValorVenda = valorVenda;
    }

}