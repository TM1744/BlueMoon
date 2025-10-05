namespace BlueMoon.Models;

public sealed class ItemVenda
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Produto Produto { get; private set; }
    public int Quantidade { get; private set; }
    public decimal SubTotal { get; private set; } = decimal.Zero;

    private ItemVenda() { }
    public ItemVenda(Produto produto, int quantidade)
    {
        Produto = produto;
        Quantidade = quantidade;
        SubTotal = CalcularSubtotal();
    }

    public decimal CalcularSubtotal() => SubTotal = Produto.ValorVenda * Quantidade; 
}