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
            return await _dbSet.Where(x => x.Situacao == SituacaoProdutoEnum.ATIVO).OrderBy(x => x.Codigo).ToListAsync();
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
            if (dto.Codigo != 0)
                return await _dbSet
                .Where(
                    x => x.Codigo == dto.Codigo &&
                    x.Nome.Contains(dto.Nome.ToUpper()) &&
                    x.Marca.Contains(dto.Marca.ToUpper()) &&
                    x.Situacao == SituacaoProdutoEnum.ATIVO
                    )
                .OrderBy(x => x.Codigo)
                .ToListAsync();

            return await _dbSet
            .Where(
                x => x.Nome.Contains(dto.Nome.ToUpper()) &&
                x.Marca.Contains(dto.Marca.ToUpper()) &&
                x.Situacao == SituacaoProdutoEnum.ATIVO
            )
            .ToListAsync();
        }
    }
}