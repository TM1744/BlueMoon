using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueMoon.Entities.Enuns;

namespace BlueMoon.DTO
{
    public class ProdutoCreateDTO
    {
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public int QuantidadeEstoque { get; set; }
        public int QuantidadeEstoqueMinimo { get; set; }
        public string NCM { get; set; } = string.Empty;
        public string CodigoBarras { get; set; } = string.Empty;
        public decimal ValorCusto { get; set; }
        public decimal ValorVenda { get; set; }
        public decimal MargemLucro { get; set; }
    }

    public class ProdutoReadDTO
    {
        public string Id { get; set; } = string.Empty;
        public int Codigo { get; set; }
        public int Situacao { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public int QuantidadeEstoque { get; set; }
        public int QuantidadeEstoqueMinimo { get; set; }
        public string NCM { get; set; } = string.Empty;
        public string CodigoBarras { get; set; } = string.Empty;
        public decimal ValorCusto { get; set; }
        public decimal ValorVenda { get; set; }
        public decimal MargemLucro { get; set; }
    }

    public class ProdutoUpdateDTO
    {
        public string Id { get; set; } = string.Empty;
        public int Situacao { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public int QuantidadeEstoque { get; set; }
        public int QuantidadeEstoqueMinimo { get; set; }
        public string NCM { get; set; } = string.Empty;
        public string CodigoBarras { get; set; } = string.Empty;
        public decimal ValorCusto { get; set; }
        public decimal ValorVenda { get; set; }
        public decimal MargemLucro { get; set; }
    }
}