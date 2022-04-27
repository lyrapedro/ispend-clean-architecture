namespace iSpend.Domain.Entities;

public class Installment
{
    public int Id { get; set; }
    public float Price { get; set; }
    public bool Paid { get; set; }

    public int PurchaseId { get; set; }
    public Purchase Purchase { get; set; }
}
