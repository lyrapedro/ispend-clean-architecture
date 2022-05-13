using iSpend.Domain.Validation;

namespace iSpend.Domain.Entities;

public sealed class Purchase : Entity
{
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public DateTime FirstInstallmentDate { get; set; }
    public DateTime PurchasedAt { get; private set; }

    public int CreditCardId { get; set; }
    public CreditCard CreditCard { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public ICollection<Installment> Installments { get; set; }
}
