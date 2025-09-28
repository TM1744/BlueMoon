using BlueMoon.Models.Enuns;

namespace BlueMoon.Models;

public sealed class Usuario : Pessoa
{
    private string _login = string.Empty;
    private string _senha = string.Empty;
    private decimal _salario = decimal.Zero;
    private DateOnly _admissao;
    private TimeOnly _horarioInicioCargaHoraria;
    private TimeOnly _horarioFimCargaHoraria;
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Login
    {
        get { return _login; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Login não pode ser nulo ou vazio");
            }
            if (value.Contains(" "))
            {
                throw new ArgumentException("Login não pode conter espaço vazio");
            }

            _login = value;
        }
    }
    public string Senha
    {
        get { return _senha; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Senha não pode ser nula ou vazia");
            }
            if (!ValidarSenha(value))
            {
                throw new ArgumentException("Senha não segue o padrão de segurança");
            }

            _senha = value;
        }
    }
    public decimal Salario
    {
        get { return _salario; }
        set
        {
            if (decimal.IsNegative(value))
            {
                throw new ArgumentException("Salario não pode ser negativo");
            }
            if (value == 0m)
            {
                throw new ArgumentException("Salário não pode ser R$0,00");
            }

            _salario = value;
        }
    }
    public DateOnly Admissao
    {
        get { return _admissao; }
        set
        {
            if (value.Equals(null))
            {
                throw new ArgumentNullException("Data de admissão não pode ser nula");
            }

            _admissao = value;
        }
    }

    public TimeOnly HorarioInicioCargaHoraria
    {
        get { return _horarioInicioCargaHoraria; }
        set
        {
            if (value.Equals(null))
            {
                throw new ArgumentNullException("Horário de início de carga horária não pode ser nulo");
            }

            _horarioInicioCargaHoraria = value;
        }
    }
    public TimeOnly HorarioFimCargaHoraria
    {
        get { return _horarioFimCargaHoraria; }
        set
        {
            if (value.Equals(null))
            {
                throw new ArgumentNullException("Horário de início de carga horária não pode ser nulo");
            }

            _horarioInicioCargaHoraria = value;
        }
    }

    public CargoUsuarioEnum Cargo { get; set; }

    private bool ValidarSenha(string senha)
    {
        if (string.IsNullOrWhiteSpace(senha))
            return false;

        if (senha.Length < 8)
            return false;

        if (senha.Count(char.IsUpper) < 2)
            return false;

        if (senha.Count(char.IsLower) < 2)
            return false;

        if (senha.Count(char.IsDigit) < 2)
            return false;

        if (!senha.Any(c => "!@#$%^&*()-_=+[{]}|;:',<.>/?`~\"\\".Contains(c)))
            return false;

        if (senha.Any(char.IsWhiteSpace))
            return false;

        return true;
    }

    private Usuario() { }

    public Usuario(string login, string senha, decimal salario, DateOnly admissao, TimeOnly horarioIncioCargaHoraria, TimeOnly horarioFimCargaHoraria)
        : base ()
    {
        
    }

}