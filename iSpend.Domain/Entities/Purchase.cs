using iSpend.Domain.Validation;

namespace iSpend.Domain.Entities;

public sealed class Purchase : Entity
{
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public DateTime PurchasedAt { get; private set; }

    public Purchase(string name, decimal price, string purchasedAt, int creditCardId)
    {
        ValidateDomain(name, price, purchasedAt, creditCardId);
    }

    public void Update(string name, decimal price, string purchasedAt, int creditCardId)
    {
        ValidateDomain(name, price, purchasedAt, creditCardId, this.RegisteredAt);
    }

    public int CreditCardId { get; set; }
    public CreditCard CreditCard { get; set; }
    public ICollection<Installment> Installments { get; set; }

    private void ValidateDomain(string name, decimal price, string purchasedAt, int creditCardId, DateTime? registeredAt = null)
    {
        DateTime validDate;
        DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid name. Name is required");
        DomainExceptionValidation.When((price < 0), "Invalid price. Price cannot be less than 0");
        DomainExceptionValidation.When((creditCardId < 0), "Invalid credit card.");
        DomainExceptionValidation.When(!DateTime.TryParse(purchasedAt, out validDate), "Invalid price. Price cannot be under then 0");

        Name = name;
        Price = price;
        PurchasedAt = validDate;
        CreditCardId = creditCardId;
        RegisteredAt = registeredAt == null ? DateTime.UtcNow : registeredAt.Value;
        ModifiedAt = DateTime.UtcNow;

    }
}
