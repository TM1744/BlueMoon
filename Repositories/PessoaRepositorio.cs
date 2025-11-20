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
using MySqlConnector;

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

        public async Task<IEnumerable<PessoaMiniReadDTO>> GetNoUsers()
        {
            var sql = @"
                        SELECT
                            CAST(P.id AS CHAR(36)) AS Id,
                            P.codigo AS Codigo,
                            P.nome AS Nome,
                            P.telefone AS Telefone,
                            P.cidade AS Cidade,
                            CONCAT(P.logradouro, ', ', P.numero) AS Endereco
                        FROM 
                            Pessoas P
                        LEFT JOIN Usuarios U ON P.id = U.id_pessoa
                        WHERE
                            P.situacao = 1
                        GROUP BY
                            P.id, P.codigo, P.nome, P.telefone, P.cidade, P.logradouro, P.numero
                        HAVING
                        COUNT(U.id_pessoa) = 0
                        OR
                        SUM(CASE WHEN U.situacao = 1 THEN 1 ELSE 0 END) = 0
                        ORDER BY 
                        Codigo ASC;
                    ";

            return await _context.Database
                .SqlQueryRaw<PessoaMiniReadDTO>(
                    sql
                )
                .ToListAsync();
        }

        public async Task<IEnumerable<Pessoa>> GetBySearchNoUsers(PessoaSearchDTO dto)
        {
            var sql = "";
            if (dto.Codigo != 0)
            {
                sql = @"
                    SELECT
                        p.id AS Id,
                        p.tipo AS Tipo,
                        p.situacao AS Situacao,
                        p.codigo AS Codigo,
                        p.email AS Email,
                        p.nome AS Nome,
                        p.documento AS Documento,
                        p.inscricao_municipal AS InscricaoMunicipal,
                        p.inscricao_estadual AS InscricaoEstadual,
                        p.bairro AS Bairro,
                        p.cep AS CEP,
                        p.cidade AS Cidade,
                        p.complemento AS Complemento,
                        p.estado AS Estado,
                        p.logradouro AS Logradouro,
                        p.numero AS Numero,
                        p.telefone AS Telefone
                    FROM Pessoas p
                    WHERE
                        NOT EXISTS (
                            SELECT 1
                            FROM Usuarios u
                            WHERE u.id_pessoa = p.id
                                AND u.situacao = 1  -- ativo
                        )
                        AND p.codigo = @codigo;
                        AND p.situacao = 1
                    ";
            }
            else
            {
                sql = @"
                    SELECT
                        p.id AS Id,
                        p.tipo AS Tipo,
                        p.situacao AS Situacao,
                        p.codigo AS Codigo,
                        p.email AS Email,
                        p.nome AS Nome,
                        p.documento AS Documento,
                        p.inscricao_municipal AS InscricaoMunicipal,
                        p.inscricao_estadual AS InscricaoEstadual,
                        p.bairro AS Bairro,
                        p.cep AS CEP,
                        p.cidade AS Cidade,
                        p.complemento AS Complemento,
                        p.estado AS Estado,
                        p.logradouro AS Logradouro,
                        p.numero AS Numero,
                        p.telefone AS Telefone
                    FROM Pessoas p
                    WHERE
                        NOT EXISTS (
                            SELECT 1
                            FROM Usuarios u
                            WHERE u.id_pessoa = p.id
                                AND u.situacao = 1  -- ativo
                        )
                        AND p.nome LIKE @nome
                        AND p.documento LIKE @documento
                        AND p.telefone LIKE @telefone
                        AND p.situacao = 1
                        ORDER BY p.codigo ASC;
                ";
            }


            return await _context.Database
                .SqlQueryRaw<Pessoa>(
                    sql,
                    new MySqlParameter("@nome", $"%{dto.Nome}%"),
                    new MySqlParameter("@documento", $"%{dto.Documento}%"),
                    new MySqlParameter("@telefone", $"%{dto.Telefone}%"),
                    new MySqlParameter("@codigo", dto.Codigo)
                )
                .ToListAsync();
        }
    }
}