using BlueMoon.Models.Modelling;
using BlueMoon.Models.Enuns;

namespace BlueMoon.DTO.PessoaDTO.PFDTO
{
    //Classe DTO para receber valores necessários para a criação de uma Pessoa Física
    public class PFCreateDTO
    {
        public TipoPessoaEnum Tipo { get; set; }
        public string? Email { get; set; }
        public ICollection<Telefone> Telefones { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public Endereco Endereco { get; set; }

    }
}