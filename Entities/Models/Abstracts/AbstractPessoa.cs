using BlueMoon.DTO.AbstractDTO;
using BlueMoon.Entities.Enuns;

namespace BlueMoon.Entities.Models
{
    public abstract class AbstractPessoa
    {
        private AbstractPessoa () {}

        public Guid Id { get; private set; } = Guid.NewGuid();
        public SituacaoPessoaEnum Situacao { get; private set; } = SituacaoPessoaEnum.ATIVO;
        public int Codigo { get; private set; }
        public ICollection<Telefone> Telefones { get; private set; } = [];
        public string Email { get; private set; } = string.Empty;
        public Endereco Endereco { get; private set; }

        protected AbstractPessoa(AbstractPessoaCreateDTO abstractPessoaCreate)
        {
            Telefones

        }

        public void InativarPessoa() => Situacao = SituacaoPessoaEnum.INATIVO;

        public void AtivarPessoa() => Situacao = SituacaoPessoaEnum.ATIVO;


    }
}