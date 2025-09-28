using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using BlueMoon.Models.Enuns;
using Microsoft.Net.Http.Headers;
namespace BlueMoon.Models;

public class Pessoa
{
    private string _email = string.Empty;
    private string _nome = string.Empty;
    private string _cpfCnpj = string.Empty;
    private string _razaoSocial = string.Empty;
    private string _nomeFantasia = string.Empty;
    private string _inscricaoMunicipal = string.Empty;

    public Guid Id { get; private set; } = Guid.NewGuid();

    public TipoPessoaEnum Tipo{ get; private set; }

    public SituacaoPessoaEnum Situacao { get; private set; }

    public int Codigo { get; set; }

    public ICollection<Telefone> Telefones { get; private set; } = [];

    public string Email
    {
        get { return _email; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                _email = "NÃO INFORMADO";
                return;
            }
            if (value.Length > 100)
            {
                throw new ArgumentException("E-mail ultrapassa o tamanho definido");
            }
            try
            {
                var email = new MailAddress(value);
                if (!email.Address.Equals(value, StringComparison.OrdinalIgnoreCase))
                    throw new ArgumentException("Email inválido");

                _email = email.Address;
            }
            catch
            {
                throw new ArgumentException("Email inválido");
            }
        }
    }

    public string Nome
    {
        get { return _nome; }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Nome não pode ser nulo ou vazio");
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
                throw new ArgumentException("CPF/CNPJ não pode ser nulo ou vazio");
            }

            value = new string(value.Where(char.IsDigit).ToArray());

            if (value.Length == 11)
            {
                if (ValidarCpf(value))
                {
                    _cpfCnpj = value;
                    return;
                }

                throw new ArgumentException("CPF é inválido");
            }
            else if (value.Length == 14)
            {
                if (ValidarCnpj(value))
                {
                    _cpfCnpj = value;
                    return;
                }

                throw new ArgumentException("CPF é inválido");
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
                throw new ArgumentException("Razão social não pode ser nula ou vazia");
            }
            if (value.Length > 100)
            {
                throw new ArgumentException("Razão social ultrapassa o tamanho definido");
            }

            _razaoSocial = value.ToUpper();
        }
    }

    public string NomeFantasia
    {
        get { return _nomeFantasia; }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Nome fantasia não pode ser nula ou vazia");
            }
            if (value.Length > 100)
            {
                throw new ArgumentException("Nome fantasia ultrapassa o tamanho definido");
            }

            _nomeFantasia = value.ToUpper();
        }
    }

    public string InscricaoMunicipal
    {
        get { return _inscricaoMunicipal; }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Inscrição municipal não pode ser nula ou vazia");
            }

            value = value.Trim();

            if (value.Length > 20)
            {
                throw new ArgumentException("Inscrição municipal ultrapassa o tamanho definido");
            }

            _inscricaoMunicipal = value;
        }
    }

    public Endereco Endereco { get; private set; }

    private Pessoa() { }

    public Pessoa(TipoPessoaEnum tipo, SituacaoPessoaEnum situacao, string email, string cpfCnpj,
    string razaoSocial, string nomeFantasia, string inscricaoMunicipal, Endereco endereco,
    ICollection<Telefone> telefones)
    {
        ArgumentNullException.ThrowIfNull(endereco);
        ArgumentNullException.ThrowIfNull(telefones);

        Tipo = tipo;
        Situacao = situacao;
        Email = email;
        CPF_CNPJ = cpfCnpj;
        RazaoSocial = razaoSocial;
        NomeFantasia = nomeFantasia;
        InscricaoMunicipal = inscricaoMunicipal;
        Endereco = endereco;
        Telefones = telefones;
    }

    public Pessoa(TipoPessoaEnum tipo, SituacaoPessoaEnum situacao, string nome, string email,
    string cpfCnpj, Endereco endereco, ICollection<Telefone> telefones)
    {
        ArgumentNullException.ThrowIfNull(endereco);
        ArgumentNullException.ThrowIfNull(telefones);

        Tipo = tipo;
        Situacao = situacao;
        Nome = nome;
        Email = email;
        CPF_CNPJ = cpfCnpj;
        Endereco = endereco;
        Telefones = telefones;
    }

    public void AtivarPessoa()
    {
        Situacao = SituacaoPessoaEnum.ATIVO;
    }

    public void InativarPessoa()
    {
        Situacao = SituacaoPessoaEnum.INATIVO;
    }

    public void AlterarTipo(TipoPessoaEnum tipo)
    {
        Tipo = tipo;
    }

    public void AdicionarTelefone(Telefone telefone)
    {
        if (Telefones.Any(t => t.Numero == telefone.Numero))
            throw new InvalidOperationException("Telefone já existe.");
        Telefones.Add(telefone);
    }

    public void RemoverTelefone(Telefone telefone)
    {
        Telefones.Remove(telefone);
    }

    private bool ValidarCpf(string cpf)
    {
        if (new string(cpf[0], cpf.Length) == cpf)
        {
            return false;
        }

        int soma = 0;
        for (int i = 0; i < 9; i++)
            soma += (cpf[i] - '0') * (10 - i);

        int primeiroDigito = soma % 11;
        primeiroDigito = (primeiroDigito < 2) ? 0 : 11 - primeiroDigito;

        soma = 0;
        for (int i = 0; i < 10; i++)
            soma += (cpf[i] - '0') * (11 - i);

        int segundoDigito = soma % 11;
        segundoDigito = (segundoDigito < 2) ? 0 : 11 - segundoDigito;

        if (cpf[9] - '0' == primeiroDigito && cpf[10] - '0' == segundoDigito)
        {
            return true;
        }

        return false;
    }

    private bool ValidarCnpj(string cnpj)
    {
        if (new string(cnpj[0], cnpj.Length) == cnpj)
        {
            return false;
        }

        int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCnpj = cnpj.Substring(0, 12);
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

        if (!cnpj.Equals(cnpjCalculado))
        {
            return false;
        }

        return true;
    }
}