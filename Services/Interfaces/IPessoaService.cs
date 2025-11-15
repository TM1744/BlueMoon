using BlueMoon.DTO;
using BlueMoon.Entities.Models;

namespace BlueMoon.Services.Interfaces
{
    public interface IPessoaService
    {
        Task<IEnumerable<Pessoa>> GetBySearch(PessoaSearchDTO dto);
        Task LogicalDeleteByIdAsync(Guid id);
        Task<Pessoa> GetByIdAssync(Guid id);
        Task<Pessoa> AddAssync(Pessoa pessoa);
        Task<Pessoa> UpdateAssync(Pessoa pessoa);
        Task<IEnumerable<Pessoa>> GetAllAsync();
        Task<bool> Exists(Guid id);
        Task<PessoaReadDTO> BuildDTO(Pessoa pessoa);
        Task<IEnumerable<PessoaMiniReadDTO>> BuildDTOList(IEnumerable<Pessoa> pessoas);
    }
}