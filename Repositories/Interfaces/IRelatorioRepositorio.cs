using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueMoon.Context;
using BlueMoon.Entities.Relatories;

namespace BlueMoon.Repositories.Interfaces
{
    public interface IRelatorioRepositorio
    {
        Task<ProdutoMaisVendido> GetProdutosMaisVendidos (DateTime inicio, DateTime fim);
        
    }
}