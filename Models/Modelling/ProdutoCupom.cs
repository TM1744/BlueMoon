using BlueMoon.Models;

namespace BlueMoon.Models.Modelling
{
    public sealed class ProdutoCupom
    {
        public Produto Produto { get; private set; }
        public Cupom Cupom { get; private set; }

        private ProdutoCupom() { }

        public ProdutoCupom(Produto produto, Cupom cupom)
        {
            Produto = produto;
            Cupom = cupom;
        }
    }
}