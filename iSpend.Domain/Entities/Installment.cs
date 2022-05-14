namespace iSpend.Domain.Entities;

public sealed class Installment
{
    public int Id { get; private set; }
    public int PurchaseId { get; private set; }
    public int Order { get; private set; }
    public decimal Price { get; private set; }
    public bool? Paid { get; private set; }
    public DateTime ExpiresAt { get; private set; }

    public Purchase Purchase { get; private set; }
}
