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
                .NotEmpty().WithMessage("Descrição do produto é obrigatório")
                .MaximumLength(70).WithMessage("Descrição não deve ter mais de 70 caracteres");

            RuleFor(dto => dto.Marca)
                .MaximumLength(50).WithMessage("Marca não deve ter mais de 50 caracteres");

            RuleFor(dto => dto.QuantidadeEstoque)
                .NotNull().WithMessage("Quantidade em estoque não deve ser nula")
                .GreaterThan(0).WithMessage("Quantidade em estoque deve ser maior ou igual a zero");

            RuleFor(dto => dto.QuantidadeEstoqueMinimo)
                .NotNull().WithMessage("Quantidade mínima de estoque não deve ser nula")
                .GreaterThanOrEqualTo(0).WithMessage("Quantidade mínima de estoque deve ser maior do que zero");

            RuleFor(dto => dto.NCM)
                .Must(NcmValido).WithMessage("NCM informado não é válido");

            RuleFor(dto => dto.ValorCusto)
                .GreaterThanOrEqualTo(0.00m).WithMessage("Valor de custo deve ser maior ou igual a zero");

            RuleFor(dto => dto.ValorVenda)
                .GreaterThan(0.00m).WithMessage("Valor de venda deve ser maior do que zero");

            RuleFor(dto => dto.MargemLucro)
                .GreaterThanOrEqualTo(0.00m).WithMessage("Margem de lucro deve ser maior ou igual a zero");
        }

        public bool NcmValido (string ncm)
        {
            if (ncm == null)
                return true;

            var numeros = ncm.Replace(".", "");
            if (numeros.Length != 8)
                return false;

            return numeros.All(char.IsDigit);
        }
    }
}