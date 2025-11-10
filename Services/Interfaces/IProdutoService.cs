using BlueMoon.DTO;
using BlueMoon.Entities.Models;

namespace BlueMoon.Services.Interfaces
{
    public interface IProdutoService
    {
        Task<IEnumerable<Produto>> GetByNome(string nome);
        Task<IEnumerable<Produto>> GetByNCM(string ncm);
        Task<IEnumerable<Produto>> GetByMarca(string marca);
        Task<Produto> GetByCodigo(int codigo);
        Task LogicalDeleteByIdAsync(Guid id);
        Task<Produto> GetByIdAsync(Guid id);
        Task<IEnumerable<Produto>> GetAllAsync();
        Task<Produto> AddAsync(Produto produto);
        Task<Produto> UpdateAsync(Produto produto);
        Task<bool> Exists(Guid id);
        Task<ProdutoReadDTO> BuildDTO(Produto produto);
        Task<IEnumerable<ProdutoReadDTO>> BuildDTOList(IEnumerable<Produto> produtos);
    }
}