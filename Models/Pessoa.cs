using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using BlueMoon.Models.Enuns;
using Microsoft.Net.Http.Headers;
namespace BlueMoon.Models;

public class Pessoa
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    private string _tipo = string.Empty;
    private string _situacao = string.Empty;
    private int _codigo;
    private string _email = string.Empty;
    private string _nome = string.Empty;
    private string _cpfCnpj = string.Empty;
    private string _razaoSocial = string.Empty;
    private string _nomeFantasia = string.Empty;
    private string _inscricaoMunicipal = string.Empty;
    private Endereco _endereco;

    public string Tipo
    {
        get { return _tipo; }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("Situação não pode ser nula ou vazia");
            }
            value = value.ToUpper();
            if (!Enum.TryParse<TipoPessoaEnum>(value, true, out _))
            {
                throw new ArgumentException("Tipo inválido");
            }

            _situacao = value;
        }
    }

    public string Situacao
    {
        get { return _situacao; }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("Situação não pode ser nula ou vazia");
            }
            value = value.ToUpper();
            if (!Enum.TryParse<SituacaoPessoaEnum>(value, true, out _))
            {
                throw new ArgumentException("Situação inválida");
            }

            _situacao = value;
        }
    }

    public int Codigo { get; set; }

    public ICollection<Telefone> Telefones { get; private set; } = []; 

    public string Email
    {
        get { return _email; }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("Email não pode ser nulo ou vazio");
            }
            if (value.Length > 100)
            {
                throw new ArgumentException("E-mail ultrapassa o tamanho definido");
            }
            var email = new MailAddress(value);
            if (!email.Address.Equals(value))
            {
                throw new ArgumentException("Email inválido");
            }

            _email = email.Address;
        }
    }

    public string Nome
    {
        get { return _nome; }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("Nome não pode ser nulo ou vazio");
            }
            if (value.Length > 100)
            {
                throw new ArgumentException("Nome ultrapassa o tamanho definido");
            }

            _nome = value.ToUpper();
        }
    }

    public string CPF_CNPJ
    {
        get { return _cpfCnpj; }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("CPF/CNPJ não pode ser nulo ou vazio");
            }

            value = Regex.Replace(value, "[^0-9]", "");

            if (value.Length == 11)
            {
                if (new string(value[0], value.Length) == value)
                {
                    throw new ArgumentException("CPF é inválido");
                }

                int soma = 0;
                for (int i = 0; i < 9; i++)
                    soma += (value[i] - '0') * (10 - i);

                int primeiroDigito = soma % 11;
                primeiroDigito = (primeiroDigito < 2) ? 0 : 11 - primeiroDigito;

                soma = 0;
                for (int i = 0; i < 10; i++)
                    soma += (value[i] - '0') * (11 - i);

                int segundoDigito = soma % 11;
                segundoDigito = (segundoDigito < 2) ? 0 : 11 - segundoDigito;

                if (value[9] - '0' == primeiroDigito && value[10] - '0' == segundoDigito)
                {
                    _cpfCnpj = value;
                }
                else
                {
                    throw new ArgumentException("CPF é inválido");
                }
            }
            else if (value.Length == 14)
            {
                if (new string(value[0], value.Length) == value)
                {
                    throw new ArgumentException("CNPJ é inválido");
                }

                int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

                string tempCnpj = value.Substring(0, 12);
                int soma = 0;

                for (int i = 0; i < 12; i++)
                    soma += (tempCnpj[i] - '0') * multiplicador1[i];

                int resto = soma % 11;
                int digito1 = resto < 2 ? 0 : 11 - resto;

                tempCnpj += digito1;
                soma = 0;

                for (int i = 0; i < 13; i++)
                    soma += (tempCnpj[i] - '0') * multiplicador2[i];

                resto = soma % 11;
                int digito2 = resto < 2 ? 0 : 11 - resto;

                string cnpjCalculado = tempCnpj + digito2;

                if (!value.Equals(cnpjCalculado))
                {
                    throw new ArgumentException("CNPJ é inválido");
                }

                _cpfCnpj = value;
            }
            else
            {
                throw new ArgumentException("Quantidade de caracteres para CPF/CNPJ é inválida");
            }
        }
    }

    public string RazaoSocial
    {
        get { return _razaoSocial; }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("Razão social não pode ser nula ou vazia");
            }
            if (value.Length > 100)
            {
                throw new ArgumentException("Razão social ultrapassa o tamanho definido");
            }

            _razaoSocial = value;
        }
    }

    [MaxLength(100, ErrorMessage = "Nome fantasia ultrapassa o tamanho definido")]
    public string NomeFantasia { get; private set; } = string.Empty;

    [MaxLength(20, ErrorMessage = "Inscrição muncipal ultrapassa o tamanho definido")]
    public string InscricaoMunicipal { get; private set; } = string.Empty;

    [Required(ErrorMessage = "Endereço é obrigatório")]
    public Endereco Endereco { get; private set; }
}