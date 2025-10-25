namespace BlueMoon.DTO
{
    public class PessoaCreateDTO
    {
        public int Tipo { get; set; }
        public string Nome { get; set; } = string.Empty;
        public ICollection<TelefoneCreateDTO> Telefones { get; set; } = [];
        public string Email { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty;
        public string InscricaoMunicipal { get; set; } = string.Empty;
        public string InscricaoEstadual { get; set; } = string.Empty;
        public EnderecoCreateDTO? Endereco { get; set; }
    }

    public class PessoaUpdateDTO
    {
        public string Id { get; set; } = string.Empty;
        public int Situacao { get; set; }
        public string Nome { get; set; } = string.Empty;
        public ICollection<TelefoneUpdateDTO> Telefones { get; set; } = [];
        public string Email { get; set; } = string.Empty;
        public EnderecoUpdateDTO? Endereco { get; set; }
    }

    public class PessoaReadDTO
    {
        public string Id { get; set; } = string.Empty;
        public int Codigo { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty;
        public int Tipo { get; set; }
        public int Situacao { get; set; }
        public ICollection<TelefoneReadDTO> Telefones { get; set; } = [];
        public string Email { get; set; } = string.Empty;
        public string InscricaoMunicipal { get; set; } = string.Empty;
        public string InscricaoEstadual { get; set; } = string.Empty;
        public EnderecoReadDTO? Endereco { get; set; }
    }
}