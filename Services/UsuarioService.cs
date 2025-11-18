using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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

            usuario.Codigo = _repositorio.GetGreaterCodeNumber() + 1;
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

        public async Task<Usuario> GetByIdAssync(Guid id)
        {
            var usuario = await _repositorio.GetByIdAsync(id);

            if (usuario.Situacao != SituacaoPessoaEnum.ATIVO || usuario == null)
                throw new ArgumentException("Não há nenhum Usuário com esse ID");

            return usuario;
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

        public async Task<IEnumerable<UsuarioMiniReadDTO>> BuildDTOList(IEnumerable<Usuario> usuarios)
        {
            ICollection<UsuarioMiniReadDTO> dtos = [];

            foreach (Usuario usuario in usuarios)
            {
                UsuarioMiniReadDTO dto = new UsuarioMiniReadDTO();
                dto.Id = usuario.Id.ToString();
                dto.Nome = usuario.Pessoa.Nome;
                dto.Codigo = usuario.Codigo;
                dto.Cargo = (int)usuario.Cargo;
                dtos.Add(dto);
            }

            return dtos;
        }

        public async Task<bool> PostLogin(UsuarioPostLoginDTO dto)
        {
            return await _repositorio.ValidateLogin(dto.Login, GerarSHA256(dto.Senha));
        }

        private string GerarSHA256(string texto)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytesTexto = Encoding.UTF8.GetBytes(texto);
                byte[] hashBytes = sha256.ComputeHash(bytesTexto);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                    sb.Append(b.ToString("x2"));

                return sb.ToString();
            }
        }

        public async Task<IEnumerable<Usuario>> GetBySearch(UsuarioSearchDTO dto)
        {
            dto.Documento = Regex.Replace(dto.Documento, "[^0-9]", "");
            dto.Telefone = Regex.Replace(dto.Telefone, "[^0-9]", "");
            dto.Nome = dto.Nome.ToUpper();

            var usuarios = await _repositorio.GetBySearch(dto);

            if(!usuarios.Any())
                throw new ArgumentException("Não há nenhum usuário compatível com as descrições de busca");

            return usuarios;
        }
    }
}