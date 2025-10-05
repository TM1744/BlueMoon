using BlueMoon.Models.Enuns;
using Microsoft.Net.Http.Headers;

namespace BlueMoon.Models;

public sealed class Usuario : Pessoa
{
    public string Login { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public CargoUsuarioEnum Cargo { get; set; }
    public decimal Salario { get; set; } = decimal.Zero;
    public DateOnly Admissao { get; private set; }
    public TimeOnly HorarioInicioCargaHoraria { get; set; }
    public TimeOnly HorarioFimCargaHoraria { get; set; }

    public Usuario(
        string? login,
        string senha,
        CargoUsuarioEnum cargo,
        decimal salario,
        DateOnly admissao,
        TimeOnly horarioInicio,
        TimeOnly horarioFim,
        TipoPessoaEnum tipo,
        SituacaoPessoaEnum situacao,
        List<Telefone> telefones,
        string email,
        string nome,
        string cpf,
        Endereco endereco
    ) : base(
        tipo, situacao, telefones, email, nome, cpf, endereco
    )
    {
        Login = login ?? Email;
        Senha = senha;
        Cargo = cargo;
        Salario = salario;
        Admissao = admissao;
        HorarioInicioCargaHoraria = horarioInicio;
        HorarioFimCargaHoraria = horarioFim;
    }

}