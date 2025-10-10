using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueMoon.Entities.Enuns;

namespace BlueMoon.DTO.TelefoneDTO
{
    // Classe DTO para receber valores necessários para a atualização de um Telefone
    public class TelefoneUpdateDTO
    {
        public DddEnum DDD { get; set; }
        public string Numero { get; set; }
    }
}