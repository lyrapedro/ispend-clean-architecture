using iSpend.Domain.Validation;

namespace iSpend.Domain.Entities;

public sealed class CreditCard : Entity
{
    public Guid UserId { get; private set; }
    public string Name { get; private set; }
    public int ExpirationDay { get; private set; }
    public int ClosingDay { get; private set; }

    public CreditCard(string name, int expirationDay, int closingDay, string userId)
    {
        ValidateDomain(name, expirationDay, closingDay, userId);
    }

    public void Update(string name, int expirationDay, int closingDay)
    {

        ValidateDomain(name, expirationDay, closingDay, this.UserId.ToString(), this.RegisteredAt);
    }

    public ICollection<Purchase> Purchases { get; set; }
    public ICollection<Subscription> Subscriptions { get; set; }

    private void ValidateDomain(string name, int expirationDay, int closingDay, string userId, DateTime? registeredAt = null)
    {
        Guid validGuid;
        DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid name. Name is required");
        DomainExceptionValidation.When((expirationDay < 1 || expirationDay > 31), "Invalid expiration day. Must be a day of the month");
        DomainExceptionValidation.When((closingDay < 1 || closingDay > 31), "Invalid closing day. Must be a day of the month");
        DomainExceptionValidation.When(!Guid.TryParse(userId, out validGuid), "Invalid user.");

        Name = name;
        ExpirationDay = expirationDay;
        ClosingDay = closingDay;
        RegisteredAt = registeredAt == null ? DateTime.UtcNow : registeredAt.Value;
        ModifiedAt = DateTime.UtcNow;
        UserId = validGuid;
    }
}
