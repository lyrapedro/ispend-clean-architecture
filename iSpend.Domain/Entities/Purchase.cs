namespace iSpend.Domain.Entities;

public sealed class Purchase : Entity
{
    public int CreditCardId { get; private set; }
    public int? CategoryId { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public int? NumberOfInstallments { get; private set; }
    public bool? Paid { get; private set; }
    public DateTime PurchasedAt { get; private set; }

    public CreditCard CreditCard { get; private set; }
    public Category? Category { get; private set; }
    public ICollection<Installment> Installments { get; private set; }
}
