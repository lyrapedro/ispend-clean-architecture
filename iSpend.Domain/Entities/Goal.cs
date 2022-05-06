using iSpend.Domain.Validation;

namespace iSpend.Domain.Entities;

public sealed class Goal : Entity
{
    public Guid UserId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal GoalValue { get; private set; }
    public decimal ValueSaved { get; private set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public Goal(string name, string description, decimal goalValue, decimal valueSaved, DateTime startDate, DateTime endDate, string userId)
    {
        ValidateDomain(name, description, goalValue, valueSaved, startDate, endDate, userId);
    }

    public void Update(string name, string description, decimal goalValue, decimal valueSaved, DateTime startDate, DateTime endDate)
    {
        ValidateDomain(name, description, goalValue, valueSaved, startDate, endDate, this.UserId.ToString(), this.RegisteredAt);
    }

    private void ValidateDomain(string name, string description, decimal goalValue, decimal valueSaved, DateTime startDate, DateTime endDate, string userId, DateTime? registeredAt = null)
    {
        Guid validGuid;
        DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid name. Name is required");
        DomainExceptionValidation.When((goalValue < 0), "Invalid value. Value cannot be less than 0");
        DomainExceptionValidation.When((endDate > DateTime.UtcNow), "Invalid end date. End date must be a future date");
        DomainExceptionValidation.When((valueSaved < 0 || valueSaved > goalValue), "Invalid value. Value cannot be under than 0 or greater than goal value");
        DomainExceptionValidation.When(!Guid.TryParse(userId, out validGuid), "Invalid user.");

        Name = name;
        Description = description;
        GoalValue = goalValue;
        ValueSaved = valueSaved;
        StartDate = startDate;
        EndDate = endDate;
        RegisteredAt = registeredAt == null ? DateTime.UtcNow : registeredAt.Value;
        ModifiedAt = DateTime.UtcNow;
        UserId = validGuid;
    }
}
