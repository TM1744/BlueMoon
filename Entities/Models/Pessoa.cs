using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using BlueMoon.DTO;
using BlueMoon.Entities.Enuns;
using Microsoft.AspNetCore.Diagnostics;
namespace BlueMoon.Entities.Models
{
    public class Pessoa
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public TipoPessoaEnum Tipo { get; private set; }
        public SituacaoPessoaEnum Situacao { get; private set; }
        public int Codigo { get; set; }
        public string Telefone { get; set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Nome { get; private set; } = string.Empty;
        public string Documento { get; private set; } = string.Empty;
        public string InscricaoMunicipal { get; private set; } = string.Empty;
        public string InscricaoEstadual { get; private set; } = string.Empty;
        public string CEP { get; set; } = string.Empty;
        public string Logradouro { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string Complemento { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public EstadoEnum Estado { get; set; }

        private Pessoa(){}
        public Pessoa(PessoaCreateDTO dto)
        {
            Tipo = (TipoPessoaEnum)dto.Tipo;
            Situacao = SituacaoPessoaEnum.ATIVO;
            Telefone = NotEmptyString(dto.Telefone);
            Email = NotEmptyString(dto.Email);
            Nome = NotEmptyString(dto.Nome);
            Documento = SequenceNumberString(dto.Documento);
            InscricaoMunicipal = SequenceNumberString(dto.InscricaoMunicipal);
            InscricaoEstadual = SequenceNumberString(dto.InscricaoEstadual);
            CEP = SequenceNumberString(dto.CEP);
            Logradouro = NotEmptyString(dto.Logradouro);
            Numero = NotEmptyString(dto.Numero);
            Complemento = NotEmptyString(dto.Complemento);
            Bairro = NotEmptyString(dto.Bairro);
            Cidade = NotEmptyString(dto.Cidade);
            Estado = (EstadoEnum)dto.Estado;             
        }

        public Pessoa(PessoaUpdateDTO dto)
        {
            Id = Guid.Parse(dto.Id);
            Situacao = (SituacaoPessoaEnum)dto.Situacao;
            Telefone = NotEmptyString(dto.Telefone);
            Nome = NotEmptyString(dto.Nome);
            Email = NotEmptyString(dto.Email);
            Documento = SequenceNumberString(dto.Documento);
            InscricaoMunicipal = SequenceNumberString(dto.InscricaoMunicipal);
            InscricaoEstadual = SequenceNumberString(dto.InscricaoEstadual);
            CEP = SequenceNumberString(dto.CEP);
            Logradouro = NotEmptyString(dto.Logradouro);
            Numero = NotEmptyString(dto.Numero);
            Complemento = NotEmptyString(dto.Complemento);
            Bairro = NotEmptyString(dto.Bairro);
            Cidade = NotEmptyString(dto.Cidade);
            Estado = (EstadoEnum)dto.Estado;
        }
        
        public void Atualizar(Pessoa pessoa)
        {
            Id = pessoa.Id;
            Situacao = pessoa.Situacao;
            Telefone = pessoa.Telefone;
            Nome = pessoa.Nome;
            Email = pessoa.Email;
            Documento = pessoa.Documento;
            InscricaoMunicipal = pessoa.InscricaoMunicipal;
            InscricaoEstadual = pessoa.InscricaoEstadual;
            CEP = pessoa.CEP;
            Logradouro = pessoa.Logradouro;
            Numero = pessoa.Numero;
            Complemento = pessoa.Complemento;
            Bairro = pessoa.Bairro;
            Cidade = pessoa.Cidade;
            Estado = pessoa.Estado;
        }

        public void Inativar() => Situacao = SituacaoPessoaEnum.INATIVO;
        
        private string NotEmptyString(string str)
        {
            if (str == null || str.Trim() == "")
                return "N/D";

            return str.ToUpper();
        }
        private string SequenceNumberString(string str)
        {
            if (str == null || str.Trim() == "")
                return "N/D";

            str = Regex.Replace(str, "[^0-9]", "");
            return str.ToUpper();
        }
    }
}