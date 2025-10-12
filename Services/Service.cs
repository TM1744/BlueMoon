using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueMoon.Repositories.Interfaces;
using BlueMoon.Services.Interfaces;

namespace BlueMoon.Services
{
    public class Service<T> : IService<T> where T : class
    {
        public readonly IRepositorio<T> _repositorio;

        public Service(IRepositorio<T> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task AddAsync(T entity)
        {
            await _repositorio.AddAsync(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repositorio.GetAllAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _repositorio.GetByIdAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            await _repositorio.UpdateAsync(entity);
        }
    }
}