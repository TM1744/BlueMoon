using BlueMoon.Entities.Enuns;
using BlueMoon.DTO.TelefoneDTO;
using BlueMoon.DTO.EnderecoDTO;

namespace BlueMoon.DTO.PessoaDTO.PJDTO
{
    public class PJUpdateDTO
    {
        //Classe DTO para receber valores necessários para atualização de uma Pessoa Jurídica

        public TipoPessoaEnum Tipo { get; set; }
        public ICollection<TelefoneUpdateDTO> Telefones { get; set; }
        public string? Email { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public EnderecoUpdateDTO Endereco { get; set; }
    }
}