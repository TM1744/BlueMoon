using BlueMoon.DTO;
using BlueMoon.Entities.Enuns;
namespace BlueMoon.Entities.Models
{
    public sealed class Endereco
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string CEP { get; private set; } = string.Empty;
        public string Logradouro { get; private set; } = string.Empty;
        public string Numero { get; private set; } = string.Empty;
        public string Complemento { get; private set; } = string.Empty;
        public string Bairro { get; private set; } = string.Empty;
        public string Cidade { get; private set; } = string.Empty;
        public EstadoEnum Estado { get; private set; } = EstadoEnum.INDEFINIDO;

        private Endereco() { }

        public Endereco(EnderecoCreateDTO create)
        {
            if (create.Logradouro.Trim().Equals(""))
                create.Logradouro = "SEM COMPLEMENTO";

            CEP = create.CEP;
            Logradouro = create.Logradouro;
            Numero = create.Logradouro;
            Complemento = create.Logradouro;
            Bairro = create.Logradouro;
            Cidade = create.Cidade;
            Estado = (EstadoEnum)create.Estado;
        }

        public Endereco(EnderecoUpdateDTO update)
        {
            if (update.Logradouro.Trim().Equals(""))
                update.Logradouro = "SEM COMPLEMENTO";

            Id = Guid.Parse(update.Id);
            CEP = update.CEP;
            Logradouro = update.Logradouro;
            Numero = update.Logradouro;
            Complemento = update.Logradouro;
            Bairro = update.Logradouro;
            Cidade = update.Cidade;
            Estado = (EstadoEnum)update.Estado;
        }
    }
}