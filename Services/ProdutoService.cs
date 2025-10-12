using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueMoon.Entities.Models;
using BlueMoon.Repositories;
using BlueMoon.Repositories.Interfaces;
using BlueMoon.Services.Interfaces;

namespace BlueMoon.Services
{
    public class ProdutoService : Service<Produto>, IProdutoService
    {
        public ProdutoService(ProdutoRepositorio repositorio) : base(repositorio)
        {
        }

        public async Task<IEnumerable<Produto>> GetByDescricao(string descricao)
        {
            return await _repositorio.Get
        }

        public Task<IEnumerable<Produto>> GetByMarca(string marca)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Produto>> GetByNCM(string ncm)
        {
            throw new NotImplementedException();
        }

        public Task LogicalDeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}