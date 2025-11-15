using BlueMoon.DTO;

namespace BlueMoon.Repositories.Interfaces
{
    public interface IRelatorioRepositorio
    {
        Task<IEnumerable<ProdutosMaisVendidosDTO>> GetProdutosMaisVendidos (DateTime inicio, DateTime fim);
        
    }
}