using BlueMoon.Context;
using BlueMoon.Entities.Enuns;
using BlueMoon.Entities.Models;
using BlueMoon.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlueMoon.Repositories
{
    public class ProdutoRepositorio : Repositorio<Produto>, IProdutoRepositorio
    {
        public ProdutoRepositorio(MySqlDataBaseContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Produto>> GetByDescricao(string descricao)
        {
            return await _dbSet.Where(x => x.Descricao.Contains(descricao)).ToListAsync();
        }

        public async Task<IEnumerable<Produto>> GetByMarca(string marca)
        {
            return await _dbSet.Where(x => x.Marca.Contains(marca)).ToListAsync();
        }

        public async Task<IEnumerable<Produto>> GetByNCM(string ncm)
        {
            return await _dbSet.Where(x => x.NCM.Contains(ncm)).ToListAsync();
        }

        public async Task<Produto> GetByCodigo(int codigo)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Codigo == codigo);
        }

        public async Task LogicalDeleteByIdAsync(Produto produto)
        {
            if (produto.Situacao != SituacaoProdutoEnum.INATIVO)
            {
                produto.Situacao = SituacaoProdutoEnum.INATIVO;
                _dbSet.Update(produto);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _dbSet.AnyAsync(x => x.Id == id);
        }

        public async Task<int> GetGreaterCodeNumber()
        {
            return await _dbSet.Select(x => (int?)x.Codigo).MaxAsync() ?? 0;
        }

        public async Task<bool> ValidateUniqueness(Produto produto)
        {
            return !await _dbSet.AnyAsync(x =>
                (x.Descricao == produto.Descricao ||
                (x.CodigoBarras == produto.CodigoBarras && produto.CodigoBarras != ""))
                && x.Id != produto.Id);
        }
    }
}