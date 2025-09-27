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
                if (value.Length < 10 || value.Length > 11)
                {
                    throw new ArgumentException("Tamanho de número é inválido");
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