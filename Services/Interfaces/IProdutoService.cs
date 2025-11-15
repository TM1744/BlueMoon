using BlueMoon.DTO;
using BlueMoon.Entities.Models;

namespace BlueMoon.Services.Interfaces
{
    public interface IProdutoService
    {
        Task<IEnumerable<Produto>> GetBySearch(ProdutoSearchDTO dto);
        Task LogicalDeleteByIdAsync(Guid id);
        Task<Produto> GetByIdAsync(Guid id);
        Task<IEnumerable<Produto>> GetAllAsync();
        Task<Produto> AddAsync(Produto produto);
        Task<Produto> UpdateAsync(Produto produto);
        Task<bool> Exists(Guid id);
        Task<ProdutoReadDTO> BuildDTO(Produto produto);
        Task<IEnumerable<ProdutoMiniReadDTO>> BuildDTOList(IEnumerable<Produto> produtos);
    }
}