using System.Diagnostics;
using BlueMoon.DTO;
using BlueMoon.Entities.Enuns;

namespace BlueMoon.Entities.Models
{
    public sealed class Produto : AbstractGenericClass
    {
        public string Nome { get; private set; } = string.Empty;
        public string Descricao { get; private set; } = string.Empty;
        public string Marca { get; private set; } = string.Empty; // n obrigatorio
        public int QuantidadeEstoque { get; private set; }
        public int QuantidadeEstoqueMinimo { get; set; } // n obrigatorio
        public string NCM { get; private set; } = string.Empty; // n obrigatorio
        public string CodigoBarras { get; private set; } = string.Empty; // n obrigatorio
        public decimal ValorCusto { get; set; } = decimal.Round(0.00m, 2); // n obrigatorio
        public decimal ValorVenda { get; set; } = decimal.Round(0.00m, 2);
        public decimal MargemLucro { get; set; } = decimal.Round(0.00m, 2); // n obrigatorio

        private Produto() { }

        public Produto(ProdutoCreateDTO dto)
        {
            Nome = NotEmptyString(dto.Nome);
            Descricao = NotEmptyString(dto.Descricao);
            Marca = NotEmptyString(dto.Marca);
            QuantidadeEstoque = dto.QuantidadeEstoque;
            QuantidadeEstoqueMinimo = dto.QuantidadeEstoqueMinimo;
            NCM = SequenceNumberString(dto.NCM);
            CodigoBarras = NotEmptyString(dto.CodigoBarras);
            Situacao = SituacaoGenericEnum.ATIVO;
            ValorCusto = decimal.Round(dto.ValorCusto, 2);
            ValorVenda = decimal.Round(dto.ValorVenda, 2);
            MargemLucro = decimal.Round(dto.MargemLucro, 2);
        }

        public Produto(ProdutoUpdateDTO dto)
        {
            ID = Guid.Parse(dto.Id);
            Situacao = (SituacaoGenericEnum)dto.Situacao;
            Nome = NotEmptyString(dto.Nome);
            Descricao = NotEmptyString(dto.Descricao);
            Marca = NotEmptyString(dto.Marca);
            QuantidadeEstoque = dto.QuantidadeEstoque;
            QuantidadeEstoqueMinimo = dto.QuantidadeEstoqueMinimo;
            NCM = SequenceNumberString(dto.NCM);
            CodigoBarras = NotEmptyString(dto.CodigoBarras);
            ValorCusto = decimal.Round(dto.ValorCusto, 2);
            ValorVenda = decimal.Round(dto.ValorVenda, 2);
            MargemLucro = decimal.Round(dto.MargemLucro, 2);
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