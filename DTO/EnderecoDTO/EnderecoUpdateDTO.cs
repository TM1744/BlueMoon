using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueMoon.Entities.Enuns;

namespace BlueMoon.DTO.EnderecoDTO
{
    //Classe DTO para receber valores necessários para a atualização de um Endereco

    public class EnderecoUpdateDTO
    {
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public EstadoEnum Estado { get; set; }
    }
}