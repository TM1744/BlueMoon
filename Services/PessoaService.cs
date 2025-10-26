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
        public async Task<PessoaReadDTO> AddAssync(Pessoa pessoa)
        {
            if (!await _pessoaRepositorio.ValidateUniqueness(pessoa))
                throw new ArgumentException("A documentação, inscrição municipal ou inscrição estdual" +
                " da pessoa já foram cadastrados");

            pessoa.Codigo = await _pessoaRepositorio.GetGreaterCodeNumber() + 1;
            await _pessoaRepositorio.AddAsync(pessoa);

            return await BuildDTO(pessoa);
        }

        public async Task<PessoaReadDTO?> GetByCodigo(int codigo)
        {
            var pessoa = await _pessoaRepositorio.GetByCodigo(codigo);

            if (pessoa.Situacao == SituacaoPessoaEnum.ATIVO)
                return await BuildDTO(pessoa);

            return null;
        }

        public async Task<PessoaReadDTO?> GetByDocumento(string documento)
        {
            documento = Regex.Replace(documento, "[^0-9]", "");

            var pessoa = await _pessoaRepositorio.GetByDocumento(documento);

            return await BuildDTO(pessoa);
        }

        public async Task<PessoaReadDTO> GetByIdAssync(Guid id)
        {
            if (await _pessoaRepositorio.Exists(id) == false)
                throw new ArgumentException("O id fornecido não existe no banco de dados");

            var pessoa = await _pessoaRepositorio.GetByIdAsync(id);

            if (pessoa.Situacao == SituacaoPessoaEnum.ATIVO)
                return await BuildDTO(pessoa);

            return null; 
        }

        public async Task<IEnumerable<PessoaReadDTO?>> GetByLocal(string local)
        {
            var pessoas = await _pessoaRepositorio.GetByLocal(local);
            ICollection<PessoaReadDTO> dtos = [];       
            foreach(Pessoa pessoa in pessoas)
            {
                if(pessoa.Situacao == SituacaoPessoaEnum.ATIVO)
                    dtos.Add(await BuildDTO(pessoa));
            }
            return dtos;
        }

        public async Task<IEnumerable<PessoaReadDTO?>> GetByNome(string nome)
        {
            var pessoas = await _pessoaRepositorio.GetByNome(nome);
            ICollection<PessoaReadDTO> dtos = [];       
            foreach(Pessoa pessoa in pessoas)
            {
                if(pessoa.Situacao == SituacaoPessoaEnum.ATIVO)
                    dtos.Add(await BuildDTO(pessoa));
            }
            return dtos;
        }

        public async Task<IEnumerable<PessoaReadDTO?>> GetByTelefone(string telefone)
        {
            telefone = Regex.Replace(telefone, "[^0-9]", "");
            var pessoas = await _pessoaRepositorio.GetByTelefone(telefone);
            ICollection<PessoaReadDTO> dtos = [];       
            foreach(Pessoa pessoa in pessoas)
            {
                if(pessoa.Situacao == SituacaoPessoaEnum.ATIVO)
                    dtos.Add(await BuildDTO(pessoa));
            }
            return dtos;
        }

        public async Task LogicalDeleteByIdAsync(Guid id)
        {
            if (await _pessoaRepositorio.Exists(id) == false)
                throw new ArgumentException("O id fornecido não existe no banco de dados");

            var pessoa = await _pessoaRepositorio.GetByIdAsync(id);
            await _pessoaRepositorio.LogicalDeleteByIdAsync(pessoa);
        }

        public async Task<PessoaReadDTO> UpdateAssync(Pessoa pessoa)
        {
            if (await _pessoaRepositorio.Exists(pessoa.Id) == false)
                throw new ArgumentException("O id fornecido não existe no banco de dados");

            if (!await _pessoaRepositorio.ValidateUniqueness(pessoa))
                throw new ArgumentException("A descrição do produto ou seu código de barras já foram cadastrados");

            await _pessoaRepositorio.UpdateAsync(pessoa);

            return await BuildDTO(pessoa);
        }

        public async Task<IEnumerable<PessoaReadDTO>> GetAllAsync()
        {
            var pessoas = await _pessoaRepositorio.GetAllAsync();
            ICollection<PessoaReadDTO> dtos = [];
            foreach (Pessoa pessoa in pessoas)
            {
                if (pessoa.Situacao == SituacaoPessoaEnum.ATIVO)
                    dtos.Add(await BuildDTO(pessoa));
            }
            
            return dtos;
        }

        private async Task<PessoaReadDTO> BuildDTO (Pessoa pessoa)
        {
            PessoaReadDTO dto = new PessoaReadDTO();
            dto.Id = pessoa.Id.ToString();
            dto.Codigo = pessoa.Codigo;
            dto.Nome = pessoa.Nome;
            dto.Documento = pessoa.Documento;
            dto.Tipo = (int)pessoa.Tipo;
            dto.Situacao = (int)pessoa.Situacao;
            dto.Email = pessoa.Email;
            dto.InscricaoMunicipal = pessoa.InscricaoMunicipal;
            dto.InscricaoEstadual = pessoa.InscricaoEstadual;

            foreach (Telefone telefone in pessoa.Telefones)
            {
                TelefoneReadDTO telefoneDTO = new TelefoneReadDTO();
                telefoneDTO.Id = telefone.Id.ToString();
                telefoneDTO.DDD = (int)telefone.DDD;
                telefoneDTO.Numero = telefone.Numero;
                dto.Telefones.Add(telefoneDTO);
            }

            EnderecoReadDTO endereco = new EnderecoReadDTO();
            endereco.Id = pessoa.Endereco.Id.ToString();
            endereco.CEP = pessoa.Endereco.CEP;
            endereco.Logradouro = pessoa.Endereco.Logradouro;
            endereco.Complemento = pessoa.Endereco.Complemento;
            endereco.Cidade = pessoa.Endereco.Cidade;
            endereco.Bairro = pessoa.Endereco.Bairro;
            endereco.Numero = pessoa.Endereco.Numero;
            endereco.Estado = (int)pessoa.Endereco.Estado;

            dto.Endereco = endereco;

            return dto;
        }

    }
}