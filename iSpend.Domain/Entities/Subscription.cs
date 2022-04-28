using iSpend.Domain.Validation;

namespace iSpend.Domain.Entities;

public sealed class Subscription : Entity
{
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public bool Active { get; private set; }

    public Subscription(string name, decimal price, bool active, int creditCardId)
    {
        ValidateDomain(name, price, active, creditCardId);
    }

    public void Update(string name, decimal price, bool active, int creditCardId)
    {
        ValidateDomain(name, price, active, creditCardId, this.RegisteredAt);
    }

    public int CreditCardId { get; set; }
    public CreditCard CreditCard { get; set; }

    private void ValidateDomain(string name, decimal price, bool active, int creditCardId, DateTime? registeredAt = null)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid name. Name is required");
        DomainExceptionValidation.When((price < 0), "Invalid price. Price cannot be less than 0");
        DomainExceptionValidation.When((creditCardId < 0), "Invalid credit card");

        Name = name;
        Price = price;
        Active = active;
        RegisteredAt = registeredAt == null ? DateTime.UtcNow : registeredAt.Value;
        ModifiedAt = DateTime.UtcNow;
        CreditCardId = creditCardId;
    }
}
