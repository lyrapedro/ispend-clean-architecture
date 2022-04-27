namespace iSpend.Domain.Entities;

public class Income
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public bool Recurrent { get; set; }
    public float Value { get; set; }
    public bool Active { get; set; }
    public DateTime RegisteredAt { get; set; }
}
