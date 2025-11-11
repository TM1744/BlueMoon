using BlueMoon.DTO;
using BlueMoon.Entities.Models;

namespace BlueMoon.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<bool> PostLogin(UsuarioPostLoginDTO dto);
        Task<IEnumerable<Usuario>> GetByNome(string nome);
        Task<Usuario> GetByDocumento(string documento);
        Task<IEnumerable<Usuario>> GetByTelefone(string telefone);

        //Busca todas os usuários cadastrados com alguma informação
        //do local, seja endereço, cidade, bairro, etc.
        Task<IEnumerable<Usuario>> GetByLocal(string local);
        Task<Usuario> GetByCodigo(int codigo);
        Task LogicalDeleteByIdAsync(Guid id);
        Task<Usuario> GetByIdAssync(Guid id);
        Task<Usuario> AddAssync(Usuario usuario);
        Task<Usuario> UpdateAssync(Usuario usuario);
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<bool> Exists(Guid id);
        Task<UsuarioReadDTO> BuildDTO(Usuario usuario);
        Task<IEnumerable<UsuarioReadDTO>> BuildDTOList(IEnumerable<Usuario> usuarios);
    }
}