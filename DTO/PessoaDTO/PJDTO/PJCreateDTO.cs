using BlueMoon.Models.Modelling;
using BlueMoon.Models.Enuns;

namespace BlueMoon.DTO.PessoaDTO.PJDTO
{
    public class PJCreateDTO
    {
        //Classe DTO para receber valores necessários para a criação de uma Pessoa Jurídica
        public TipoPessoaEnum Tipo { get; set; }
        public string? Email { get; set; }
        public ICollection<Telefone> Telefones { get; set; }
        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string InscricaoMunicipal { get; set; }
        public Endereco Endereco { get; set; }
    }
}