using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueMoon.Entities.Models;

namespace BlueMoon.Repositories.Interfaces
{
    public interface IProdutoRepositorio : IRepositorio<Produto>
    {
        Task<IEnumerable<Produto>> GetByDescricao(string descricao);
        Task<IEnumerable<Produto>> GetByNCM(string ncm);
        Task<IEnumerable<Produto>> GetByMarca(string marca);
        Task LogicalDeleteByIdAsync(Produto produto);
    }
}