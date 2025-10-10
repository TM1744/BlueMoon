using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueMoon.Entities.Enuns;

namespace BlueMoon.DTO.TelefoneDTO
{
    // Classe DTO para receber valores necessários para a apresentação de um Telefone
    public class TelefoneReadDTO
    {
        public Guid Id { get; set; }
        public DddEnum DDD { get; set; }
        public string Numero { get; set; }
    }
}