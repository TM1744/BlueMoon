using BlueMoon.DTO;
using BlueMoon.Entities.Models;

namespace BlueMoon.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<Usuario>> GetBySearch(UsuarioSearchDTO dto);
        Task<bool> PostLogin(UsuarioPostLoginDTO dto);
        Task LogicalDeleteByIdAsync(Guid id);
        Task<Usuario> GetByIdAssync(Guid id);
        Task<Usuario> AddAssync(Usuario usuario);
        Task<Usuario> UpdateAssync(Usuario usuario);
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<bool> Exists(Guid id);
        Task<UsuarioReadDTO> BuildDTO(Usuario usuario);
        Task<IEnumerable<UsuarioMiniReadDTO>> BuildDTOList(IEnumerable<Usuario> usuarios);
    }
}