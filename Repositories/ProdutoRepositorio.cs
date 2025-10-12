using BlueMoon.Entities.Enuns;
using BlueMoon.Entities.Models;
using BlueMoon.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlueMoon.Repositories
{
    public class ProdutoRepositorio : Repositorio<Produto>, IProdutoRepositorio
    {
        public ProdutoRepositorio(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Produto>> GetByDescricao(string descricao)
        {
            return await _dbSet.Where(x => x.Descricao == descricao).ToListAsync();
        }

        public async Task<IEnumerable<Produto>> GetByMarca(string marca)
        {
            return await _dbSet.Where(x => x.Marca == marca).ToListAsync();
        }

        public async Task<IEnumerable<Produto>> GetByNCM(string ncm)
        {
            return await _dbSet.Where(x => x.NCM == ncm).ToListAsync();
        }

        public async Task LogicalDeleteByIdAsync(Guid id)
        {
            var produto = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            produto.Situacao = SituacaoProdutoEnum.INATIVO;
            _dbSet.Update(produto);
            await _context.SaveChangesAsync();
        }
    }
}