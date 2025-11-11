using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueMoon.Context;
using BlueMoon.Entities.Models;
using BlueMoon.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlueMoon.Repositories
{
    public class VendaRepositorio : Repositorio<Venda>, IVendaRepositorio
    {
        public VendaRepositorio(MySqlDataBaseContext context) : base(context)
        {
        }

        public override async Task<Venda?> GetByIdAsync(Guid id)
        {
            return await _dbSet.Where(x => x.Id == id)
                                .Include(x => x.Cliente)
                                .Include(x => x.Vendedor)
                                .Include(x => x.Itens)
                                    .ThenInclude(y => y.Produto)
                                .FirstOrDefaultAsync();
        }

        public override async Task<IEnumerable<Venda?>> GetAllAsync()
        {
            return await _dbSet.Include(x => x.Cliente)
                                .Include(x => x.Vendedor)
                                .Include(x => x.Itens)
                                    .ThenInclude(y => y.Produto)
                                .ToListAsync();
        }

        public async Task AddItensAsync(Venda venda)
        {
            _dbSet.Update(venda);
            await _context.SaveChangesAsync();
        }

        public async Task Cancelar(Venda venda)
        {
            foreach (var item in venda.Itens)
                item.Produto.AdicionarEstoque(item.Quantidade);
            
            venda.CancelarVenda();
            await _context.SaveChangesAsync();
        }

        public async Task Faturar(Venda venda)
        {
            venda.FaturarVenda();
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetGreatCodeNumber()
        {
            return await _dbSet.Select(x => (int?)x.Codigo).MaxAsync() ?? 0;
        }
    }
}