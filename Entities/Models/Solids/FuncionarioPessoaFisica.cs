using BlueMoon.Entities.Enuns;

namespace BlueMoon.Entities.Models.Solids
{
    public class FuncionarioPessoaFisica : AbstractUsuarioFisico
    {
        public decimal Salario { get; private set; } = 0.00m;
        public DateOnly Admissao { get; private set; }
        public TimeOnly HorarioInicioCargaHoraria { get; private set; }
        public TimeOnly HorarioFimCargaHoraria { get; private set; }


        public FuncionarioPessoaFisica
        (
            ICollection<Telefone> telefones,
            string email,
            Endereco endereco,
            string login,
            string userName,
            string senha,
            string cpf,
            string nome,
            CargoUsuarioEnum cargo,
            decimal salario,
            DateOnly admissao,
            TimeOnly horarioInicioCargaHoraria,
            TimeOnly horarioFimCargaHoraria
        )
            : base(telefones, email, endereco, login, userName, senha, cpf, nome, cargo)
        {
            Salario = salario;
            Admissao = admissao;
            HorarioInicioCargaHoraria = horarioInicioCargaHoraria;
            HorarioFimCargaHoraria = horarioFimCargaHoraria;
        }
    }
}