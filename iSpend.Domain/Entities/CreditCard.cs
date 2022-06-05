using iSpend.Domain.Validation;

namespace iSpend.Domain.Entities;

public sealed class CreditCard : Entity
{
    public string UserId { get; private set; }
    public string Name { get; private set; }
    public decimal Limit { get; private set; }
    public int ExpirationDay { get; private set; }
    public int ClosingDay { get; private set; }

    public ICollection<Purchase> Purchases { get; set; }
    public ICollection<Subscription> Subscriptions { get; set; }

    public CreditCard(string userId, string name, decimal limit, int expirationDay, int closingDay)
    {
        ValidateDomain(userId, name, limit, expirationDay, closingDay);
    }

    public void Update(string userId, string name, decimal limit, int expirationDay, int closingDay)
    {
        ValidateDomain(userId, name, limit, expirationDay, closingDay);
    }

    private void ValidateDomain(string userId, string name, decimal limit, int expirationDay, int closingDay)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(name),
            "Invalid name. Name is required");

        DomainExceptionValidation.When(string.IsNullOrEmpty(userId),
            "Invalid user.");

        DomainExceptionValidation.When(limit < 0,
            "Invalid limit.");

        DomainExceptionValidation.When(expirationDay <= 0 || expirationDay > 31,
            "Invalid expiration day. Must be a day of month");

        DomainExceptionValidation.When(closingDay <= 0 || closingDay > 31,
            "Invalid closing day. Must be a day of month");

        UserId = userId;
        Name = name;
        Limit = limit;
        ExpirationDay = expirationDay;
        ClosingDay = closingDay;
        RegisteredAt = DateTime.Now;
        ModifiedAt = DateTime.Now;
    }
}
