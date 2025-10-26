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
        public ICollection<Telefone> Telefones { get; private set; } = [];
        public string Email { get; private set; } = string.Empty;
        public string Nome { get; private set; } = string.Empty;
        public string Documento { get; private set; } = string.Empty;
        public string InscricaoMunicipal { get; private set; } = string.Empty;
        public string InscricaoEstadual { get; private set; } = string.Empty;
        public Endereco Endereco { get; private set; }

        private Pessoa(){}
        public Pessoa(PessoaCreateDTO create)
        {
            Tipo = (TipoPessoaEnum)create.Tipo;
            Situacao = SituacaoPessoaEnum.ATIVO;
            Email = NotEmptyString(create.Email);
            Nome = NotEmptyString(create.Nome);
            Documento = SequenceNumberString(create.Documento);
            InscricaoMunicipal = SequenceNumberString(create.InscricaoMunicipal);
            InscricaoEstadual = SequenceNumberString(create.InscricaoEstadual);
            Endereco = new Endereco(create.Endereco);


            foreach (TelefoneCreateDTO telefone in create.Telefones)
                AdicionarTelefone(new Telefone(telefone));                

        }
        
        public Pessoa(PessoaUpdateDTO update)
        {
            Id = Guid.Parse(update.Id);
            Situacao = (SituacaoPessoaEnum)update.Situacao;
            Nome = NotEmptyString(update.Nome);
            Email = NotEmptyString(update.Email);
            Documento = SequenceNumberString(update.Documento);
            InscricaoMunicipal = SequenceNumberString(update.InscricaoMunicipal);
            InscricaoEstadual = SequenceNumberString(update.InscricaoEstadual);
            Endereco = new Endereco(update.Endereco);

            foreach (TelefoneUpdateDTO telefone in update.Telefones)
                AdicionarTelefone(new Telefone(telefone));
        }

        public void AdicionarTelefone(Telefone telefone)
        {
            if (Telefones.Any(t => t.Numero.Equals(telefone.Numero)))
                throw new InvalidOperationException("Telefone já existe.");
            Telefones.Add(telefone);
        }
        public void RetirarTelefone(Telefone telefone)
        {
            if (!Telefones.Any(t => t.Numero.Equals(telefone.Numero)))
                throw new InvalidOperationException("Telefone não existe.");
            Telefones.Remove(telefone);
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