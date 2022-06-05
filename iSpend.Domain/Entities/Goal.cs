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

    public Goal(string userId, string name, string? description, decimal goalValue, decimal? valueSaved, DateTime startDate, DateTime endDate)
    {
        ValidateDomain(userId, name, description, goalValue, valueSaved, startDate, endDate);
    }

    public void Update(string userId, string name, string? description, decimal goalValue, decimal? valueSaved, DateTime startDate, DateTime endDate)
    {
        ValidateDomain(userId, name, description, goalValue, valueSaved, startDate, endDate);
    }

    private void ValidateDomain(string userId, string name, string? description, decimal goalValue, decimal? valueSaved, DateTime startDate, DateTime endDate)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(name),
            "Invalid name. Name is required");

        DomainExceptionValidation.When(string.IsNullOrEmpty(userId),
            "Invalid user.");

        DomainExceptionValidation.When(goalValue < 0,
            "Invalid goal value.");

        DomainExceptionValidation.When(startDate < DateTime.MinValue,
            "Invalid start date");

        DomainExceptionValidation.When(endDate > DateTime.MaxValue,
            "Invalid end date.");

        UserId = userId;
        Name = name;
        GoalValue = goalValue;
        Description = description;
        ValueSaved = valueSaved;
        StartDate = startDate;
        EndDate = endDate;
        RegisteredAt = RegisteredAt > DateTime.MinValue ? RegisteredAt : DateTime.Now;
        ModifiedAt = DateTime.Now;
    }
}
