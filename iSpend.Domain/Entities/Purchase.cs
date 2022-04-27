namespace iSpend.Domain.Entities;

public class Purchase
{
    public int Id { get; set; }
    public string Name { get; set; }
    public float Price { get; set; }
    public DateTime PurchasedAt { get; set; }

    public int CreditCardId { get; set; }
    public CreditCard CreditCard { get; set; }
    public ICollection<Installment> Installments { get; set; }
}
