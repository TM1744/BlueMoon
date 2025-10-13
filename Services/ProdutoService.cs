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
        public readonly IProdutoRepositorio _produtoRepositorio;
        public ProdutoService(IRepositorio<Produto> repositorio, IProdutoRepositorio produtoRepositorio) : base(repositorio)
        {
            _produtoRepositorio = produtoRepositorio;
        }

        public async Task<IEnumerable<Produto>> GetByDescricao(string descricao)
        {
            return await _produtoRepositorio.GetByDescricao(descricao);
        }

        public async Task<IEnumerable<Produto>> GetByMarca(string marca)
        {
            return await _produtoRepositorio.GetByMarca(marca);
        }

        public async Task<IEnumerable<Produto>> GetByNCM(string ncm)
        {
            return await _produtoRepositorio.GetByNCM(ncm);
        }

        public async Task LogicalDeleteByIdAsync(Guid id)
        {
            await _produtoRepositorio.LogicalDeleteByIdAsync(id);
        }
    }
}