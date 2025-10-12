using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueMoon.Entities.Models;

namespace BlueMoon.Services.Interfaces
{
    public interface IProdutoService : IService<Produto>
    {
        Task<IEnumerable<Produto>> GetByDescricao(string descricao);
        Task<IEnumerable<Produto>> GetByNCM(string ncm);
        Task<IEnumerable<Produto>> GetByMarca(string marca);
        Task LogicalDeleteByIdAsync(Guid id);
    }
}