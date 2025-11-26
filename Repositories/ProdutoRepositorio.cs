using BlueMoon.Context;
using BlueMoon.DTO;
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

        public override async Task<IEnumerable<Produto?>> GetAllAsync()
        {
            return await _dbSet
                .Where(x => x.Situacao == SituacaoProdutoEnum.ATIVO)
                .OrderByDescending(x => x.Codigo)
                .ThenBy(x => x.Nome)
                .ToListAsync();
        }

        public override async Task<Produto?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id && x.Situacao == SituacaoProdutoEnum.ATIVO);
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

        public int GetGreaterCodeNumber()
        {
            return _dbSet.Select(x => (int?)x.Codigo).Max() ?? 0;
        }

        public async Task<bool> ValidateUniqueness(Produto produto)
        {
            return !await _dbSet.AnyAsync(x =>
                (x.Nome == produto.Nome ||
                (x.CodigoBarras == produto.CodigoBarras && produto.CodigoBarras != "N/D"))
                && x.Id != produto.Id && x.Situacao == SituacaoProdutoEnum.ATIVO);
        }

        public async Task<IEnumerable<Produto>> GetBySearch(ProdutoSearchDTO dto)
        {
            IQueryable<Produto> query = _dbSet
                .Where(x => x.Situacao == SituacaoProdutoEnum.ATIVO);

            if (dto.Codigo != 0)
            {
                query = query.Where(x => x.Codigo == dto.Codigo);
                return await query.ToListAsync();
            }

            query = query
                .Where(x => x.Nome.Contains(dto.Nome))
                .Where(x => x.Marca.Contains(dto.Marca));

            return await query
                .OrderByDescending(x => x.Codigo)
                .ThenBy(x => x.Nome)
                .ToListAsync();
        }
    }
}