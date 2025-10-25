using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueMoon.DTO;
using BlueMoon.Entities.Models;

namespace BlueMoon.Repositories.Interfaces
{
    public interface IPessoaRepositorio : IRepositorio<Pessoa>
    {
        Task<IEnumerable<Pessoa?>> GetByNome(string nome);
        Task<Pessoa?> GetByDocumento(string documento);
        Task<IEnumerable<Pessoa?>> GetByTelefone(string telefone);

        //Busca todas as pessoas cadastradas com alguma informação
        //do local, seja endereço, cidade, bairro, etc.
        Task<IEnumerable<Pessoa?>> GetByLocal(string local);
        Task<Pessoa?> GetByCodigo(int codigo);
        Task LogicalDeleteByIdAsync(Pessoa pessoa);
        Task<bool> Exists(Guid id);
        Task<bool> ValidateUniqueness(Pessoa pessoa);
        Task<int> GetGreaterCodeNumber();
    }
}