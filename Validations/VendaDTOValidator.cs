using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueMoon.DTO;
using FluentValidation;

namespace BlueMoon.Validations
{
    public class VendaDTOValidator
    {
        public class VendaCreateDTOValidator : AbstractValidator<VendaCreateDTO>
        {
            public VendaCreateDTOValidator()
            {
                RuleFor(dto => dto.IdPessoa)
                    .NotEmpty().WithMessage("ID de pessoa é obrigatório")
                    .Must(Validacoes.IdValido).WithMessage("ID de pessoa não é válido");
                
                RuleFor(dto => dto.IdUsuario)
                    .NotEmpty().WithMessage("ID de usuário é obrigatório")
                    .Must(Validacoes.IdValido).WithMessage("ID de usuário não é válido");
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