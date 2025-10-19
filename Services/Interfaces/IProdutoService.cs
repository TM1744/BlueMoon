using BlueMoon.DTO;
using BlueMoon.Entities.Models;

namespace BlueMoon.Services.Interfaces
{
    public interface IProdutoService
    {
        Task<IEnumerable<ProdutoReadDTO>> GetByDescricao(string descricao);
        Task<IEnumerable<ProdutoReadDTO>> GetByNCM(string ncm);
        Task<IEnumerable<ProdutoReadDTO>> GetByMarca(string marca);
        Task<ProdutoReadDTO> GetByCodigo(int codigo);
        Task LogicalDeleteByIdAsync(Guid id);
        Task<ProdutoReadDTO> GetByIdAsync(Guid id);
        Task<IEnumerable<ProdutoReadDTO>> GetAllAsync();
        Task<ProdutoReadDTO> AddAsync(Produto produto);
        Task<ProdutoReadDTO> UpdateAsync(Produto produto);
    }
}