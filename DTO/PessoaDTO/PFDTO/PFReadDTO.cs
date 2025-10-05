using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueMoon.Models;
using BlueMoon.Models.Enuns;

namespace BlueMoon.DTO.PessoaDTO.PFDTO
{
    //Classe DTO para receber valores necessários para a apresentação de uma Pessoa Física
    public class PFReadDTO
    {
        public Guid Id { get; set; }
        public TipoPessoaEnum Tipo { get; set; }
        public SituacaoPessoaEnum Situacao { get; set; }
        public int Codigo { get; set; }
        public ICollection<Telefone> Telefones { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public Endereco endereco { get; set; }
    }
}