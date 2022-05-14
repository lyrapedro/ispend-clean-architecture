using iSpend.Domain.Validation;

namespace iSpend.Domain.Entities;

public sealed class Goal : Entity
{
    public string UserId { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public decimal GoalValue { get; private set; }
    public decimal? ValueSaved { get; private set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
