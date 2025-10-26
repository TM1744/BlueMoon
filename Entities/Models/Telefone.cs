using System;
using BlueMoon.Entities.Enuns;
using BlueMoon.DTO;
using System.Text.RegularExpressions;

namespace BlueMoon.Entities.Models
{
    public class Telefone
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public DddEnum DDD { get; private set; } = DddEnum.INDEFINIDO;
        public string Numero { get; private set; } = string.Empty;

        private Telefone() { }

        public Telefone(TelefoneCreateDTO create)
        {
            DDD = (DddEnum)create.DDD;
            Numero = SequenceNumberString(create.Numero);
        }

        public Telefone(TelefoneUpdateDTO update)
        {
            if (!(update.Id == ""))
                Id = Guid.Parse(update.Id);
                
            DDD = (DddEnum)update.DDD;
            Numero = SequenceNumberString(update.Numero);
        }
        
        private string SequenceNumberString(string str)
        {
            if (str == null || str.Trim() == "")
                return "N/D";

            str = Regex.Replace(str, "[^0-9]", "");
            return str.ToUpper();
        }
    }
}