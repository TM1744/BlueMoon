using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueMoon.DTO;
using FluentValidation;

namespace BlueMoon.Validations
{
    public class ItemVendaDTOValidator
    {
        public class ItemVendaCreateDTOValidator : AbstractValidator<ItemVendaCreateDTO>
        {
            public ItemVendaCreateDTOValidator()
            {
                RuleFor(dto => dto.IdProduto)
                    .NotEmpty().WithMessage("ID de produto é obrigatório")
                    .Must(Validacoes.IdValido).WithMessage("ID de produto não é válido");
                
                RuleFor(dto => dto.Quantidade)
                    .NotEmpty().WithMessage("Quantidade do produto é obrigatória")
                    .GreaterThan(0).WithMessage("Quantidade do produto deve ser maior do que 0");
            }
        }

        public sealed class Validacoes()
        {
            public static bool IdValido(string id)
            {
                return Guid.TryParse(id, out Guid result);
            }
        }
    }
}