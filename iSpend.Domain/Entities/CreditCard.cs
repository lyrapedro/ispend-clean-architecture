namespace iSpend.Domain.Entities;

public class CreditCard
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public int ExpirationDay { get; set; }
    public int ClosingDay { get; set; }
    public DateTime RegisteredAt { get; set; }
    public DateTime ModifiedAt { get; set; }

    public ICollection<Purchase> Purchases { get; set; }
    public ICollection<Subscription> Subscriptions { get; set; }
}
