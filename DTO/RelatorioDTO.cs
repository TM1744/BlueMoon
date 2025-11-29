using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueMoon.DTO
{
    public class ProdutosMaisVendidosDTO
    {
        public string Id {get;set;} = string.Empty;
        public int Codigo {get;set;}
        public int Situacao {get; set;}
        public string Nome {get;set;} = string.Empty;
        public int QuantidadeVendida {get;set;}
        public int EstoqueAtual {get;set;}
        public decimal TotalVendido {get;set;} = 0.00m;
    }

    public class PessoasQueMaisCompraramDTO
    {
        public string Id {get;set;} = string.Empty;
        public int Codigo {get;set;}
        public int Situacao {get;set;}
        public string Nome {get;set;} = string.Empty;
        public int QuantidadeVendas{get;set;}
        public decimal ValorTotalVendas{get;set;}
    }

    public class VendedoresQueMaisVenderamDTO
    {
        public string Id {get;set;} = string.Empty;
        public int Codigo {get;set;}
        public int Situacao {get;set;}
        public string Nome {get;set;} = string.Empty;
        public int QuantidadeVendas {get;set;}
        public decimal ValorTotalVendas {get;set;}
    }

    public class PeriodoBuscaDTO
    {
        public string DataInicio {get;set;} = string.Empty;
        public string DataFim {get;set;} = string.Empty;
    }
}