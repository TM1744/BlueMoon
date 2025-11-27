using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;

namespace BlueMoon.DTO
{
    public class UsuarioCreateDTO
    {
        public string IdPessoa { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public int Cargo { get; set; }
        public decimal Salario { get; set; }
        public string Admissao { get; set; } = string.Empty;
        public string HorarioInicioCargaHoraria { get; set; } = string.Empty;
        public string HorarioFimCargaHoraria { get; set; } = string.Empty;
    }

    public class UsuarioUpdateDTO
    {
        public string Id { get; set; } = string.Empty;
        public string IdPessoa { get; set; } = string.Empty;
        public int Situacao { get; set; }
        public int Cargo { get; set; }
        public decimal Salario { get; set; }
        public string Admissao { get; set; } = string.Empty;
        public string HorarioInicioCargaHoraria { get; set; } = string.Empty;
        public string HorarioFimCargaHoraria { get; set; } = string.Empty;
    }

    public class UsuarioReadDTO
    {
        public string IdPessoa { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public int CodigoPessoa { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty;
        public int Tipo { get; set; }
        public int SituacaoPessoa { get; set; }
        public string Telefone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string InscricaoMunicipal { get; set; } = string.Empty;
        public string InscricaoEstadual { get; set; } = string.Empty;
        public string CEP { get; set; } = string.Empty;
        public string Logradouro { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string Complemento { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public int Estado { get; set; }
        public int CodigoUsuario { get; set; }
        public int SituacaoUsuario { get; set; }
        public int Cargo { get; set; }
        public decimal Salario { get; set; }
        public string Admissao { get; set; } = string.Empty;
        public string HorarioInicioCargaHoraria { get; set; } = string.Empty;
        public string HorarioFimCargaHoraria { get; set; } = string.Empty;
    }

    public class UsuarioSearchDTO
    {
        public int Codigo {get;set;}
        public string Nome {get;set;} = string.Empty;
        public string Telefone {get;set;} = string.Empty;
        public string Documento {get;set;} = string.Empty;
    }

    public class UsuarioMiniReadDTO
    {
        public string IdUsuario {get;set;} = string.Empty;
        public string IdPessoa {get;set;} = string.Empty;
        public int Codigo {get;set;}
        public string Nome {get;set;} = string.Empty;
        public int Cargo {get;set;}
    }

    public class UsuarioPostLoginDTO
    {
        public string Login { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
}