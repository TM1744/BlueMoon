namespace BlueMoon.Entities.Models
{
    public sealed class ClientePessoaFisica : AbstractPessoa
    {
        public string CPF { get; private set; } = string.Empty;
        public string Nome { get; private set; } = string.Empty;

        public ClientePessoaFisica
        (
            ICollection<Telefone> telefones,
            string email,
            Endereco endereco,
            string cpf,
            string nome
        )
            : base(telefones, email, endereco)
        {
            CPF = cpf;
            Nome = nome;
        }
    }
}