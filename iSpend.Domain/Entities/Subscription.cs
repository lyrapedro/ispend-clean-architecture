namespace iSpend.Domain.Entities;

public sealed class Subscription : Entity
{
    public int CreditCardId { get; set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public DateTime PaymentAt { get; set; }
    public bool Active { get; private set; }

    public CreditCard CreditCard { get; set; }
}
