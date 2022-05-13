using iSpend.Domain.Validation;

namespace iSpend.Domain.Entities;

public sealed class Installment : Entity
{
    public int Sequence { get; set; }
    public decimal Price { get; private set; }
    public bool Paid { get; private set; }
    public DateTime ExpiresAt { get; set; }

    public int PurchaseId { get; set; }
    public Purchase Purchase { get; set; }
}
