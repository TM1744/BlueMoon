using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BlueMoon.Models.Enuns;

namespace BlueMoon.Models
{
    public sealed class Endereco
    {
        private string _cep = string.Empty;
        private string _logradouro = string.Empty;
        private string _numero = string.Empty;
        private string _complemento = string.Empty;
        private string _bairro = string.Empty;
        private string _cidade = string.Empty;
        private string _estado = string.Empty;

        public Guid Id { get; private set; } = Guid.NewGuid();
        public string CEP
        {
            get { return _cep; }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("CEP não pode ser nulo ou vazio");
                }

                value = new string(value.Where(char.IsDigit).ToArray());

                if (value.Length != 8)
                {
                    throw new ArgumentException("CEP não corresponde ao tamanho máximo");
                }

                _cep = value;
            }
        }
        public string Logradouro
        {
            get { return _logradouro; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Logradouro não pode ser nulo ou vazio");
                }

                if (value.Length > 100)
                {
                    throw new ArgumentException("Logradouro ultrapassa o tamanho máximo");
                }
                _logradouro = value;
            }
        }
        public string Numero
        {
            get { return _numero; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Número não pode ser nulo ou vazio");
                }
                if (value.Length > 10)
                {
                    throw new ArgumentException("Numero de logradouro ultrapassa o tamanho máximo");
                }
                _numero = value;
            }
        }
        public string Complemento
        {
            get { return _complemento; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _complemento = "NÃO DEFINIDO";
                    return;
                }

                if (value.Length > 20)
                {
                    throw new ArgumentException("Complemento ultrapassa o tamanho máximo");
                }

                _complemento = value.ToUpper();
            }
        }
        public string Bairro
        {
            get { return _bairro; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Bairro não pode ser nulo ou vazio");
                }
                if (value.Length > 20)
                {
                    throw new ArgumentException("Bairro ultrapassa o tamanho máximo");
                }
                _bairro = value.ToUpper();
            }
        }
        public string Cidade
        {
            get { return _cidade; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Cidade não pode ser nula ou vazia");
                }

                if (value.Length > 30)
                {
                    throw new ArgumentException("Cidade ultrapassa o tamanho máximo");
                }

                _cidade = value.ToUpper();
            }
        }
        public string Estado
        {
            get { return _estado; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Estado não pode ser nulo ou vazio");
                }
                if (value.Length != 2)
                {
                    throw new ArgumentException("Quantidade de caracteres do Estado é inválida");
                }
                value = value.ToUpper();
                if (!Enum.TryParse<EstadoEnum>(value, true, out _))
                {
                    throw new ArgumentException("Estado inválido");
                }

                _estado = value;
            }
        }

        private Endereco() { }

        public Endereco(string cep, string logradouro, string numero, string complemento, string bairro, string cidade, string estado)
        {
            CEP = cep;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
        }
    }
}