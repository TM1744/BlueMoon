using BlueMoon.Models.Enuns;

namespace BlueMoon.Models
{
    public sealed class Cupom
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public TipoCupom Tipo { get; private set; }
        public SituacaoCupom Situacao { get; set; } = SituacaoCupom.ATIVO;
        public int Codigo { get; private set; }
        public DateTime PrazoValidade { get; set; }
        public decimal? ValorMinimoUtilizacao { get; set; }
        public decimal ValorNumerico { get; private set; } = decimal.Zero;
        public decimal ValorPorcentagem { get; private set; } = decimal.Zero;

        private Cupom() { } 

        public Cupom(
            TipoCupom tipo,
            int codigo,
            DateTime prazoValidade,
            decimal valorMinimoUtilizacao,
            decimal valorNumerico,
            decimal valorPorcentagem,
            SituacaoCupom situacao
        )
        {
            Tipo = tipo;
            Codigo = codigo;
            PrazoValidade = prazoValidade;
            ValorMinimoUtilizacao = valorMinimoUtilizacao;
            Situacao = situacao;
            ValorNumerico = valorNumerico;
            ValorPorcentagem = valorPorcentagem;
        }
        public void Inativar() => Situacao = SituacaoCupom.INATIVO;
    }
        
}