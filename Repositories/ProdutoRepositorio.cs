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

        public async Task<IEnumerable<Produto?>> GetByNome(string nome)
        {
            return await _dbSet.Where(x => x.Nome.Contains(nome) &&
            x.Situacao == SituacaoProdutoEnum.ATIVO).ToListAsync();
        }

        public async Task<IEnumerable<Produto?>> GetByMarca(string marca)
        {
            return await _dbSet.Where(x => x.Marca.Contains(marca) &&
            x.Situacao == SituacaoProdutoEnum.ATIVO).ToListAsync();
        }

        public async Task<IEnumerable<Produto?>> GetByNCM(string ncm)
        {
            return await _dbSet.Where(x => x.NCM.Contains(ncm) &&
            x.Situacao == SituacaoProdutoEnum.ATIVO).ToListAsync();
        }

        public async Task<Produto?> GetByCodigo(int codigo)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Codigo == codigo &&
            x.Situacao == SituacaoProdutoEnum.ATIVO);
        }

        public async Task LogicalDeleteByIdAsync(Produto produto)
        {
            produto.Inativar();
            _dbSet.Update(produto);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _dbSet.AnyAsync(x => x.Id == id &&
                x.Situacao == SituacaoProdutoEnum.ATIVO);
        }

        public async Task<int> GetGreaterCodeNumber()
        {
            return await _dbSet.Select(x => (int?)x.Codigo).MaxAsync() ?? 0;
        }

        public async Task<bool> ValidateUniqueness(Produto produto)
        {
            return !await _dbSet.AnyAsync(x =>
                (x.Descricao == produto.Descricao ||
                (x.CodigoBarras == produto.CodigoBarras && produto.CodigoBarras != "N/D"))
                && x.Id != produto.Id && x.Situacao == SituacaoProdutoEnum.ATIVO);
        }
    }
}