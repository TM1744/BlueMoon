using System.Text.RegularExpressions;
using BlueMoon.Entities.Enuns;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BlueMoon.Entities.Models
{
    public abstract class AbstractGenericClass
    {
        public Guid ID { get; protected set; } = Guid.NewGuid();
        public int Codigo { get; protected set; }
        public virtual SituacaoGenericEnum Situacao { get; protected set; } = SituacaoGenericEnum.ATIVO;

        public void Inativar() => Situacao = SituacaoGenericEnum.INATIVO;
        public void Ativar() => Situacao = SituacaoGenericEnum.ATIVO;
        public void SetCodigo(int codigo) => Codigo = codigo;
        protected string NotEmptyString(string str)
        {
            if (str == null || str.Trim() == "")
                return "N/D";

            return str.ToUpper();
        }
        protected string SequenceNumberString(string str)
        {
            if (str == null || str.Trim() == "")
                return "N/D";

            str = Regex.Replace(str, "[^0-9]", "");
            return str;
        }
    }
}