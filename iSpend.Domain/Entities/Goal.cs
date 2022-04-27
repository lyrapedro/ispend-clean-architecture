namespace iSpend.Domain.Entities;

public class Goal
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public float GoalValue { get; set; }
    public float ValueSaved { get; set; }
    public int Duration { get; set; }
    public DateTime RegisteredAt { get; set; }
    public DateTime ModifiedAt { get; set; }
}
