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
        Task<IEnumerable<Pessoa>> GetBySearch(PessoaSearchDTO dto);
        Task LogicalDeleteByIdAsync(Pessoa pessoa);
        Task<bool> Exists(Guid id);
        Task<bool> ValidateUniqueness(Pessoa pessoa);
        int GetGreaterCodeNumber();
    }
}