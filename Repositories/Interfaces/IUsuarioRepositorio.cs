using BlueMoon.DTO;
using BlueMoon.Entities.Models;

namespace BlueMoon.Repositories.Interfaces
{
    public interface IUsuarioRepositorio : IRepositorio<Usuario>
    {
        Task<IEnumerable<Usuario>> GetBySearch(UsuarioSearchDTO dto);
        Task<bool> ValidateLogin(string login, string senha);
        Task LogicalDeleteByIdAsync(Usuario usuario);
        Task<bool> Exists(Guid id);
        Task<bool> ValidateUniqueness(Usuario usuario);
        int GetGreaterCodeNumber();
    }
}