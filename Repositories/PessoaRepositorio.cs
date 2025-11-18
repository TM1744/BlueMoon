using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BlueMoon.Context;
using BlueMoon.DTO;
using BlueMoon.Entities.Enuns;
using BlueMoon.Entities.Models;
using BlueMoon.Repositories.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace BlueMoon.Repositories
{
    public class PessoaRepositorio : Repositorio<Pessoa>, IPessoaRepositorio
    {
        public PessoaRepositorio(MySqlDataBaseContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Pessoa?>> GetAllAsync()
        {
            return await _dbSet.Where(x => x.Situacao == SituacaoPessoaEnum.ATIVO).OrderBy(x => x.Codigo).ToListAsync();
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _dbSet.AnyAsync(x => x.Id == id && x.Situacao == SituacaoPessoaEnum.ATIVO);
        }

        public int GetGreaterCodeNumber()
        {
            return _dbSet.Select(x => (int?)x.Codigo).Max() ?? 0;
        }

        public async Task LogicalDeleteByIdAsync(Pessoa pessoa)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Pessoa.Id == pessoa.Id);
            if (usuario != null)
            {
                usuario.Inativar();
                _context.Update(usuario);
            }
            pessoa.Inativar();
            _dbSet.Update(pessoa);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidateUniqueness(Pessoa pessoa)
        {
            return !await _dbSet.AnyAsync(x =>
            ((x.Documento == pessoa.Documento && pessoa.Documento != "N/D") ||
            (x.InscricaoEstadual == pessoa.InscricaoEstadual && pessoa.InscricaoEstadual != "N/D") ||
            (x.InscricaoMunicipal == pessoa.InscricaoMunicipal && pessoa.InscricaoMunicipal != "N/D") ||
            (x.Email == pessoa.Email && pessoa.Email != "N/D"))
            && x.Id != pessoa.Id && x.Situacao == SituacaoPessoaEnum.ATIVO);
        }

        public async Task<IEnumerable<Pessoa>> GetBySearch(PessoaSearchDTO dto)
        {
            IQueryable<Pessoa> query = _dbSet
                .Where(x => x.Situacao == SituacaoPessoaEnum.ATIVO);

            if (dto.Codigo != 0)
            {
                query = query.Where(x => x.Codigo == dto.Codigo);
                return await query.ToListAsync();
            }

            query = query
                .Where(x => x.Nome.Contains(dto.Nome))
                .Where(x => x.Documento.Contains(dto.Documento))
                .Where(x => x.Telefone.Contains(dto.Telefone));

            return await query
                .OrderBy(x => x.Codigo)
                .ToListAsync();
        }
    }
}