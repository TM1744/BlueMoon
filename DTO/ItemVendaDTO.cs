using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueMoon.DTO
{
    public class ItemVendaCreateDTO
    {
        public string IdProduto { get; set; } = string.Empty;
        public int Quantidade { get; set; }
    }

    public class ItemVendaReadDTO
    {
        public string Id { get; set; } = string.Empty;
        public string IdProduto { get; set; } = string.Empty;
        public string ProdutoNome { get; set; } = string.Empty;
        public string ProdutoMarca { get; set; } = string.Empty;
        public int ProdutoCodigo { get; set; }
        public decimal ProdutoValorVenda { get; set; }
        public int Quantidade { get; set; }
        public decimal SubTotal { get; set; }
    }
}