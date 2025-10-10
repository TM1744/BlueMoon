using BlueMoon.DTO.PessoaDTO.PFDTO;
using BlueMoon.Entities.Enuns;

namespace BlueMoon.DTO.UsuarioDTO
{
    // Classe DTO para receber valores necessários para a criação de um Usuario
    public class UsuarioCreateDTO : PFCreateDTO
    {
        public string Login { get; set; }
        public string Senha { get; set; }
        public CargoUsuarioEnum Cargo { get; set; }
        public decimal Salario { get; set; }
        public DateOnly Admissao { get; set; }
        public TimeOnly HorarioInicioCargaHoraria { get; set; }
        public TimeOnly HorarioFimCargaHoraria { get; set; }
    }
}