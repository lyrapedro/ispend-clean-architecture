using iSpend.Domain.Validation;

namespace iSpend.Domain.Entities;

public sealed class Subscription : Entity
{
    public int CreditCardId { get; set; }
    public CreditCard CreditCard { get; set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public int BillingDay { get; private set; }
    public bool Active { get; private set; }

    public Subscription(int creditCardId, string name, decimal price, int billingDay, bool active)
    {
        ValidateDomain(creditCardId, name, price, billingDay, active);
    }

    public void Update(int creditCardId, string name, decimal price, int billingDay, bool active)
    {
        ValidateDomain(creditCardId, name, price, billingDay, active);
    }

    private void ValidateDomain(int creditCardId, string name, decimal price, int billingDay, bool active)
    {
        DomainExceptionValidation.When(price < 0,
            "Invalid price.");

        DomainExceptionValidation.When(creditCardId < 0,
            "Invalid credit card");

        DomainExceptionValidation.When(string.IsNullOrEmpty(name),
            "Invalid name");

        DomainExceptionValidation.When(billingDay < int.MinValue,
            "Invalid payment date");

        CreditCardId = creditCardId;
        Name = name;
        Price = price;
        BillingDay = billingDay;
        Active = active;
        RegisteredAt = RegisteredAt > DateTime.MinValue ? RegisteredAt : DateTime.Now;
        ModifiedAt = DateTime.Now;
    }
}

public sealed class SubscriptionPaid
{
    public int Id { get; protected set; }
    public int SubscriptionId { get; set; }
    public Subscription Subscription { get; set; }
    public DateTime Date { get; private set; }

    public SubscriptionPaid(int subscriptionId, DateTime date)
    {
        ValidateDomain(subscriptionId, date);
    }

    private void ValidateDomain(int subscriptionId, DateTime date)
    {
        DomainExceptionValidation.When(subscriptionId < 0,
            "Invalid subscription.");

        SubscriptionId = subscriptionId;
        Date = date;
    }
}
