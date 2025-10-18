using BlueMoon.DTO;
using BlueMoon.Entities.Enuns;

namespace BlueMoon.Entities.Models
{
    public sealed class Produto
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Descricao { get; private set; } = string.Empty;
        public string Marca { get; private set; } = string.Empty; // n obrigatorio
        public int Codigo { get; set; }
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
            Marca = createDTO.Marca.ToUpper();
            QuantidadeEstoque = createDTO.QuantidadeEstoque;
            QuantidadeEstoqueMinimo = createDTO.QuantidadeEstoqueMinimo;
            NCM = createDTO.NCM;
            CodigoBarras = createDTO.CodigoBarras;
            Situacao = SituacaoProdutoEnum.ATIVO;
            ValorCusto = decimal.Round(createDTO.ValorCusto, 2);
            ValorVenda = decimal.Round(createDTO.ValorVenda, 2);
            MargemLucro = decimal.Round(createDTO.MargemLucro, 2);
        }

        public Produto(ProdutoUpdateDTO updateDTO)
        {
            Id = Guid.Parse(updateDTO.Id);
            Situacao = (SituacaoProdutoEnum)updateDTO.Situacao;
            Descricao = updateDTO.Descricao.ToUpper();
            Marca = updateDTO.Marca.ToUpper();
            QuantidadeEstoque = updateDTO.QuantidadeEstoque;
            QuantidadeEstoqueMinimo = updateDTO.QuantidadeEstoqueMinimo;
            NCM = updateDTO.NCM;
            CodigoBarras = updateDTO.CodigoBarras;
            ValorCusto = decimal.Round(updateDTO.ValorCusto, 2);
            ValorVenda = decimal.Round(updateDTO.ValorVenda, 2);
            MargemLucro = decimal.Round(updateDTO.MargemLucro, 2);
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