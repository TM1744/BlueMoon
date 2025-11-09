using BlueMoon.DTO;
using BlueMoon.Entities.Models;

namespace BlueMoon.Services.Interfaces
{
    public interface IPessoaService
    {
        Task<IEnumerable<Pessoa>> GetByNome(string nome);
        Task<Pessoa> GetByDocumento(string documento);
        Task<IEnumerable<Pessoa>> GetByTelefone(string telefone);

        //Busca todas as pessoas cadastradas com alguma informação
        //do local, seja endereço, cidade, bairro, etc.
        Task<IEnumerable<Pessoa>> GetByLocal(string local);
        Task<Pessoa> GetByCodigo(int codigo);
        Task LogicalDeleteByIdAsync(Guid id);
        Task<Pessoa> GetByIdAssync(Guid id);
        Task<Pessoa> AddAssync(Pessoa pessoa);
        Task<Pessoa> UpdateAssync(Pessoa pessoa);
        Task<IEnumerable<Pessoa>> GetAllAsync();
        Task<bool> Exists(Guid id);
        Task<PessoaReadDTO> BuildDTO(Pessoa pessoa);
        Task<IEnumerable<PessoaReadDTO>> BuildDTOList(IEnumerable<Pessoa> pessoas);
    }
}