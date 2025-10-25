using System.Data;
using System.Text.RegularExpressions;
using BlueMoon.DTO;
using BlueMoon.Entities.Models;
using FluentValidation;

namespace BlueMoon.Validations
{
    public class ProdutoCreateDTOValidator : AbstractValidator<ProdutoCreateDTO>
    {
        public ProdutoCreateDTOValidator()
        {
            RuleFor(dto => dto.Descricao)
                .NotEmpty().WithMessage("Descrição do produto é obrigatória")
                .MaximumLength(70).WithMessage("Descrição não deve ter mais de 70 caracteres");

            RuleFor(dto => dto.Marca)
                .MaximumLength(50).WithMessage("Marca não deve ter mais de 50 caracteres");

            RuleFor(dto => dto.QuantidadeEstoque)
                .NotNull().WithMessage("Quantidade em estoque é obrigatório")
                .GreaterThanOrEqualTo(0).WithMessage("Quantidade em estoque deve ser maior ou igual a zero");

            RuleFor(dto => dto.QuantidadeEstoqueMinimo)
                .GreaterThanOrEqualTo(0).WithMessage("Quantidade mínima de estoque deve ser maior do que zero");

            RuleFor(dto => dto.NCM)
                .Must(Validacoes.NcmValido).WithMessage("NCM informado não é válido");

            RuleFor(dto => dto.CodigoBarras)
                .Must(Validacoes.CodigoBarrasValido).WithMessage("Codigo de barras informado não é válido");

            RuleFor(dto => dto.ValorCusto)
                .GreaterThanOrEqualTo(0.00m).WithMessage("Valor de custo deve ser maior ou igual a zero");

            RuleFor(dto => dto.ValorVenda)
                .NotNull().WithMessage("Valor de venda é obrigatório")
                .GreaterThan(0.00m).WithMessage("Valor de venda deve ser maior do que zero");

            RuleFor(dto => dto.MargemLucro)
                .GreaterThanOrEqualTo(0.00m).WithMessage("Margem de lucro deve ser maior ou igual a zero");
        }

    }

    public class ProdutoUpdateDTOValidator : AbstractValidator<ProdutoUpdateDTO>
    {
        public ProdutoUpdateDTOValidator()
        {
            RuleFor(dto => dto.Id)
                .NotEmpty().WithMessage("Id do produto é obrigatório")
                .Length(36).WithMessage("Id do produto deve ter 36 caracteres")
                .Must(Validacoes.IdValido).WithMessage("Id do produto não é valido");

            RuleFor(dto => dto.Situacao)
                .NotEmpty().WithMessage("Situação do produto é obrigatório")
                .GreaterThanOrEqualTo(0).WithMessage("Situação do produto é inválida")
                .LessThanOrEqualTo(4).WithMessage("Situação do produto é inválida");

            RuleFor(dto => dto.Descricao)
                .NotEmpty().WithMessage("Descrição do produto é obrigatória")
                .MaximumLength(70).WithMessage("Descrição não deve ter mais de 70 caracteres");

            RuleFor(dto => dto.Marca)
                .MaximumLength(50).WithMessage("Marca não deve ter mais de 50 caracteres");

            RuleFor(dto => dto.QuantidadeEstoque)
                .NotNull().WithMessage("Quantidade em estoque é obrigatório")
                .GreaterThanOrEqualTo(0).WithMessage("Quantidade em estoque deve ser maior ou igual a zero");

            RuleFor(dto => dto.QuantidadeEstoqueMinimo)
                .GreaterThanOrEqualTo(0).WithMessage("Quantidade mínima de estoque deve ser maior do que zero");

            RuleFor(dto => dto.NCM)
                .Must(Validacoes.NcmValido).WithMessage("NCM informado não é válido");

            RuleFor(dto => dto.CodigoBarras)
                .Must(Validacoes.CodigoBarrasValido).WithMessage("Codigo de barras informado não é válido");

            RuleFor(dto => dto.ValorCusto)
                .GreaterThanOrEqualTo(0.00m).WithMessage("Valor de custo deve ser maior ou igual a zero");

            RuleFor(dto => dto.ValorVenda)
                .NotNull().WithMessage("Valor de venda é obrigatório")
                .GreaterThan(0.00m).WithMessage("Valor de venda deve ser maior do que zero");

            RuleFor(dto => dto.MargemLucro)
                .GreaterThanOrEqualTo(0.00m).WithMessage("Margem de lucro deve ser maior ou igual a zero");
        }
    }

    public sealed class Validacoes()
    {
        public static bool NcmValido(string ncm)
        {
            if (ncm == null || ncm.Trim() == "")
                return true;

            var numeros = Regex.Replace(ncm, "[^0-9]", "");
            if (numeros.Length != 8)
                return false;

            return numeros.All(char.IsDigit);
        }

        public static bool CodigoBarrasValido(string codigo)
        {
            if (codigo == null || codigo.Trim() == "")
                return true;

            if (codigo.Length >= 3 || codigo.Length <= 9)
                return ValidarCode39(codigo);

            if (codigo.Length >= 13)
            {
                codigo = Regex.Replace(codigo, "[^0-9]", "");
                return ValidarEAN(codigo);
            }

            return false;
        }

        private static bool ValidarEAN(string ean)
        {
            int soma = 0;

            for (int i = 0; i < 12; i++)
            {
                int digito = int.Parse(ean[i].ToString());
                soma += (i % 2 == 0) ? digito : digito * 3;
            }

            int digitoVerificador = (10 - (soma % 10)) % 10;
            return digitoVerificador == int.Parse(ean[12].ToString());
        }

        private static bool ValidarCode39(string code39)
        {
            const string caracteresValidos = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. $/+%*";

            foreach (char c in code39)
            {
                if (!caracteresValidos.Contains(c))
                    return false;
            }

            return true;
        }

        public static bool IdValido (string id)
        {
            return Guid.TryParse(id, out Guid result);
        }
    }
}