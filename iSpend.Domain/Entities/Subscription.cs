using iSpend.Domain.Validation;

namespace iSpend.Domain.Entities;

public sealed class Subscription : Entity
{
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public bool Active { get; private set; }

    public int CreditCardId { get; set; }
    public CreditCard CreditCard { get; set; }
}
