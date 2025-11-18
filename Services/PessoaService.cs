using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BlueMoon.DTO;
using BlueMoon.Entities.Enuns;
using BlueMoon.Entities.Models;
using BlueMoon.Mapping;
using BlueMoon.Repositories.Interfaces;
using BlueMoon.Services.Interfaces;
using Humanizer;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BlueMoon.Services
{
    public class PessoaService : IPessoaService
    {
        public readonly IPessoaRepositorio _pessoaRepositorio;

        public PessoaService(IPessoaRepositorio pessoaRepositorio)
        {
            _pessoaRepositorio = pessoaRepositorio;
        }
        
        public async Task<Pessoa> AddAssync(Pessoa pessoa)
        {
            if (!await _pessoaRepositorio.ValidateUniqueness(pessoa))
                throw new ArgumentException("A documentação, e-mail, inscrição municipal ou inscrição estadual" +
                " da pessoa já foram cadastrados");

            pessoa.Codigo = _pessoaRepositorio.GetGreaterCodeNumber() + 1;
            await _pessoaRepositorio.AddAsync(pessoa);

            return pessoa;
        }
        public async Task<Pessoa> GetByIdAssync(Guid id)
        {
            var pessoa = await _pessoaRepositorio.GetByIdAsync(id);

            if (pessoa.Situacao != SituacaoPessoaEnum.ATIVO || pessoa == null)
                throw new ArgumentException("Não há nenhuma pessoa com esse ID");

            return pessoa;
        }

        public async Task LogicalDeleteByIdAsync(Guid id)
        {
            var pessoa = await _pessoaRepositorio.GetByIdAsync(id);

            if (pessoa == null || pessoa.Situacao == SituacaoPessoaEnum.INATIVO)
                throw new ArgumentException("Não há nenhuma pessoa com esse ID ou ela já foi deletada");

            await _pessoaRepositorio.LogicalDeleteByIdAsync(pessoa);
        }

        public async Task<Pessoa> UpdateAssync(Pessoa pessoa)
        {
            var old = await _pessoaRepositorio.GetByIdAsync(pessoa.Id);

            if (old.Documento != "N/D" && old.Documento != pessoa.Documento)
                throw new ArgumentException("Não é possível alterar o documento de pessoa");

            if (!await _pessoaRepositorio.ValidateUniqueness(pessoa))
                throw new ArgumentException("A documentação, e-mail, inscrição municipal ou inscrição estadual" +
                " da pessoa já foram cadastrados");

            old.Atualizar(pessoa);

            await _pessoaRepositorio.UpdateAsync(old);

            return pessoa;
        }

        public async Task<IEnumerable<Pessoa>> GetAllAsync()
        {
            var pessoas = await _pessoaRepositorio.GetAllAsync();

            if (!pessoas.Any())
                throw new InvalidOperationException("Não há nenhuma pessoa cadastrada");

            return pessoas;
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _pessoaRepositorio.Exists(id);
        }


        public async Task<PessoaReadDTO> BuildDTO(Pessoa pessoa)
        {
            PessoaReadDTO dto = new PessoaReadDTO();
            dto.Id = pessoa.Id.ToString();
            dto.Codigo = pessoa.Codigo;
            dto.Nome = pessoa.Nome;
            dto.Telefone = pessoa.Telefone;
            dto.Documento = pessoa.Documento;
            dto.Tipo = (int)pessoa.Tipo;
            dto.Situacao = (int)pessoa.Situacao;
            dto.Email = pessoa.Email;
            dto.InscricaoMunicipal = pessoa.InscricaoMunicipal;
            dto.InscricaoEstadual = pessoa.InscricaoEstadual;
            dto.CEP = pessoa.CEP;
            dto.Logradouro = pessoa.Logradouro;
            dto.Complemento = pessoa.Complemento;
            dto.Cidade = pessoa.Cidade;
            dto.Bairro = pessoa.Bairro;
            dto.Numero = pessoa.Numero;
            dto.Estado = (int)pessoa.Estado;

            return dto;
        }

        public async Task<IEnumerable<PessoaMiniReadDTO>> BuildDTOList(IEnumerable<Pessoa> pessoas)
        {
            ICollection<PessoaMiniReadDTO> dtos = [];

            foreach (Pessoa pessoa in pessoas)
            {
                PessoaMiniReadDTO dto = new PessoaMiniReadDTO();
                dto.Id = pessoa.Id.ToString();
                dto.Codigo = pessoa.Codigo;
                dto.Nome = pessoa.Nome;
                dto.Telefone = pessoa.Telefone;
                dto.Cidade = pessoa.Cidade;
                dto.Endereco = pessoa.Logradouro + ", " + pessoa.Numero;
                dto.Telefone = pessoa.Telefone;
                dtos.Add(dto);
            }

            return dtos;
        }

        public async Task<IEnumerable<Pessoa>> GetBySearch(PessoaSearchDTO dto)
        {
            dto.Documento = Regex.Replace(dto.Documento, "[^0-9]", "");
            dto.Telefone = Regex.Replace(dto.Telefone, "[^0-9]", "");
            dto.Nome = dto.Nome.ToUpper();

            var pessoas = await _pessoaRepositorio.GetBySearch(dto);

            if(!pessoas.Any())
                throw new ArgumentException("Não há nenhuma pessoa compatível com as descrições de busca");

            return pessoas;
        }
    }
}