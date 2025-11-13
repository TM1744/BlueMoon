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
            return await _dbSet.Where(x => x.Id == id && x.Situacao == SituacaoPessoaEnum.ATIVO)
                                    .Include(x => x.Pessoa).FirstOrDefaultAsync();
        }  

        public override async Task<IEnumerable<Usuario?>> GetAllAsync()
        {
            return await _dbSet.Where(x => x.Situacao == SituacaoPessoaEnum.ATIVO)
                                .Include(x => x.Pessoa).OrderBy(x => x.Codigo).ToListAsync();
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _dbSet.AnyAsync(x => x.Id == id && x.Situacao == SituacaoPessoaEnum.ATIVO);
        }

        public async Task<Usuario?> GetByCodigo(int codigo)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Codigo == codigo
             && x.Situacao == SituacaoPessoaEnum.ATIVO);
        }

        public async Task<Usuario?> GetByDocumento(string documento)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Pessoa.Documento == documento
             && x.Situacao == SituacaoPessoaEnum.ATIVO);
        }

        public async Task<IEnumerable<Usuario?>> GetByLocal(string local)
        {
            return await _dbSet.Where(x =>
             (x.Pessoa.Logradouro.Contains(local) ||
             x.Pessoa.Cidade.Contains(local) ||
             x.Pessoa.Complemento.Contains(local) ||
             x.Pessoa.Bairro.Contains(local) ||
             x.Pessoa.Numero.Contains(local))
             && x.Situacao == SituacaoPessoaEnum.ATIVO).ToListAsync();
        }

        public async Task<IEnumerable<Usuario?>> GetByNome(string nome)
        {
            return await _dbSet.Where(x => x.Pessoa.Nome.Contains(nome)
                && x.Situacao == SituacaoPessoaEnum.ATIVO).ToListAsync();
        }

        public async Task<IEnumerable<Usuario?>> GetByTelefone(string telefone)
        {
            return await _dbSet.Where(x => x.Pessoa.Telefone.Contains(telefone) &&
             x.Situacao == SituacaoPessoaEnum.ATIVO).ToListAsync();
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
            ((x.Pessoa.Documento == usuario.Pessoa.Documento && usuario.Pessoa.Documento != "N/D") ||
            (x.Pessoa.InscricaoEstadual == usuario.Pessoa.InscricaoEstadual && usuario.Pessoa.InscricaoEstadual != "N/D") ||
            (x.Pessoa.InscricaoMunicipal == usuario.Pessoa.InscricaoMunicipal && usuario.Pessoa.InscricaoMunicipal != "N/D") ||
            (x.Pessoa.Email == usuario.Pessoa.Email && usuario.Pessoa.Email != "N/D") ||
            x.Login.Equals(usuario.Login) ||
            (x.Pessoa.Id == usuario.Pessoa.Id))
            && x.Id != usuario.Id && x.Situacao == SituacaoPessoaEnum.ATIVO);
        }

        public async Task<bool> ValidateLogin(string login, string senha)
        {
            return await _dbSet.AnyAsync(x => x.Login == login && x.Senha == senha);
        }
    }
}