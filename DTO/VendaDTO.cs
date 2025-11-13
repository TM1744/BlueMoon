using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueMoon.DTO
{
    public class VendaCreateDTO
    {
        public string IdPessoa { get; set; } = string.Empty;
        public string IdUsuario { get; set; } = string.Empty;
    }

    public class VendaReadDTO
    {
        public string Id { get; set; } = string.Empty;
        public string IdCliente { get; set; } = string.Empty;
        public string NomeCliente { get; set; } = string.Empty;
        public string IdVendedor { get; set; } = string.Empty;
        public string NomeVendedor { get; set; } = string.Empty;
        public ICollection<ItemVendaReadDTO> Itens { get; set; } = [];
        public int Codigo { get; set; }
        public int Situacao { get; set; }
        public decimal ValorTotal { get; set; } = 0.00m;
        public string DataAbertura { get; set; } = string.Empty;
        public string DataFaturamento { get; set; } = string.Empty;

    }
}