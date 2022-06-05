using iSpend.Domain.Validation;

namespace iSpend.Domain.Entities;

public sealed class Subscription : Entity
{
    public int CreditCardId { get; set; }
    public CreditCard CreditCard { get; set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public DateTime PaymentAt { get; set; }
    public bool Active { get; private set; }

    public Subscription(int creditCardId, string name, decimal price, DateTime paymentAt, bool active)
    {
        ValidateDomain(creditCardId, name, price, paymentAt, active);
    }

    public void Update(int creditCardId, string name, decimal price, DateTime paymentAt, bool active)
    {
        ValidateDomain(creditCardId, name, price, paymentAt, active);
    }

    private void ValidateDomain(int creditCardId, string name, decimal price, DateTime paymentAt, bool active)
    {
        DomainExceptionValidation.When(price < 0,
            "Invalid price.");

        DomainExceptionValidation.When(creditCardId < 0,
            "Invalid credit card");

        DomainExceptionValidation.When(string.IsNullOrEmpty(name),
            "Invalid name");

        DomainExceptionValidation.When(paymentAt < DateTime.MinValue,
            "Invalid payment date");

        CreditCardId = creditCardId;
        Name = name;
        Price = price;
        PaymentAt = paymentAt;
        Active = active;
        RegisteredAt = RegisteredAt > DateTime.MinValue ? RegisteredAt : DateTime.Now;
        ModifiedAt = DateTime.Now;
    }
}
