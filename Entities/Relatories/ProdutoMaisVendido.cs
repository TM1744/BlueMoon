using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueMoon.Entities.Relatories
{
    public class ProdutoMaisVendido
    {
        public int Codigo {get; set;}
        public string Nome {get; set;}
        public int QuantidadeVendida {get;set;}
        public decimal ValorTotalVendido {get;set;}
    }
}