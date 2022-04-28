using iSpend.Domain.Validation;

namespace iSpend.Domain.Entities;

public sealed class Installment
{
    public int Id { get; private set; }
    public float Price { get; private set; }
    public bool Paid { get; private set; }

    public int PurchaseId { get; set; }
    public Purchase Purchase { get; set; }

    public Installment(float price, bool paid, int purchaseId)
    {
        ValidateDomain(price, paid, purchaseId);
    }

    private void ValidateDomain(float price, bool paid, int purchaseId)
    {
        DomainExceptionValidation.When((price < 0), "Invalid price. Price cannot be less than 0");
        DomainExceptionValidation.When((purchaseId < 0), "Invalid purchase");

        Price = price;
        PurchaseId = purchaseId;
        Paid = paid;
    }
}
