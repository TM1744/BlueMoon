using BlueMoon.Entities.Models;

namespace BlueMoon.DTO
{
    public class PessoaCreateDTO
    {
        public int Tipo { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty;
        public string InscricaoMunicipal { get; set; } = string.Empty;
        public string InscricaoEstadual { get; set; } = string.Empty;
        public string CEP { get; set; } = string.Empty;
        public string Logradouro { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string Complemento { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public int Estado { get; set; }
    }

    public class PessoaUpdateDTO
    {
        public string Id { get; set; } = string.Empty;
        public int Situacao { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty;
        public string InscricaoMunicipal { get; set; } = string.Empty;
        public string InscricaoEstadual { get; set; } = string.Empty;
        public string CEP { get; set; } = string.Empty;
        public string Logradouro { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string Complemento { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public int Estado { get; set; }
    }

    public class PessoaReadDTO
    {
        public string Id { get; set; } = string.Empty;
        public int Codigo { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty;
        public int Tipo { get; set; }
        public int Situacao { get; set; }
        public string Telefone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string InscricaoMunicipal { get; set; } = string.Empty;
        public string InscricaoEstadual { get; set; } = string.Empty;
        public string CEP { get; set; } = string.Empty;
        public string Logradouro { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string Complemento { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public int Estado { get; set; }
    }
}