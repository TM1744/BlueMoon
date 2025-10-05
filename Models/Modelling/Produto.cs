using BlueMoon.Models.Enuns;

namespace BlueMoon.Models;

public sealed class Produto
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Descricao { get; private set; } = string.Empty; 
    public string Marca { get; private set; } = string.Empty;
    public string Fornecedor { get; private set; } = string.Empty;
    public string Codigo { get; private set; } = string.Empty;
    public int QuantidadeEstoque { get; private set; }
    public int QuantidadeEstoqueMinimo { get; set; } 
    public string NCM { get; private set; } = string.Empty;
    public string CodigoBarras { get; private set; } = string.Empty;
    public SituacaoProdutoEnum Situacao { get; set; } = SituacaoProdutoEnum.ATIVO;
    public decimal ValorCusto { get; set; } = decimal.Zero;
    public decimal ValorVenda { get; set; } = decimal.Zero;
    public decimal MargemLucro { get; set; } = decimal.Zero;
    private Produto() { }

    public Produto
    (
        string descricao,
        string marca,
        string fornecedor,
        int quantidadeEstoque,
        int quantidadeEstoqueMinimo,
        string ncm,
        string codigoBarras,
        SituacaoProdutoEnum situacao,
        decimal valorCusto,
        decimal valorVenda,
        decimal margemLucro
    )
    {
        Descricao = descricao;
        Marca = marca;
        Fornecedor = fornecedor;
        QuantidadeEstoque = quantidadeEstoque;
        QuantidadeEstoqueMinimo = quantidadeEstoqueMinimo;
        NCM = ncm;
        CodigoBarras = codigoBarras;
        Situacao = situacao;
        ValorCusto = valorCusto;
        ValorVenda = valorVenda;
        MargemLucro = margemLucro;
    }

    public void AdicionarEstoque(int quantidade)
    {
        if (quantidade <= 0)
        {
            throw new ArgumentException("A quantidade a adicionar deve ser maior que zero!");
        }

        QuantidadeEstoque += quantidade;
    }

    public void RemoverEstoque(int quantidade)
    {
        if (QuantidadeEstoque - quantidade < 0)
        {
            throw new ArgumentException("Estoque insuficiente");
        }

        QuantidadeEstoque -= quantidade;
    }

    public bool VerificarEstoqueAbaixoDoMinimo()
    {
        return QuantidadeEstoque < QuantidadeEstoqueMinimo;
    }

    public decimal CalcularMargemLucro() => Math.Round(ValorCusto * (1 + (MargemLucro / 100)), 2);
}