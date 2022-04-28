using iSpend.Domain.Validation;

namespace iSpend.Domain.Entities;

public sealed class Installment : Entity
{
    public decimal Price { get; private set; }
    public bool Paid { get; private set; }

    public int PurchaseId { get; set; }
    public Purchase Purchase { get; set; }

    public Installment(decimal price, bool paid, int purchaseId)
    {
        ValidateDomain(price, paid, purchaseId);
    }

    public void Update(decimal price, bool paid, int purchaseId)
    {
        ValidateDomain(price, paid, purchaseId, this.RegisteredAt);
    }

    private void ValidateDomain(decimal price, bool paid, int purchaseId, DateTime? registeredAt = null)
    {
        DomainExceptionValidation.When((price < 0), "Invalid price. Price cannot be less than 0");
        DomainExceptionValidation.When((purchaseId < 0), "Invalid purchase");

        Price = price;
        PurchaseId = purchaseId;
        Paid = paid;
        RegisteredAt = registeredAt == null ? DateTime.UtcNow : registeredAt.Value;
        ModifiedAt = DateTime.UtcNow;
    }
}
