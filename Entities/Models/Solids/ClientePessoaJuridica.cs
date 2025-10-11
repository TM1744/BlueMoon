using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueMoon.Entities.Models.Solids
{
    public sealed class ClientePessoaJuridica : AbstractPessoa
    {
        public string CNPJ { get; private set; } = string.Empty;
        public string NomeFantasia { get; private set; } = string.Empty;
        public string RazaoSocial { get; private set; } = string.Empty;
        public string InscricaoEstadual { get; private set; } = string.Empty;
        public string InscricaoMunicipal { get; private set; } = string.Empty;
        public string CNAE { get; private set; } = string.Empty;

        public ClientePessoaJuridica
        (
            ICollection<Telefone> telefones,
            string email,
            Endereco endereco,
            string cnpj,
            string nomeFantasia,
            string razaoSocial,
            string inscricaoEstadual,
            string inscricaoMunicipal,
            string cnae
        )
            : base(telefones, email, endereco)
        {
            CNPJ = cnpj;
            NomeFantasia = nomeFantasia;
            RazaoSocial = razaoSocial;
            InscricaoEstadual = inscricaoEstadual;
            InscricaoMunicipal = inscricaoMunicipal;
            CNAE = cnae;
        }
    }
}