using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueMoon.Entities.Enuns;

namespace BlueMoon.Entities.Models.Solids
{
    public class FuncionarioPessoaJuridica : AbstractUsuarioJuridico
    {
        public FuncionarioPessoaJuridica
        (
            ICollection<Telefone> telefones,
            string email,
            Endereco endereco,
            string login,
            string userName,
            string senha,
            CargoUsuarioEnum cargo,
            string cnpj,
            string nomeFantasia,
            string razaoSocial,
            string inscricaoEstadual,
            string inscricaoMunicipal,
            string cnae
        )
            : base(telefones, email, endereco, login, userName, senha, cargo, cnpj, nomeFantasia, razaoSocial, inscricaoEstadual, inscricaoMunicipal, cnae)
        {
        }

        public string CPF { get; private set; }
        public string Nome { get; private set; }

        
        
    }
}