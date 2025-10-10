using BlueMoon.Entities.Enuns;

namespace BlueMoon.Entities.Models
{
    public abstract class AbstractUsuarioFisico : AbstractUsuario
    {
        public string CPF { get; private set; } = string.Empty;
        public string Nome { get; private set; } = string.Empty;

        protected AbstractUsuarioFisico
        (
            ICollection<Telefone> telefones,
            string email,
            Endereco endereco,
            string login,
            string userName,
            string senha,
            string cpf,
            string nome,
            CargoUsuarioEnum cargo
        ) : base(telefones, email, endereco, login, userName, senha, cargo)
        {
            CPF = cpf;
            Nome = nome;
        }
    }
}