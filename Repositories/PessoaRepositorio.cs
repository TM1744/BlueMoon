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

        public async Task<Pessoa?> GetByCodigo(int codigo)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Codigo == codigo
             && x.Situacao == SituacaoPessoaEnum.ATIVO);
        }

        public async Task<Pessoa?> GetByDocumento(string documento)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Documento == documento
             && x.Situacao == SituacaoPessoaEnum.ATIVO);
        }

        public async Task<IEnumerable<Pessoa?>> GetByLocal(string local)
        {
            return await _dbSet.Where(x =>
             (x.Logradouro.Contains(local) ||
             x.Cidade.Contains(local) ||
             x.Complemento.Contains(local) ||
             x.Bairro.Contains(local) ||
             x.Numero.Contains(local))
             && x.Situacao == SituacaoPessoaEnum.ATIVO).ToListAsync();
        }

        public async Task<IEnumerable<Pessoa?>> GetByNome(string nome)
        {
            return await _dbSet.Where(x => x.Nome.Contains(nome)
                && x.Situacao == SituacaoPessoaEnum.ATIVO).ToListAsync();
        }

        public async Task<IEnumerable<Pessoa?>> GetByTelefone(string telefone)
        {
            return await _dbSet.Where(x => x.Telefone.Contains(telefone) &&
                x.Situacao == SituacaoPessoaEnum.ATIVO).ToListAsync();
        }

        public int GetGreaterCodeNumber()
        {
            return _dbSet.Select(x => (int?)x.Codigo).Max() ?? 0;
        }

        public async Task LogicalDeleteByIdAsync(Pessoa pessoa)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Pessoa.Id == pessoa.Id);
            if(usuario != null)
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
    }
}