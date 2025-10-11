using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueMoon.Entities.Enuns;

namespace BlueMoon.DTO.AbstractDTO
{
    public class AbstractUsuarioCreateDTO : AbstractPessoaCreateDTO
    {
        public string Login { get; set; }
        public string UserName { get; set; }
        public string Senha { get; set; }
        public CargoUsuarioEnum Cargo { get; set; }
    }

    public class AbstractUsuarioReadDTO : AbstractPessoaReadDTO
    {
        public string Login { get; set; }
        public string UserName { get; set; }
        public CargoUsuarioEnum Cargo { get; set; }
    }

    public class AbstractUsuarioUpdateDTO : AbstractPessoaUpdateDTO
    {
        public string UserName { get; set; }
        public string Senha { get; set; }
        public CargoUsuarioEnum Cargo { get; set; }
    }

    public class AbstractUsuarioDeleteDTO : AbstractPessoaDeleteDTO {}
}