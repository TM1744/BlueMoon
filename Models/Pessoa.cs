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
    public TipoPessoaEnum Tipo { get; private set; }
    public SituacaoPessoaEnum Situacao { get; set; }
    public int Codigo { get; private set; }
    public ICollection<Telefone> Telefones { get; private set; } = [];
    public string Email { get; set; } = string.Empty;
    public string Nome { get; private set; } = string.Empty;
    public string CPF_CNPJ { get; private set; } = string.Empty;
    public string RazaoSocial { get; set; } = string.Empty;
    public string NomeFantasia { get; set; } = string.Empty;
    public string InscricaoMunicipal { get; private set; } = string.Empty;
    public Endereco Endereco { get; set; }

    private Pessoa() { }

    public Pessoa(
        TipoPessoaEnum? tipo,
        SituacaoPessoaEnum? situacao,
        ICollection<Telefone> telefones,
        string? email,
        string nome,
        string cpf,
        Endereco endereco
    )
    {
        Tipo = tipo ?? TipoPessoaEnum.CLIENTE_PF;
        Situacao = situacao ?? SituacaoPessoaEnum.ATIVO;
        Telefones = telefones;
        Nome = nome;
        Email = email ?? "NÃO INFORMADO";
        CPF_CNPJ = cpf;
        Endereco = endereco;
    }

    public Pessoa(
        TipoPessoaEnum? tipo,
        SituacaoPessoaEnum? situacao,
        ICollection<Telefone> telefones,
        string? email,
        string nomeFantasia,
        string razaoSocial,
        string inscricaoMunicipal,
        string cnpj,
        Endereco endereco
    )
    {
        Tipo = tipo ?? TipoPessoaEnum.CLIENTE_PJ;
        Situacao = situacao ?? SituacaoPessoaEnum.ATIVO;
        Telefones = telefones;
        Email = email ?? "NÃO INFORMADO";
        NomeFantasia = nomeFantasia;
        RazaoSocial = razaoSocial;
        InscricaoMunicipal = inscricaoMunicipal;
        CPF_CNPJ = cnpj;
        Endereco = endereco;
    }

    public void AdicionarTelefone(Telefone telefone)
    {
        if (Telefones.Any(t => t.Numero == telefone.Numero))
            throw new InvalidOperationException("Telefone já existe.");
        Telefones.Add(telefone);
    }

    public void RetirarTelefone(Telefone telefone)
    {
        if (!Telefones.Any(t => t.Numero == telefone.Numero))
            throw new InvalidOperationException("Telefone não existe.");
        Telefones.Remove(telefone);
    }
}