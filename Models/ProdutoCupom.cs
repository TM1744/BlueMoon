using BlueMoon.Models;

namespace BlueMoon.Models
{
    public sealed class ProdutoCupom
    {
        public Guid IdProduto { get; private set; }
        public Guid IdCupom { get; private set; }

        private ProdutoCupom() { }

        public ProdutoCupom(Guid idProduto, Guid idCupom)
        {
            validador(idProduto, idCupom)
            IdProduto = idProduto;
            IdCupom = idCupom;
        }

        public override bool Equals(object? obj)
        {
            return obj is ProdutoCupom other &&
                   IdProduto.Equals(other.IdProduto) &&
                   IdCupom.Equals(other.IdCupom);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdProduto, IdCupom);
        }

        public override string ToString()
        {
            return $"Produto: {IdProduto}, Cupom: {IdCupom}";
        }

        public void validador(Guid idProduto, Guid idCupom){
             if (idProduto == Guid.Empty)
                throw new ArgumentException("Id do produto não pode ser vazio.");

            if (idCupom == Guid.Empty)
                throw new ArgumentException("Id do cupom não pode ser vazio.");
        }

    }
}