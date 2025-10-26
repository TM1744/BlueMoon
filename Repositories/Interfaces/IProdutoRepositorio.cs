using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueMoon.Entities.Models;

namespace BlueMoon.Repositories.Interfaces
{
    public interface IProdutoRepositorio : IRepositorio<Produto>
    {
        Task<IEnumerable<Produto>> GetByNome(string nome);
        Task<IEnumerable<Produto>> GetByNCM(string ncm);
        Task<IEnumerable<Produto>> GetByMarca(string marca);
        Task<Produto> GetByCodigo(int codigo);
        Task LogicalDeleteByIdAsync(Produto produto);
        Task<bool> Exists(Guid id);
        Task<bool> ValidateUniqueness(Produto produto);
        Task<int> GetGreaterCodeNumber();
    }
}