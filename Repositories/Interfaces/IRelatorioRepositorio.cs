using BlueMoon.DTO;

namespace BlueMoon.Repositories.Interfaces
{
    public interface IRelatorioRepositorio
    {
        Task<IEnumerable<ProdutosMaisVendidosDTO>> GetProdutosMaisVendidos (DateTime inicio, DateTime fim);
        Task<IEnumerable<PessoasQueMaisCompraramDTO>> GetPessoasQueMaisCompraram (DateTime inicio, DateTime fim);
        Task<IEnumerable<VendedoresQueMaisVenderamDTO>> GetVendedoresQueMaisVenderam(DateTime inicio, DateTime fim);
        
    }
}