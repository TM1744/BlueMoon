using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BlueMoon.DTO;
using BlueMoon.Entities.Enuns;
using BlueMoon.Entities.Models;
using BlueMoon.Repositories.Interfaces;
using BlueMoon.Services.Interfaces;

namespace BlueMoon.Services
{
    public class UsuarioService : IUsuarioService
    {
        public readonly IUsuarioRepositorio _repositorio;

        public UsuarioService(IUsuarioRepositorio usuarioRepositorio)
        {
            _repositorio = usuarioRepositorio;
        }

        public async Task<Usuario> AddAssync(Usuario usuario)
        {
            if (!await _repositorio.ValidateUniqueness(usuario))
                throw new ArgumentException("A documentação, e-mail, inscrição municipal, login ou inscrição estadual" +
                " do usuário já foram cadastrados, ou há outro registro ativo de usuário para a mesma pessoa");

            usuario.Codigo = await _repositorio.GetGreaterCodeNumber() + 1;
            await _repositorio.AddAsync(usuario);

            return usuario;
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _repositorio.Exists(id);
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            var usuarios = await _repositorio.GetAllAsync();

            if (!usuarios.Any())
                throw new InvalidOperationException("Não há nenhum usuário cadastrado");

            return usuarios;
        }

        public async Task<Usuario> GetByCodigo(int codigo)
        {
            var usuario = await _repositorio.GetByCodigo(codigo);

            if (usuario == null)
                throw new ArgumentException("Não há nenhum usuário com esse codigo");

            return usuario;
        }

        public async Task<Usuario> GetByDocumento(string documento)
        {
            var usuario = await _repositorio.GetByDocumento(Regex.Replace(documento, "[^0-9]", ""));

            if (usuario == null)
                throw new ArgumentException("Não há nenhum usuário com esse documento");

            return usuario;
        }

        public async Task<Usuario> GetByIdAssync(Guid id)
        {
            var usuario = await _repositorio.GetByIdAsync(id);

            if (usuario.Situacao != SituacaoPessoaEnum.ATIVO || usuario == null)
                throw new ArgumentException("Não há nenhum Usuário com esse ID");

            return usuario;
        }

        public async Task<IEnumerable<Usuario>> GetByLocal(string local)
        {
            var usuarios = await _repositorio.GetByLocal(local.ToUpper());

            if (!usuarios.Any())
                throw new ArgumentException("Não há nenhum usuário que reside nesse local");

            return usuarios;
        }

        public async Task<IEnumerable<Usuario>> GetByNome(string nome)
        {
            var usuarios = await _repositorio.GetByNome(nome.ToUpper());

            if (!usuarios.Any())
                throw new ArgumentException("Não há nenhum usuário com esse nome");

            return usuarios;
        }

        public async Task<IEnumerable<Usuario>> GetByTelefone(string telefone)
        {
            var usuarios = await _repositorio.GetByTelefone(Regex.Replace(telefone, "[^0-9]", ""));

            if (!usuarios.Any())
                throw new ArgumentException("Não há nenhum usuário com esse telefone");

            return usuarios;
        }

        public async Task LogicalDeleteByIdAsync(Guid id)
        {
            var usuario = await _repositorio.GetByIdAsync(id);

            if (usuario == null || usuario.Situacao == SituacaoPessoaEnum.INATIVO)
                throw new ArgumentException("Não há nenhum usuário com esse ID ou ele já foi deletado");

            await _repositorio.LogicalDeleteByIdAsync(usuario);
        }

        public async Task<Usuario> UpdateAssync(Usuario usuario)
        {
            var old = await _repositorio.GetByIdAsync(usuario.Id);

            if (old.Pessoa.Documento != "N/D" && old.Pessoa.Documento != usuario.Pessoa.Documento)
                throw new ArgumentException("Não é possível alterar o documento de usuário");

            if (old.Admissao != DateOnly.MinValue && old.Admissao != usuario.Admissao)
                throw new ArgumentException("Não é possível alterar a data de admissão");

            if (!await _repositorio.ValidateUniqueness(usuario))
                throw new ArgumentException("A documentação, e-mail, inscrição municipal, login ou inscrição estadual" +
                " do usuário já foram cadastrados, ou há outro registro ativo de usuário para a mesma pessoa");

            old.Atualizar(usuario);

            await _repositorio.UpdateAsync(old);

            return usuario;
        }

        public async Task<UsuarioReadDTO> BuildDTO(Usuario usuario)
        {
            UsuarioReadDTO dto = new UsuarioReadDTO();
            dto.Id = usuario.Id.ToString();
            dto.IdPessoa = usuario.Pessoa.Id.ToString();
            dto.CodigoPessoa = usuario.Pessoa.Codigo;
            dto.Nome = usuario.Pessoa.Nome;
            dto.Telefone = usuario.Pessoa.Telefone;
            dto.Documento = usuario.Pessoa.Documento;
            dto.Tipo = (int)usuario.Pessoa.Tipo;
            dto.SituacaoPessoa = (int)usuario.Pessoa.Situacao;
            dto.Email = usuario.Pessoa.Email;
            dto.InscricaoMunicipal = usuario.Pessoa.InscricaoMunicipal;
            dto.InscricaoEstadual = usuario.Pessoa.InscricaoEstadual;
            dto.CEP = usuario.Pessoa.CEP;
            dto.Logradouro = usuario.Pessoa.Logradouro;
            dto.Complemento = usuario.Pessoa.Complemento;
            dto.Cidade = usuario.Pessoa.Cidade;
            dto.Bairro = usuario.Pessoa.Bairro;
            dto.Numero = usuario.Pessoa.Numero;
            dto.Estado = (int)usuario.Pessoa.Estado;
            dto.SituacaoUsuario = (int)usuario.Situacao;
            dto.Salario = usuario.Salario;
            dto.HorarioInicioCargaHoraria = usuario.HorarioInicioCargaHoraria.ToString();
            dto.HorarioFimCargaHoraria = usuario.HorarioFimCargaHoraria.ToString();
            dto.Admissao = usuario.Admissao.ToString();
            dto.Cargo = (int)usuario.Cargo;
            dto.CodigoUsuario = usuario.Codigo;

            return dto;
        }

        public async Task<IEnumerable<UsuarioReadDTO>> BuildDTOList(IEnumerable<Usuario> usuarios)
        {
            ICollection<UsuarioReadDTO> dtos = [];

            foreach (Usuario usuario in usuarios)
            {
                UsuarioReadDTO dto = new UsuarioReadDTO();
                dto.Id = usuario.Id.ToString();
                dto.IdPessoa = usuario.Pessoa.Id.ToString();
                dto.CodigoPessoa = usuario.Pessoa.Codigo;
                dto.Nome = usuario.Pessoa.Nome;
                dto.Telefone = usuario.Pessoa.Telefone;
                dto.Documento = usuario.Pessoa.Documento;
                dto.Tipo = (int)usuario.Pessoa.Tipo;
                dto.SituacaoPessoa = (int)usuario.Pessoa.Situacao;
                dto.Email = usuario.Pessoa.Email;
                dto.InscricaoMunicipal = usuario.Pessoa.InscricaoMunicipal;
                dto.InscricaoEstadual = usuario.Pessoa.InscricaoEstadual;
                dto.CEP = usuario.Pessoa.CEP;
                dto.Logradouro = usuario.Pessoa.Logradouro;
                dto.Complemento = usuario.Pessoa.Complemento;
                dto.Cidade = usuario.Pessoa.Cidade;
                dto.Bairro = usuario.Pessoa.Bairro;
                dto.Numero = usuario.Pessoa.Numero;
                dto.Estado = (int)usuario.Pessoa.Estado;
                dto.SituacaoUsuario = (int)usuario.Situacao;
                dto.Salario = usuario.Salario;
                dto.HorarioInicioCargaHoraria = usuario.HorarioInicioCargaHoraria.ToString();
                dto.HorarioFimCargaHoraria = usuario.HorarioFimCargaHoraria.ToString();
                dto.Admissao = usuario.Admissao.ToString();
                dto.Cargo = (int)usuario.Cargo;
                dto.CodigoUsuario = usuario.Codigo;

                dtos.Add(dto);
            }

            return dtos;
        }
    }
}