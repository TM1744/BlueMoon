using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using BlueMoon.Models.Enuns;
using Microsoft.Net.Http.Headers;
namespace BlueMoon.Models;

public class Pessoa
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    [Required(ErrorMessage = "Tipo é obrigatório")]
    public TipoPessoaEnum tipo { get; private set; }

    [Required(ErrorMessage = "Situacao é obrigatória")]
    [MaxLength(7, ErrorMessage = "Situação ultrapassa o tamanho definido")]
    public string Situacao { get; private set; } = "ATIVO";

    [Required(ErrorMessage = "Código é obrigatório")]
    [StringLength(6, ErrorMessage = "Código não corresponde ao padrão")]
    public string Codigo { get; private set; } = string.Empty;

    [Required(ErrorMessage = "Telefone é obrigatório")]
    [MaxLength(11, ErrorMessage = "Telefone ultrapassa o tamanho definido")]
    public string Telefone { get; private set; } = string.Empty;

    [EmailAddress(ErrorMessage = "E-mail inválido")]
    [MaxLength(100, ErrorMessage = "E-mail ultrapassa o tamanho definido")]
    public string Email { get; private set; } = string.Empty;

    [MaxLength(100, ErrorMessage = "Nome ultrapassa o tamanho definido")]
    public string Nome { get; private set; } = string.Empty;

    [Required(ErrorMessage = "CPF ou CNPJ é obrigatório")]
    [MaxLength(14, ErrorMessage = "CPF ou CNPJ ultrapassa o tamanho definido")]
    public string CPF_CNPJ { get; private set; } = string.Empty;
    public DateTime DataNascimento { get; private set; }

    [MaxLength(100, ErrorMessage = "Razão social ultrapassa o tamanho definido")]
    public string RazaoSocial { get; private set; } = string.Empty;

    [MaxLength(100, ErrorMessage = "Nome fantasia ultrapassa o tamanho definido")]
    public string NomeFantasia { get; private set; } = string.Empty;

    [MaxLength(20, ErrorMessage = "Inscrição muncipal ultrapassa o tamanho definido")]
    public string InscricaoMunicipal { get; private set; } = string.Empty;

    [Required(ErrorMessage = "CEP é obrigatório")]
    [StringLength(8, ErrorMessage = "CEP não corresponde ao padrão")]
    public string CEP { get; private set; } = string.Empty;

    [Required(ErrorMessage = "Endereço é obrigatório")]
    public Endereco Endereco { get; private set; }
}