namespace BlueMoon.Models
{
    public sealed class Cupom
    {
        public enum TipoCupom { NUMERICO, PORCENTAGEM }
        public enum SituacaoCupom { ATIVO, INATIVO }

        private string _codigo = string.Empty;
        private decimal? _valorMinimoUtilizacao;

        public Guid Id { get; private set; } = Guid.NewGuid();
        public TipoCupom Tipo { get; private set; }
        public SituacaoCupom Situacao { get; private set; } = SituacaoCupom.ATIVO;

        public string Codigo
        {
            get { return _codigo; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length != 6)
                {
                    throw new ArgumentException("Código deve ter exatamente 6 caracteres.");
                }
                _codigo = value.ToUpper();
            }
        }

        public DateTime PrazoValidade { get; private set; }

        public decimal? ValorMinimoUtilizacao
        {
            get { return _valorMinimoUtilizacao; }
            private set
            {
                if (value.HasValue && value <= 0)
                {
                    throw new ArgumentException("Se informado, o valor mínimo de utilização deve ser maior que zero.");
                }
                _valorMinimoUtilizacao = value;
            }
        }

        public decimal ? ValorNumerico { get; private set; }
        public decimal ? ValorPorcentagem { get; private set; }

        private Cupom() { } 

        public Cupom(
            TipoCupom tipo,
            string codigo,
            DateTime prazoValidade,
            decimal ? valorMinimoUtilizacao = null,
            decimal ? valorNumerico = null,
            decimal ? valorPorcentagem = null,
            SituacaoCupom situacao = SituacaoCupom.ATIVO)

        {
            validador(tipo, valorNumerico, valorPorcentagem)
            Tipo = tipo;
            Codigo = codigo;
            PrazoValidade = prazoValidade;
            ValorMinimoUtilizacao = valorMinimoUtilizacao;
            Situacao = situacao;
            ValorNumerico = valorNumerico;
            ValorPorcentagem = valorPorcentagem;
        }

        public void Ativar() => Situacao = SituacaoCupom.ATIVO;
        public void Inativar() => Situacao = SituacaoCupom.INATIVO;

        public bool EstaValido() 
            => Situacao == SituacaoCupom.ATIVO && PrazoValidade > DateTime.Now;

        private void validador(TipoCupom tipo, decimal ? valorNumerico, decimal ? valorPorcentagem)
        {
            if (tipo == TipoCupom.NUMERICO)
            {
                if (valorNumerico == null || valorNumerico <= 0)
                    throw new ArgumentException("Cupom NUMERICO deve possuir ValorNumerico maior que 0.");
                
                if (valorPorcentagem != null)
                    throw new ArgumentException("Cupom NUMERICO deve ter ValorPorcentagem nulo.");
            }
            else if (tipo == TipoCupom.PORCENTAGEM)
            {
                if (valorPorcentagem == null || valorPorcentagem <= 0)
                    throw new ArgumentException("Cupom PORCENTAGEM deve possuir ValorPorcentagem maior que 0.");
                
                if (valorNumerico != null)
                    throw new ArgumentException("Cupom PORCENTAGEM deve ter ValorNumerico nulo.");
        }
    }
        
}