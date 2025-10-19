using System;
using BlueMoon.Entities.Enuns;
using BlueMoon.DTO;

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
            Numero = create.Numero;
        }
        
        public Telefone(TelefoneUpdateDTO update)
        {
            Id = Guid.Parse(update.Id);
            DDD = (DddEnum)update.DDD;
            Numero = update.Numero;
        }
    }
}