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
            get {return _cep;}

            set
            {
                if (value == null || value.Length > 8)
                {
                    throw new ArgumentException("CEP inválido ou ultrapassa o tamanho definido");
                }
                _cep = new string(value.Where(char.IsDigit).ToArray());
            }
        }
        public string Logradouro
        {
            get {return _logradouro;}
            set
            {
                if (value == null || value.Length > 100)
                {
                    throw new ArgumentException("Logradouro inválido ou ultrapassa o tamanho definido");
                }
                _logradouro = value;
            }
        }
        public string Numero
        {
            get { return _numero; }
            set
            {
                if (value == null || value.Length > 5)
                {
                    throw new ArgumentException("Numero de logradouro inválido ou ultrapassa o tamanho definido");
                }              
                _numero = new string(value.Where(char.IsDigit).ToArray());
            }
        }
        public string Complemento
        {
            get { return _complemento; }
            set
            {
                if (value == null || value.Length > 20)
                {
                    throw new ArgumentException("Complemento inválido ou ultrapassa o tamanho definido");
                }
                _complemento = value;     
            }
        }
        public string Bairro
        {
            get { return _bairro; }
            set
            {
                if (value == null || value.Length > 20)
                {
                    throw new ArgumentException("Bairro inválido ou ultrapassa o tamanho definido");
                }
                _bairro = value;
            }
        }
        public string Cidade
        {
            get { return _cidade; }
            set
            {
                if (value == null || value.Length > 20)
                {
                    throw new ArgumentException("Cidade inválida ou ultrapassa o tamanho definido");
                }

                _cidade = value;
            }
        }
        public string Estado
        {
            get { return _estado; }
            set
            {
                if (value == null || value.Length != 2)
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

        public Endereco(string logradouro, string numero, string complemento, string bairro, string cidade, string estado)
        {
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
        }
    }
}