using BlueMoon.DTO;
using BlueMoon.Entities.Models;

namespace BlueMoon.Repositories.Interfaces
{
    public interface IUsuarioRepositorio : IRepositorio<Usuario>
    {
        Task<bool> ValidateLogin(string login, string senha);
        Task<IEnumerable<Usuario?>> GetByNome(string nome);
        Task<Usuario?> GetByDocumento(string documento);
        Task<IEnumerable<Usuario?>> GetByTelefone(string telefone);

        //Busca todas os usuarios cadastrados com alguma informação
        //do local, seja endereço, cidade, bairro, etc.
        Task<IEnumerable<Usuario?>> GetByLocal(string local);
        Task<Usuario?> GetByCodigo(int codigo);
        Task LogicalDeleteByIdAsync(Usuario usuario);
        Task<bool> Exists(Guid id);
        Task<bool> ValidateUniqueness(Usuario usuario);
        Task<int> GetGreaterCodeNumber();
    }
}