using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using BlueMoon.DTO;
using BlueMoon.Entities.Enuns;
namespace BlueMoon.Entities.Models
{
    public class Usuario
    {
        public Pessoa Pessoa { get; private set; }
        public int Codigo { get; set; }
        public SituacaoPessoaEnum Situacao { get; private set; }
        public string Login { get; private set; } = string.Empty;
        public string Senha { get; private set; } = string.Empty;
        public CargoUsuarioEnum Cargo { get; private set; }
        public decimal Salario { get; private set; } = decimal.Round(0.00m, 2);
        public DateOnly Admissao { get; private set; } = DateOnly.MinValue;
        public TimeOnly HorarioInicioCargaHoraria { get; private set; } = TimeOnly.MinValue;
        public TimeOnly HorarioFimCargaHoraria { get; private set; } = TimeOnly.MinValue;

        private Usuario() { }

        public Usuario(Pessoa pessoa, UsuarioCreateDTO dto)
        {
            Pessoa = pessoa;
            Situacao = SituacaoPessoaEnum.ATIVO;
            Login = Login;
            Senha = GerarSHA256(dto.Senha);
            Cargo = (CargoUsuarioEnum)dto.Cargo;
            Salario = decimal.Round(dto.Salario, 2);
            Admissao = StringToDate(dto.Admissao);
            HorarioInicioCargaHoraria = StringToTime(dto.HorarioInicioCargaHoraria);
            HorarioFimCargaHoraria = StringToTime(dto.HorarioFimCargaHoraria);
        }

        public Usuario(Pessoa pessoa, UsuarioUpdateDTO dto)
        {
            Pessoa = pessoa;
            Situacao = SituacaoPessoaEnum.ATIVO;
            Cargo = (CargoUsuarioEnum)dto.Cargo;
            Salario = decimal.Round(dto.Salario, 2);
            Admissao = StringToDate(dto.Admissao);
            HorarioInicioCargaHoraria = StringToTime(dto.HorarioInicioCargaHoraria);
            HorarioFimCargaHoraria = StringToTime(dto.HorarioFimCargaHoraria);
        }

        public void Atualizar(Usuario usuario)
        {
            Pessoa = usuario.Pessoa;
            Situacao = usuario.Situacao;
            Cargo = usuario.Cargo;
            Salario = usuario.Salario;
            Admissao = usuario.Admissao;
            HorarioInicioCargaHoraria = usuario.HorarioInicioCargaHoraria;
            HorarioFimCargaHoraria = usuario.HorarioFimCargaHoraria;
        }

        public void Inativar() => Situacao = SituacaoPessoaEnum.INATIVO;

        private TimeOnly StringToTime(string str)
        {
            str = SequenceNumberString(str);

            if (str.Length != 4)
                return TimeOnly.MinValue;

            return TimeOnly.ParseExact(str, "HHmm", CultureInfo.InvariantCulture);
        }

        private DateOnly StringToDate(string str)
        {
            str = SequenceNumberString(str);

            if (str.Length != 8)
                return DateOnly.MinValue;

            return DateOnly.ParseExact(str, "ddMMyyyy", CultureInfo.InvariantCulture);
        }

        private string SequenceNumberString(string str)
        {
            if (str == null || str.Trim() == "")
                return "N/D";

            str = Regex.Replace(str, "[^0-9]", "");
            return str.ToUpper();
        }

        private string GerarSHA256(string texto)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytesTexto = Encoding.UTF8.GetBytes(texto);
                byte[] hashBytes = sha256.ComputeHash(bytesTexto);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                    sb.Append(b.ToString("x2"));

                return sb.ToString();
            }
        }
    }
}