using System.Data;
using System.Net.Mail;
using System.Text.RegularExpressions;
using BlueMoon.DTO;
using BlueMoon.Entities.Enuns;
using FluentValidation;

namespace BlueMoon.Validations
{
    public class PessoaDTOValidator
    {
        public class PessoaCreateDTOValidator : AbstractValidator<PessoaCreateDTO>
        {
            public PessoaCreateDTOValidator()
            {
                RuleFor(dto => dto.Nome)
                    .NotEmpty().WithMessage("Nome de pessoa é obrigatório")
                    .MaximumLength(100).WithMessage("Nome não deve ter mais de 100 caracteres");

                RuleFor(dto => dto.Tipo)
                    .NotEmpty().WithMessage("Tipo de pessoa é obrigatório")
                    .GreaterThan(0).WithMessage("Tipo deve ser 1 ou 2")
                    .LessThan(3).WithMessage("Tipo deve ser 1 ou 2");

                RuleFor(dto => dto.Telefone)
                    .NotEmpty().WithMessage("Telefone de pessoa é obrigatório")
                    .Must(Validacoes.TelefoneValido).WithMessage("Telefone informado não é válido");

                RuleFor(dto => dto.Email)
                    .MaximumLength(100).WithMessage("Email não deve ter mais de 100 caracteres")
                    .Must(Validacoes.EmailValido).WithMessage("Email informado não é válido");

                RuleFor(dto => dto.Documento)
                    .MaximumLength(14).WithMessage("Documento não deve ter mais de 14 dígitos")
                    .Must(Validacoes.DocumentoValido).WithMessage("Documento informado não é valido");

                RuleFor(dto => dto.InscricaoMunicipal)
                    .MaximumLength(12).WithMessage("Inscrição municipal não deve ter mais de 12 caracteres")
                    .Must(Validacoes.InscricaoMunicipalValida).WithMessage("Inscrição municipal informada não é válida");

                RuleFor(dto => dto.InscricaoEstadual)
                    .MaximumLength(13).WithMessage("Inscrição estadual não deve ter mais de 13 caracteres")
                    .Must(Validacoes.InscricaoEstadualValida).WithMessage("Inscrição estadual informada não é válida");

                RuleFor(dto => dto.CEP)
                    .MaximumLength(8).WithMessage("CEP não deve ter mais de 8 caracteres")
                    .Must(Validacoes.CepValido).WithMessage("CEP informado não é válido");

                RuleFor(dto => dto.Logradouro)
                    .MaximumLength(100).WithMessage("Logradouro não deve ter mais de 100 caracteres");

                RuleFor(dto => dto.Numero)
                    .MaximumLength(10).WithMessage("Número não deve ter mais de 10 caracteres");

                RuleFor(dto => dto.Complemento)
                    .MaximumLength(40).WithMessage("Complemento não deve ter mais de 40 caracteres");

                RuleFor(dto => dto.Bairro)
                    .MaximumLength(70).WithMessage("Bairro não deve ter mais de 70 caracteres");

                RuleFor(dto => dto.Cidade)
                    .MaximumLength(50).WithMessage("Cidade não deve ter mais de 50 caracteres");

                RuleFor(dto => dto.Estado)
                    .GreaterThan(0).WithMessage("Valor de Estado deve ser maior do que 0")
                    .LessThan(27).WithMessage("Valor de Estado deve ser menor do que 27");
            }
        }

        public class PessoaUpdateDTOValidator : AbstractValidator<PessoaUpdateDTO>
        {
            public PessoaUpdateDTOValidator()
            {
                RuleFor(dto => dto.Id)
                    .NotEmpty().WithMessage("Id de pessoa é obrigatório")
                    .Must(Validacoes.IdValido).WithMessage("Id de pessoa é inválido");

                RuleFor(dto => dto.Situacao)
                    .LessThan(3).WithMessage("Situação deve ser 1 ou 2")
                    .GreaterThan(0).WithMessage("Situação deve ser 1 ou 2");

                RuleFor(dto => dto.Nome)
                    .NotEmpty().WithMessage("Nome de pessoa é obrigatório")
                    .MaximumLength(100).WithMessage("Nome não deve ter mais de 100 caracteres");

                RuleFor(dto => dto.Telefone)
                    .NotEmpty().WithMessage("Telefone de pessoa é obrigatório")
                    .Must(Validacoes.TelefoneValido).WithMessage("Telefone informado não é válido");

                RuleFor(dto => dto.Email)
                    .MaximumLength(100).WithMessage("Email não deve ter mais de 100 caracteres")
                    .Must(Validacoes.EmailValido).WithMessage("Email informado não é válido");

                RuleFor(dto => dto.Documento)
                    .MaximumLength(14).WithMessage("Documento não deve ter mais de 14 dígitos")
                    .Must(Validacoes.DocumentoValido).WithMessage("Documento informado não é valido");

                RuleFor(dto => dto.InscricaoMunicipal)
                    .MaximumLength(12).WithMessage("Inscrição municipal não deve ter mais de 12 caracteres")
                    .Must(Validacoes.InscricaoMunicipalValida).WithMessage("Inscrição municipal informada não é válida");

                RuleFor(dto => dto.InscricaoEstadual)
                    .MaximumLength(13).WithMessage("Inscrição estadual não deve ter mais de 13 caracteres")
                    .Must(Validacoes.InscricaoEstadualValida).WithMessage("Inscrição estadual informada não é válida");

                RuleFor(dto => dto.CEP)
                    .MaximumLength(8).WithMessage("CEP não deve ter mais de 8 caracteres")
                    .Must(Validacoes.CepValido).WithMessage("CEP informado não é válido");

                RuleFor(dto => dto.Logradouro)
                    .MaximumLength(100).WithMessage("Logradouro não deve ter mais de 100 caracteres");

                RuleFor(dto => dto.Numero)
                    .MaximumLength(10).WithMessage("Número não deve ter mais de 10 caracteres");

                RuleFor(dto => dto.Complemento)
                    .MaximumLength(40).WithMessage("Complemento não deve ter mais de 40 caracteres");

                RuleFor(dto => dto.Bairro)
                    .MaximumLength(70).WithMessage("Bairro não deve ter mais de 70 caracteres");

                RuleFor(dto => dto.Cidade)
                    .MaximumLength(50).WithMessage("Cidade não deve ter mais de 50 caracteres");

                RuleFor(dto => dto.Estado)
                    .GreaterThan(0).WithMessage("Valor de Estado deve ser maior do que 0")
                    .LessThan(27).WithMessage("Valor de Estado deve ser menor do que 27");
            }
        }

        private sealed class Validacoes()
        {
            public static bool TelefoneValido(string telefone)
            {
                if (telefone == null || telefone.Trim() == "")
                    return true;

                telefone = Regex.Replace(telefone, "[^0-9]", "");

                if (telefone.Length < 10 || telefone.Length > 11)
                    return false;

                string ddd = telefone.Substring(0, 2);

                if (!int.TryParse(ddd, out int numero))
                    return false;

                bool dddValido = Enum.IsDefined(typeof(DddEnum), numero);

                if (!dddValido)
                    return false;

                if (telefone.Length == 11 && (!telefone[2].Equals("9")))
                    return false;

                return true;
            }

            public static bool EmailValido(string email)
            {
                if (email == null || email.Trim() == "")
                    return true;

                email = email.Trim();

                string modelo = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                if (!Regex.IsMatch(email, modelo))
                    return false;

                try
                {
                    var mail = new MailAddress(email);
                    return mail.Address == email;
                }
                catch
                {
                    return false;
                }
            }

            public static bool DocumentoValido(string documento)
            {
                if (documento == null || documento.Trim() == "")
                    return true;

                documento = Regex.Replace(documento, "[^0-9]", "");

                if (documento.Length == 11)
                    return CpfValido(documento);

                if (documento.Length == 14)
                    return CnpjValido(documento);

                return false;
            }

            private static bool CpfValido(string cpf)
            {
                if (new string(cpf[0], cpf.Length) == cpf)
                    return false;

                int[] multiplicadores1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicadores2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

                string tempCpf = cpf.Substring(0, 9);
                int soma = 0;

                for (int i = 0; i < 9; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicadores1[i];

                int resto = soma % 11;
                resto = resto < 2 ? 0 : 11 - resto;

                string digito = resto.ToString();
                tempCpf += digito;
                soma = 0;

                for (int i = 0; i < 10; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicadores2[i];

                resto = soma % 11;
                resto = resto < 2 ? 0 : 11 - resto;
                digito += resto.ToString();

                return cpf.EndsWith(digito);
            }

            private static bool CnpjValido(string cnpj)
            {
                if (new string(cnpj[0], cnpj.Length) == cnpj)
                    return false;

                int[] multiplicadores1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicadores2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

                string tempCnpj = cnpj.Substring(0, 12);
                int soma = 0;

                for (int i = 0; i < 12; i++)
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicadores1[i];

                int resto = (soma % 11);
                resto = resto < 2 ? 0 : 11 - resto;

                string digito = resto.ToString();
                tempCnpj += digito;
                soma = 0;

                for (int i = 0; i < 13; i++)
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicadores2[i];

                resto = (soma % 11);
                resto = resto < 2 ? 0 : 11 - resto;
                digito += resto.ToString();

                return cnpj.EndsWith(digito);
            }

            public static bool InscricaoMunicipalValida(string inscricaoMunicipal)
            {
                if (string.IsNullOrWhiteSpace(inscricaoMunicipal))
                    return true;

                inscricaoMunicipal = Regex.Replace(inscricaoMunicipal, @"[^0-9A-Za-z]", "").Trim();

                if (inscricaoMunicipal.Length < 8 || inscricaoMunicipal.Length > 12)
                    return false;

                if (inscricaoMunicipal.Equals("ISENTO", StringComparison.OrdinalIgnoreCase))
                    return false;

                return true;
            }

            public static bool InscricaoEstadualValida(string inscricaoEstadual)
            {
                if (string.IsNullOrWhiteSpace(inscricaoEstadual))
                    return true;

                inscricaoEstadual = inscricaoEstadual.Trim();

                inscricaoEstadual = Regex.Replace(inscricaoEstadual, "[^0-9]", "");

                if (inscricaoEstadual.Length < 8 || inscricaoEstadual.Length > 13)
                    return false;

                if (new string(inscricaoEstadual[0], inscricaoEstadual.Length) == inscricaoEstadual)
                    return false;

                return true;
            }

            public static bool CepValido(string cep)
            {
                if (string.IsNullOrWhiteSpace(cep))
                    return true;

                cep = cep.Trim();

                cep = Regex.Replace(cep, "[^0-9]", "");

                if (cep.Length != 8)
                    return false;

                if (new string(cep[0], cep.Length) == cep)
                    return false;

                return true;
            }

            public static bool IdValido(string id)
            {
                return Guid.TryParse(id, out Guid result);
            }
        }
    }

}