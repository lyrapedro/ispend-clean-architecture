using iSpend.Domain.Validation;

namespace iSpend.Domain.Entities;

public sealed class Expense : Entity
{
    public Guid UserId { get; private set; }
    public string Name { get; private set; }
    public float Value { get; private set; }
    public bool Active { get; private set; }
    public int Duration { get; private set; }
    public int PaidMonths { get; private set; }

    public Expense(string name, float value, bool active, int duration, int paidMonths, string userId)
    {
        ValidateDomain(name, value, active, duration, paidMonths, userId);
    }

    public void Update(string name, float value, bool active, int duration, int paidMonths)
    {
        ValidateDomain(name, value, active, duration, paidMonths, this.UserId.ToString(), this.RegisteredAt);
    }

    private void ValidateDomain(string name, float value, bool active, int duration, int paidMonths, string userId, DateTime? registeredAt = null)
    {
        Guid validGuid;
        DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid name. Name is required");
        DomainExceptionValidation.When((value < 0), "Invalid value. Value cannot be less than 0");
        DomainExceptionValidation.When((duration < 0), "Invalid duration. Duration must be number of months");
        DomainExceptionValidation.When((paidMonths < 0), "Invalid number of paid months.");
        DomainExceptionValidation.When(!Guid.TryParse(userId, out validGuid), "Invalid user.");

        Name = name;
        Value = value;
        Active = active;
        Duration = duration;
        PaidMonths = paidMonths;
        RegisteredAt = registeredAt == null ? DateTime.UtcNow : registeredAt.Value;
        ModifiedAt = DateTime.UtcNow;
        UserId = validGuid;
    }
}
