namespace iSpend.Domain.Entities;

public class Subscription
{
    public int Id { get; set; }
    public string Name { get; set; }
    public float Price { get; set; }
    public bool Active { get; set; }
    public DateTime RegisteredAt { get; set; }
    public DateTime ModifiedAt { get; set; }

    public int CreditCardId { get; set; }
    public CreditCard CreditCard { get; set; }
}
