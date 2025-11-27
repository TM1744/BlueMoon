using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueMoon.Repositories.Interfaces
{
    public interface IRepositorio<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T?>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
    }
}