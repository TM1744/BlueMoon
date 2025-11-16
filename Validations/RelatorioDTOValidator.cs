using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using BlueMoon.DTO;
using FluentValidation;
namespace BlueMoon.Validations
{
    public class RelatorioDTOValidator
    {
        public class PeriodoBuscaDTOValidator : AbstractValidator<PeriodoBuscaDTO>
        {
            public PeriodoBuscaDTOValidator()
            {
                RuleFor(dto => dto.DataInicio)
                    .NotEmpty().WithMessage("Data início de busca é obrigatória")
                    .Must(Validacoes.DataValida).WithMessage("Data inicio de busca informada não é válida");

                RuleFor(dto => dto.DataFim)
                    .NotEmpty().WithMessage("Data fim de busca é obrigatória")
                    .Must(Validacoes.DataValida).WithMessage("Data fim de busca informada é inválida");
            }
        }

        public sealed class Validacoes()
        {
            public static bool DataValida(string data)
            {
                if (data == null || data == "")
                    return true;

                data = Regex.Replace(data, "[^0-9]", "");
                return DateOnly.TryParseExact(data, "ddMMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
            }
        }
    }
}