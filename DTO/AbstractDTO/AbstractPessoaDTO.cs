using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueMoon.DTO.EnderecoDTO;
using BlueMoon.DTO.TelefoneDTO;
using BlueMoon.Entities.Enuns;

namespace BlueMoon.DTO.AbstractDTO
{
    public class AbstractPessoaCreateDTO
    {
        public ICollection<TelefoneCreateDTO> Telefones { get; set; }
        public string Email { get; set; }
        public EnderecoCreateDTO Endereco { get; set; }
    }

    public class AbstractPessoaReadDTO
    {
        public Guid Id { get; set; }
        public SituacaoPessoaEnum Situacao { get; set; }
        public int Codigo { get; set; }
        public ICollection<TelefoneReadDTO> Telefones { get; set; }
        public string Email { get; set; }
        public EnderecoReadDTO Endereco { get; set; }
    }

    public class AbstractPessoaUpdateDTO
    {
        public ICollection<TelefoneUpdateDTO> Telefones { get; set; }
        public string Email { get; set; }
        public EnderecoUpdateDTO Endereco { get; set; }
        public SituacaoPessoaEnum Situacao { get; set; }
    }

    public class AbstractPessoaDeleteDTO
    {
        public Guid Id { get; set; }
    }
}