using iSpend.Domain.Validation;

namespace iSpend.Domain.Entities;

public sealed class Subscription
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public float Price { get; private set; }
    public bool Active { get; private set; }
    public DateTime RegisteredAt { get; private set; }
    public DateTime ModifiedAt { get; private set; }

    public Subscription(string name, float price, bool active, int creditCardId)
    {
        ValidateDomain(name, price, active, creditCardId);
    }

    public int CreditCardId { get; set; }
    public CreditCard CreditCard { get; set; }

    private void ValidateDomain(string name, float price, bool active, int creditCardId)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid name. Name is required");
        DomainExceptionValidation.When((price < 0), "Invalid price. Price cannot be less than 0");
        DomainExceptionValidation.When((creditCardId < 0), "Invalid credit card");

        Name = name;
        Price = price;
        Active = active;
        RegisteredAt = DateTime.UtcNow;
        ModifiedAt = DateTime.UtcNow;
        CreditCardId = creditCardId;
    }
}
