using iSpend.Domain.Validation;

namespace iSpend.Domain.Entities;

public sealed class Goal : Entity
{
    public Guid UserId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public float GoalValue { get; private set; }
    public float ValueSaved { get; private set; }
    public int Duration { get; private set; }

    public Goal(string name, string description, float goalValue, float valueSaved, int duration, string userId)
    {
        ValidateDomain(name, description, goalValue, valueSaved, duration, userId);
    }

    public void Update(string name, string description, float goalValue, float valueSaved, int duration)
    {
        ValidateDomain(name, description, goalValue, valueSaved, duration, this.UserId.ToString(), this.RegisteredAt);
    }

    private void ValidateDomain(string name, string description, float goalValue, float valueSaved, int duration, string userId, DateTime? registeredAt = null)
    {
        Guid validGuid;
        DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid name. Name is required");
        DomainExceptionValidation.When((goalValue < 0), "Invalid value. Value cannot be less than 0");
        DomainExceptionValidation.When((duration < 0), "Invalid duration. Duration must be number of months");
        DomainExceptionValidation.When((valueSaved < 0 || valueSaved > goalValue), "Invalid value. Value cannot be under than 0 or greater than goal value");
        DomainExceptionValidation.When(!Guid.TryParse(userId, out validGuid), "Invalid user.");

        Name = name;
        Description = description;
        GoalValue = goalValue;
        ValueSaved = valueSaved;
        Duration = duration;
        RegisteredAt = registeredAt == null ? DateTime.UtcNow : registeredAt.Value;
        ModifiedAt = DateTime.UtcNow;
        UserId = validGuid;
    }
}
