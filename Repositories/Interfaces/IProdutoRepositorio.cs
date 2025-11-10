using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueMoon.DTO;
using BlueMoon.Entities.Models;

namespace BlueMoon.Repositories.Interfaces
{
    public interface IProdutoRepositorio : IRepositorio<Produto>
    {
        Task<IEnumerable<Produto>> GetBySearch(ProdutoSearchDTO dto);
        Task LogicalDeleteByIdAsync(Produto produto);
        Task<bool> Exists(Guid id);
        Task<bool> ValidateUniqueness(Produto produto);
        Task<int> GetGreaterCodeNumber();
    }
}