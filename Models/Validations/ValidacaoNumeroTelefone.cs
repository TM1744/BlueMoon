using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlueMoon.Models.Validations
{
    public class ValidacaoNumeroTelefone : ValidationAttribute
    {

        public override bool IsValid(object? value)
        {
            if (value == null)
                return true;

            var texto = value.ToString();
            texto = new string(texto.Where(char.IsDigit).ToArray());

            if (texto.Length < 8 || texto.Length > 9)
                return false;

            if (texto.Length == 8 && texto[0] == 9)
                return false;

            if (texto.Length == 9 && texto[0] != 9)
                return false;

            return true;    
        }
    }
}