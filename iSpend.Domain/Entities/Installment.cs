using iSpend.Domain.Validation;

namespace iSpend.Domain.Entities;

public sealed class Installment
{
    public int Id { get; private set; }
    public int PurchaseId { get; set; }
    public Purchase Purchase { get; set; }
    public int Order { get; private set; }
    public decimal Price { get; private set; }
    public bool? Paid { get; private set; }
    public DateTime ExpiresAt { get; private set; }

    public Installment(int purchaseId, int order, decimal price, bool? paid, DateTime expiresAt)
    {
        ValidateDomain(purchaseId, order, price, paid, expiresAt);
    }

    public void Update(int purchaseId, int order, decimal price, bool? paid, DateTime expiresAt)
    {
        ValidateDomain(purchaseId, order, price, paid, expiresAt);
    }

    private void ValidateDomain(int purchaseId, int order, decimal price, bool? paid, DateTime expiresAt)
    {
        DomainExceptionValidation.When(price < 0,
            "Invalid Price");

        PurchaseId = purchaseId;
        Order = order;
        Price = price;
        Paid = paid;
        ExpiresAt = expiresAt;
    }
}
