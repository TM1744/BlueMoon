using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BlueMoon.Context;
using BlueMoon.DTO;
using BlueMoon.Entities.Enuns;
using BlueMoon.Entities.Models;
using BlueMoon.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

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
                                    .ThenInclude(x => x.Pessoa)
                                .Include(x => x.Itens)
                                    .ThenInclude(y => y.Produto)
                                .FirstOrDefaultAsync();
        }

        public override async Task<IEnumerable<Venda?>> GetAllAsync()
        {
            return await _dbSet.Include(x => x.Cliente)
                                .Include(x => x.Vendedor)
                                    .ThenInclude(x => x.Pessoa)
                                .Include(x => x.Itens)
                                    .ThenInclude(y => y.Produto)
                                .OrderBy(x => x.Codigo)
                                .ToListAsync();
        }

        public async Task AddItensAsync(Venda venda)
        {
            foreach (var item in venda.Itens)
            {
                await _context.ItemVendas.AddAsync(item);
                _context.Produtos.Update(item.Produto);
            }
            _dbSet.Update(venda);
                
            await _context.SaveChangesAsync();
        }

        public async Task Cancelar(Venda venda)
        {
            venda.CancelarVenda();
            await _context.SaveChangesAsync();
        }

        public async Task Faturar(Venda venda)
        {
            venda.FaturarVenda();
            await _context.SaveChangesAsync();
        }

        public async Task Estornar(Venda venda)
        {
            venda.EstornarVenda();
            await _context.SaveChangesAsync();
        }

        public int GetGreatCodeNumber()
        {
            return _dbSet.Select(x => (int?)x.Codigo).Max() ?? 0;
        }

        public async Task<bool> ValidateIntegrity(Venda venda)
        {
            return !await _dbSet.AnyAsync(x => x.Situacao != EnumSituacaoVenda.ABERTA && x.Id == venda.Id);
        }

        public async Task<IEnumerable<Venda>> GetBySearch(VendaSearchDTO dto)
        {
            IQueryable<Venda> query = _dbSet
                                            .Include(x => x.Cliente)
                                            .Include(x => x.Vendedor)
                                                .ThenInclude(x => x.Pessoa)
                                            .Include(x => x.Itens)
                                                .ThenInclude(x => x.Produto);

            if (dto.Codigo != 0)
            {
                query = query.Where(x => x.Codigo == dto.Codigo);
                return await query.ToListAsync();
            }

            if(dto.DataAbertura == "")
            {
                query = query
                .Where(x => x.Cliente.Nome.Contains(dto.NomeCliente))
                .Where(x => x.Situacao == (EnumSituacaoVenda)dto.Situacao);
            }
            else
            {
                DateTime data = DateTime.ParseExact(dto.DataAbertura, "ddMMyyyy", CultureInfo.InvariantCulture);

                DateTime inicioDoDia = data.Date;
                DateTime fimDoDia = data.Date.AddDays(1).AddTicks(-1);

                query = query
                .Where(x => x.Cliente.Nome.Contains(dto.NomeCliente))
                .Where(x => x.Situacao == (EnumSituacaoVenda)dto.Situacao)
                .Where(x => x.DataAbertura >= inicioDoDia &&
                                          x.DataAbertura <= fimDoDia);
            }
            
            return await query
                .OrderBy(x => x.Codigo)
                .ToListAsync();
        }
    }
}