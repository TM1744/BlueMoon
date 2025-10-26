using System.Text.RegularExpressions;
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
            CEP = SequenceNumberString(create.CEP);
            Logradouro = NotEmptyString(create.Logradouro);
            Numero = NotEmptyString(create.Numero);
            Complemento = NotEmptyString(create.Complemento);
            Bairro = NotEmptyString(create.Bairro);
            Cidade = NotEmptyString(create.Cidade);
            Estado = (EstadoEnum)create.Estado;
        }

        public Endereco(EnderecoUpdateDTO update)
        {
            Id = Guid.Parse(update.Id);
            CEP = SequenceNumberString(update.CEP);
            Logradouro = NotEmptyString(update.Logradouro);
            Numero = NotEmptyString(update.Numero);
            Complemento = NotEmptyString(update.Complemento);
            Bairro = NotEmptyString(update.Bairro);
            Cidade = NotEmptyString(update.Cidade);
            Estado = (EstadoEnum)update.Estado;
        }

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