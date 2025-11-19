using System.Globalization;
using System.Net.Mail;
using System.Text.RegularExpressions;
using BlueMoon.DTO;
using FluentValidation;

namespace BlueMoon.Validations
{
    public class UsuarioDTOValidator
    {
        public class UsuarioCreateDTOValidator : AbstractValidator<UsuarioCreateDTO>
        {
            public UsuarioCreateDTOValidator()
            {
                RuleFor(dto => dto.IdPessoa)
                    .NotEmpty().WithMessage("ID de Pessoa é obrigatório")
                    .Must(Validacoes.IdValido).WithMessage("ID de Pessoa não é válido");

                RuleFor(dto => dto.Login)
                    .NotEmpty().WithMessage("Login é obrigatório")
                    .MaximumLength(100).WithMessage("Login não deve ter mais de 100 caracteres")
                    .Must(Validacoes.EmailValido).WithMessage("Login deve ser um e-mail válido");

                RuleFor(dto => dto.Senha)
                    .NotEmpty().WithMessage("Senha é obrigatória")
                    .MaximumLength(64).WithMessage("Senha não deve ter mais de 64 caracteres")
                    .Must(Validacoes.SenhaValida).WithMessage("Senha não é válida");

                RuleFor(dto => dto.Cargo)
                    .NotEmpty().WithMessage("Cargo é obrigatório")
                    .GreaterThan(0).WithMessage("Cargo deve ter um valor de 1 a 4")
                    .LessThan(5).WithMessage("Cargo deve ter um valor de 1 a 4");

                RuleFor(dto => dto.Salario)
                    .GreaterThanOrEqualTo(0.00m).WithMessage("Salário deve ser maior ou igual a zero");

                RuleFor(dto => dto.Admissao)
                    .Must(Validacoes.DataValida).WithMessage("Data de Admissão não é válida");

                RuleFor(dto => dto.HorarioInicioCargaHoraria)
                    .Must(Validacoes.TempoValido).WithMessage("Horaio de Inicio de Carga Horária não é válido");

                RuleFor(dto => dto.HorarioFimCargaHoraria)
                    .Must(Validacoes.TempoValido).WithMessage("Horaio de Inicio de Carga Horária não é válido");
            }
        }

        public class UsuarioUpdateDTOValidator : AbstractValidator<UsuarioUpdateDTO>
        {
            public UsuarioUpdateDTOValidator()
            {
                RuleFor(dto => dto.Id)
                    .NotEmpty().WithMessage("ID de Usuário é obrigatório")
                    .Must(Validacoes.IdValido).WithMessage("ID de Usuário não é válido");

                RuleFor(dto => dto.IdPessoa)
                    .NotEmpty().WithMessage("ID de Pessoa é obrigatório")
                    .Must(Validacoes.IdValido).WithMessage("ID de Pessoa não é válido");

                RuleFor(dto => dto.Situacao)
                    .LessThan(3).WithMessage("Situação deve ter um valor de 1 ou 2")
                    .GreaterThan(0).WithMessage("Situação deve ter um valor 1 ou 2");

                RuleFor(dto => dto.Cargo)
                    .NotEmpty().WithMessage("Cargo é obrigatório")
                    .GreaterThan(0).WithMessage("Cargo deve ter um valor de 1 a 4")
                    .LessThan(5).WithMessage("Cargo deve ter um valor de 1 a 4");

                RuleFor(dto => dto.Salario)
                    .GreaterThanOrEqualTo(0.00m).WithMessage("Salário deve ser maior ou igual a zero");

                RuleFor(dto => dto.Admissao)
                    .Must(Validacoes.DataValida).WithMessage("Data de Admissão não é válida");

                RuleFor(dto => dto.HorarioInicioCargaHoraria)
                    .Must(Validacoes.TempoValido).WithMessage("Horaio de Inicio de Carga Horária não é válido");

                RuleFor(dto => dto.HorarioFimCargaHoraria)
                    .Must(Validacoes.TempoValido).WithMessage("Horaio de Inicio de Carga Horária não é válido");
            }
        }

        public sealed class Validacoes()
        {
            public static bool IdValido(string id)
            {
                return Guid.TryParse(id, out Guid result);
            }

            public static bool SenhaValida(string senha)
            {
                bool tamanhoValido = senha.Length >= 8;
                bool temMaiuscula = Regex.IsMatch(senha, "[A-Z]");
                bool temMinuscula = Regex.IsMatch(senha, "[a-z]");
                bool temNumero = Regex.IsMatch(senha, "[0-9]");
                bool temEspecial = Regex.IsMatch(senha, "[^a-zA-Z0-9]");

                return tamanhoValido && temMaiuscula &&
                temMinuscula && temNumero &&
                temEspecial;
            }

            public static bool DataValida(string data)
            {
                if (data == null || data == "")
                    return true;

                data = Regex.Replace(data, "[^0-9]", "");
                return DateOnly.TryParseExact(data, "ddMMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
            }

            public static bool TempoValido(string tempo)
            {
                if (tempo == null || tempo == "")
                    return true;

                tempo = Regex.Replace(tempo, "[^0-9]", "");
                return TimeOnly.TryParseExact(tempo, "HHmm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
            }

            public static bool EmailValido(string email)
            {
                if (email == null || email.Trim() == "")
                    return true;

                email = email.Trim();

                string modelo = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                if (!Regex.IsMatch(email, modelo))
                    return false;

                try
                {
                    var mail = new MailAddress(email);
                    return mail.Address == email;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}