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
                .GreaterThan(0).WithMessage("Quantidade em estoque deve ser menor ou igual a zero");

            RuleFor(dto => dto.QuantidadeEstoqueMinimo)
                .NotNull().WithMessage("Quantidade mínima de estoque não deve ser nula")
                .LessThanOrEqualTo(0).WithMessage("Quantidade mínima de estoque deve ser menor do que zero");

        }
    }
}