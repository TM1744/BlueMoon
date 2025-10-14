using BlueMoon.DTO;
using BlueMoon.Entities.Enuns;

namespace BlueMoon.Entities.Models
{
    public sealed class Produto
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Descricao { get; private set; } = string.Empty;
        public string Marca { get; private set; } = string.Empty; // n obrigatorio
        public int Codigo { get; private set; }
        public int QuantidadeEstoque { get; private set; }
        public int QuantidadeEstoqueMinimo { get; set; } // n obrigatorio
        public string NCM { get; private set; } = string.Empty; // n obrigatorio
        public string CodigoBarras { get; private set; } = string.Empty; // n obrigatorio
        public SituacaoProdutoEnum Situacao { get; set; } = SituacaoProdutoEnum.INDEFINIDO;
        public decimal ValorCusto { get; set; } = decimal.Round(0.00m, 2); // n obrigatorio
        public decimal ValorVenda { get; set; } = decimal.Round(0.00m, 2);
        public decimal MargemLucro { get; set; } = decimal.Round(0.00m, 2); // n obrigatorio

        private Produto() { }

        public Produto(ProdutoCreateDTO createDTO)
        {
            Descricao = createDTO.Descricao.ToUpper();
            Marca = createDTO.Marca ?? "INDEFINIDO";
            QuantidadeEstoque = createDTO.QuantidadeEstoque;
            QuantidadeEstoqueMinimo = createDTO.QuantidadeEstoqueMinimo;
            NCM = createDTO.NCM ?? "INDEFINIDO";
            CodigoBarras = createDTO.CodigoBarras ?? "INDEFINIDO";
            Situacao = SituacaoProdutoEnum.ATIVO;
            ValorCusto = decimal.Round(createDTO.ValorCusto, 2);
            ValorVenda = decimal.Round(createDTO.ValorVenda, 2);
            MargemLucro = decimal.Round(createDTO.MargemLucro, 2);
        }

        public Produto(ProdutoReadDTO produtoReadDTO)
        {
            Id = Guid.Parse(produtoReadDTO.Id);
            Codigo = produtoReadDTO.Codigo;
            Situacao = (SituacaoProdutoEnum)produtoReadDTO.Situacao;
            Descricao = produtoReadDTO.Descricao;
            Marca = produtoReadDTO.Marca;
            QuantidadeEstoque = produtoReadDTO.QuantidadeEstoque;
            QuantidadeEstoqueMinimo = produtoReadDTO.QuantidadeEstoqueMinimo;
            NCM = produtoReadDTO.NCM;
            CodigoBarras = produtoReadDTO.CodigoBarras;
            ValorCusto = produtoReadDTO.ValorCusto;
            ValorVenda = produtoReadDTO.ValorVenda;
            MargemLucro = produtoReadDTO.MargemLucro;
        }

        public Produto(ProdutoUpdateDTO produtoUpdateDTO)
        {
            Id = Guid.Parse(produtoUpdateDTO.Id);
            Situacao = (SituacaoProdutoEnum)produtoUpdateDTO.Situacao;
            Descricao = produtoUpdateDTO.Descricao ?? "INDEFINIDO";
            Marca = produtoUpdateDTO.Marca ?? "INDEFINIDO";
            QuantidadeEstoque = produtoUpdateDTO.QuantidadeEstoque;
            QuantidadeEstoqueMinimo = produtoUpdateDTO.QuantidadeEstoqueMinimo;
            NCM = produtoUpdateDTO.NCM ?? "INDEFINIDO";
            CodigoBarras = produtoUpdateDTO.CodigoBarras ?? "INDEFINIDO";
            ValorCusto = produtoUpdateDTO.ValorCusto;
            ValorVenda = produtoUpdateDTO.ValorVenda;
            MargemLucro = produtoUpdateDTO.MargemLucro;
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
}