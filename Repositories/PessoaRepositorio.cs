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
using Microsoft.EntityFrameworkCore;

namespace BlueMoon.Repositories
{
    public class PessoaRepositorio : Repositorio<Pessoa>, IPessoaRepositorio
    {
        public PessoaRepositorio(MySqlDataBaseContext context) : base(context)
        {
        }
        public override async Task<IEnumerable<Pessoa>?> GetAllAsync()
        {
            return await _dbSet.Include(x => x.Endereco)
                .Include(y => y.Telefones).ToListAsync();
        }

        public override async Task<Pessoa?> GetByIdAsync(Guid id)
        {
            return await _dbSet.Include(x => x.Endereco)
                .Include(x => x.Telefones).FirstOrDefaultAsync();
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _dbSet.AnyAsync(x => x.Id == id);
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
             (x.Endereco.Logradouro.Contains(local) ||
             x.Endereco.Cidade.Contains(local) ||
             x.Endereco.Complemento.Contains(local) ||
             x.Endereco.Bairro.Contains(local) ||
             x.Endereco.Numero.Contains(local))
             && x.Situacao == SituacaoPessoaEnum.ATIVO).ToListAsync();
        }

        public async Task<IEnumerable<Pessoa?>> GetByNome(string nome)
        {
            return await _dbSet.Where(x => x.Nome.Contains(nome)
                && x.Situacao == SituacaoPessoaEnum.ATIVO).ToListAsync();
        }

        public async Task<IEnumerable<Pessoa?>> GetByTelefone(string telefone)
        {
            return await _dbSet.Where(x => x.Telefones.Any(
                t => t.Numero == telefone)
                && x.Situacao == SituacaoPessoaEnum.ATIVO).ToListAsync();
        }

        public async Task<int> GetGreaterCodeNumber()
        {
            return await _dbSet.Select(x => (int?)x.Codigo).MaxAsync() ?? 0;
        }

        public async Task LogicalDeleteByIdAsync(Pessoa pessoa)
        {
            if(pessoa.Situacao != SituacaoPessoaEnum.INATIVO)
            {
                pessoa.Inativar();
                _dbSet.Update(pessoa);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ValidateUniqueness(Pessoa pessoa)
        {
            return !await _dbSet.AnyAsync(x =>
            ((x.Documento == pessoa.Documento && pessoa.Documento != "N/D") ||
            (x.InscricaoEstadual == pessoa.InscricaoEstadual && pessoa.InscricaoEstadual != "N/D") ||
            (x.InscricaoMunicipal == pessoa.InscricaoMunicipal && pessoa.InscricaoMunicipal != "N/D"))
            && x.Id != pessoa.Id);
        }
    }
}