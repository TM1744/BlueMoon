// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using BlueMoon.Entities.Enuns;
// using BlueMoon.DTO.TelefoneDTO;
// using BlueMoon.DTO.EnderecoDTO;

// namespace BlueMoon.DTO.PessoaDTO.PJDTO
// {
//     //Classe DTO para receber valores necessários para a apresentação de uma Pessoa Jurídica

//     public interface PJRead
//     {
//         public Guid Id { get; set; }
//         public TipoPessoaEnum Tipo { get; set; }
//         public SituacaoPessoaEnum Situacao { get; set; }
//         public int Codigo { get; set; }
//         public ICollection<TelefoneReadDTO> Telefones { get; set; }
//         public string Email { get; set; }
//         public string CNPJ { get; set; }
//         public string RazaoSocial { get; set; }
//         public string NomeFantasia { get; set; }
//         public string InscricaoMunicipal { get; set; }
//         public EnderecoReadDTO Endereco { get; set; }
//     }
// }