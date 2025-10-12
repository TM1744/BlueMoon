using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueMoon.Entities.Enuns;

namespace BlueMoon.DTO
{
    public class ProdutoCreateDTO
    {
        public string Descricao { get; set; }
        public string? Marca { get; set; }
        public int QuantidadeEstoque { get; set; }
        public int? QuantidadeEstoqueMinimo { get; set; }
        public string? NCM { get; set; }
        public string? CodigoBarras { get; set; }
        public decimal? ValorCusto { get; set; }
        public decimal ValorVenda { get; set; }
        public decimal? MargemLucro { get; set; }
    }

    public class ProdutoReadDTO
    {
        public Guid Id { get; set; }
        public int Codigo { get; set; }
        public int Situacao { get; set; }
        public string Descricao { get; set; }
        public string Marca { get; set; }
        public int QuantidadeEstoque { get; set; }
        public int QuantidadeEstoqueMinimo { get; set; }
        public string NCM { get; set; }
        public string CodigoBarras { get; set; }
        public decimal ValorCusto { get; set; }
        public decimal ValorVenda { get; set; }
        public decimal MargemLucro { get; set; }
    }

    public class ProdutoUpdateDTO
    {
        public int Situacao { get; set; }
        public string Descricao { get; set; }
        public string? Marca { get; set; }
        public int QuantidadeEstoque { get; set; }
        public int? QuantidadeEstoqueMinimo { get; set; }
        public string? NCM { get; set; }
        public string? CodigoBarras { get; set; }
        public decimal? ValorCusto { get; set; }
        public decimal ValorVenda { get; set; }
        public decimal? MargemLucro { get; set; }
    }

    public class ProdutoDeleteDTO
    {
        public Guid Id { get; set; }
    }
}