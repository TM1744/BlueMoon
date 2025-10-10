using BlueMoon.Entities.Enuns;

namespace BlueMoon.Entities.Models
{
    public abstract class AbstractUsuarioJuridico : AbstractUsuario
    {
        public string CNPJ { get; private set; } = string.Empty;
        public string NomeFantasia { get; private set; } = string.Empty;
        public string RazaoSocial { get; private set; } = string.Empty;
        public string InscricaoEstadual { get; private set; } = string.Empty;
        public string InscricaoMunicipal { get; private set; } = string.Empty;
        public string CNAE { get; private set; } = string.Empty;

        public AbstractUsuarioJuridico
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
            : base(telefones, email, endereco, login, userName, senha, cargo)
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