using BlueMoon.DTO;
using BlueMoon.Entities.Models;

namespace BlueMoon.Services.Interfaces
{
    public interface IPessoaService
    {
        Task<IEnumerable<PessoaReadDTO?>> GetByNome(string nome);
        Task<PessoaReadDTO?> GetByDocumento(string documento);
        Task<IEnumerable<PessoaReadDTO?>> GetByTelefone(string telefone);

        //Busca todas as pessoas cadastradas com alguma informação
        //do local, seja endereço, cidade, bairro, etc.
        Task<IEnumerable<PessoaReadDTO?>> GetByLocal(string local);
        Task<PessoaReadDTO?> GetByCodigo(int codigo);
        Task LogicalDeleteByIdAsync(Guid id);
        Task<PessoaReadDTO> GetByIdAssync(Guid id);
        Task<PessoaReadDTO> AddAssync(Pessoa pessoa);
        Task<PessoaReadDTO> UpdateAssync(Pessoa pessoa);
        Task<IEnumerable<PessoaReadDTO>> GetAllAsync();
    }
}