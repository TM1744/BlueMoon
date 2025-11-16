using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueMoon.DTO;

namespace BlueMoon.Services.Interfaces
{
    public interface IRelatorioService
    {
        Task<byte[]> GerarRelatorioProdutosMaisVendidosAsync(string inicio, string fim);
        Task<IEnumerable<ProdutosMaisVendidosDTO>> GetProdutosMaisVendidosAsync(string inicio, string fim);
        Task<byte[]> GerarRelatorioPessoasQueMaisCompraramAsync(string inicio, string fim);
        Task<IEnumerable<PessoasQueMaisCompraramDTO>> GetPessoasQueMaisCompraramAsync(string inicio, string fim);
        Task<byte[]> GerarRelatorioVendedoresQueMaisVenderamAsync(string inicio, string fim);
        Task<IEnumerable<VendedoresQueMaisVenderamDTO>> GetVendedoresQueMaisVenderamsAsync(string inicio, string fim);
    }
}