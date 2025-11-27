using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueMoon.Context;
using BlueMoon.DTO;
using BlueMoon.Entities.Enuns;
using BlueMoon.Entities.Models;
using BlueMoon.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlueMoon.Repositories
{
    public class UsuarioRepositorio : Repositorio<Usuario>, IUsuarioRepositorio
    {
        public UsuarioRepositorio(MySqlDataBaseContext context) : base(context)
        {
        }

        public override async Task<Usuario?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .Where(x => x.Id == id && x.Situacao == SituacaoPessoaEnum.ATIVO)
                .Include(x => x.Pessoa)
                .FirstOrDefaultAsync();
        }  

        public override async Task<IEnumerable<Usuario?>> GetAllAsync()
        {
            return await _dbSet
                .Where(x => x.Situacao == SituacaoPessoaEnum.ATIVO)
                .Include(x => x.Pessoa)
                .OrderByDescending(x => x.Codigo)
                .ThenBy(x => x.Pessoa.Nome)
                .ToListAsync();
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _dbSet.AnyAsync(x => x.Id == id && x.Situacao == SituacaoPessoaEnum.ATIVO);
        }
        
        public int GetGreaterCodeNumber()
        {
            return _dbSet.Select(x => (int?)x.Codigo).Max() ?? 0;
        }

        public async Task LogicalDeleteByIdAsync(Usuario usuario)
        {
            usuario.Inativar();
            _dbSet.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidateUniqueness(Usuario usuario)
        {
            return !await _dbSet.AnyAsync(x =>
            (x.Login.Equals(usuario.Login) ||
            (x.Pessoa.Id == usuario.Pessoa.Id))
            && x.Id != usuario.Id && x.Situacao == SituacaoPessoaEnum.ATIVO);
        }

        public async Task<bool> ValidateLogin(string login, string senha)
        {
            return await _dbSet.AnyAsync(x => x.Login == login && x.Senha == senha && x.Situacao == SituacaoPessoaEnum.ATIVO);
        }

        public async Task<IEnumerable<Usuario>> GetBySearch(UsuarioSearchDTO dto)
        {
            IQueryable<Usuario> query = _dbSet
                .Where(x => x.Situacao == SituacaoPessoaEnum.ATIVO);

            if (dto.Codigo != 0)
            {
                query = query.Where(x => x.Codigo == dto.Codigo)
                                .Include(x => x.Pessoa);
                return await query.ToListAsync();
            }

            query = query
                .Where(x => x.Pessoa.Nome.Contains(dto.Nome))
                .Where(x => x.Pessoa.Documento.Contains(dto.Documento))
                .Where(x => x.Pessoa.Telefone.Contains(dto.Telefone))
                .Include(x => x.Pessoa);

            return await query
                .OrderByDescending(x => x.Codigo)
                .ThenBy(x => x.Pessoa.Nome)
                .ToListAsync();
        }
    }
}