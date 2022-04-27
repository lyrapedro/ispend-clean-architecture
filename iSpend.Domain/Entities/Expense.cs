namespace iSpend.Domain.Entities;

public class Expense
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public float Value { get; set; }
    public bool Active { get; set; }
    public int Duration { get; set; }
    public int PaidMonths { get; set; }
    public DateTime RegisteredAt { get; set; }
}
