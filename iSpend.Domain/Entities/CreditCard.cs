using iSpend.Domain.Validation;

namespace iSpend.Domain.Entities;

public sealed class CreditCard : Entity
{
    public Guid UserId { get; private set; }
    public string Name { get; private set; }
    public decimal Limit { get; private set; }
    public int ExpirationDay { get; private set; }
    public int ClosingDay { get; private set; }

    public ICollection<Purchase> Purchases { get; set; }
    public ICollection<Subscription> Subscriptions { get; set; }
}
