using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueMoon.Services.Interfaces
{
    public interface IRelatorioService
    {
        byte[] GerarRelatorioProdutosMaisVendidos();        
    }
}