using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueMoon.Entities.Enuns;
using BlueMoon.DTO.PessoaDTO.PFDTO;

namespace BlueMoon.DTO.UsuarioDTO
{
    // Classe DTO para receber valores necessários para a apresentação de um Usuario
    public class UsuarioReadDTO : PFReadDTO
    {
        public CargoUsuarioEnum Cargo { get; set; }
        public decimal Salario { get; set; }
        public DateOnly Admissao { get; set; }
        public TimeOnly HorarioInicioCargaHoraria { get; set; }
        public TimeOnly HoararioFimCargaHoraria { get; set; }
    }
}