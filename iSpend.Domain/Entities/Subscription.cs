using iSpend.Domain.Validation;

namespace iSpend.Domain.Entities;

public sealed class Subscription : Entity
{
    public int CreditCardId { get; set; }
    public CreditCard CreditCard { get; set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public int BillingDay { get; private set; }

    public ICollection<SubscriptionNode> SubscriptionNodes { get; private set; }

    public Subscription(int creditCardId, string name, decimal price, int billingDay)
    {
        ValidateDomain(creditCardId, name, price, billingDay);
    }

    public void Update(int creditCardId, string name, decimal price, int billingDay)
    {
        ValidateDomain(creditCardId, name, price, billingDay);
    }

    private void ValidateDomain(int creditCardId, string name, decimal price, int billingDay)
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
        RegisteredAt = RegisteredAt > DateTime.MinValue ? RegisteredAt : DateTime.Now;
        ModifiedAt = DateTime.Now;
    }
}

public sealed class SubscriptionNode
{
    public int Id { get; protected set; }
    public int SubscriptionId { get; set; }
    public Subscription Subscription { get; set; }
    public bool Paid { get; set; }
    public DateOnly ReferenceDate { get; private set; }

    public SubscriptionNode(int subscriptionId, DateOnly referenceDate)
    {
        ValidateDomain(subscriptionId, referenceDate);
    }

    private void ValidateDomain(int subscriptionId, DateOnly referenceDate)
    {
        DomainExceptionValidation.When(subscriptionId < 0,
            "Invalid subscription.");

        SubscriptionId = subscriptionId;
        ReferenceDate = referenceDate;
    }
}
