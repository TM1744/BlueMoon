using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueMoon.Models.Enuns;

namespace BlueMoon.Models
{
    public class Telefone
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        private int _ddd;
        private string _numero = string.Empty;
        public int DDD
        {
            get { return _ddd; }
            set
            {
                if (!Enum.IsDefined(typeof(DddEnum), value))
                {
                    throw new ArgumentException("DDD inválido");
                }

                _ddd = value;
            }
        }
        public string Numero
        {
            get { return _numero; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Número não pode ser nulo");
                }
                value = new string(value.Where(char.IsDigit).ToArray());
                if (value.Length < 8 || value.Length > 9)
                {
                    throw new ArgumentException("Tamanho de número é inválido");
                }
                if (value.Length == 8 && value[0] == 9)
                {
                    throw new ArgumentException("Primeiro dígito do número do telefone fixo contém 9");
                }
                if (value.Length == 9 && value[0] != 9)
                {
                    throw new ArgumentException("Número de telefone requer mais um 9");
                }

                _numero = value;
            }
        }

        private Telefone() { }

        public Telefone(int ddd, string numero)
        {
            DDD = ddd;
            Numero = numero;
        }
    }
}