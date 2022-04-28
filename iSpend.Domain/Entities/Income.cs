using iSpend.Domain.Validation;

namespace iSpend.Domain.Entities;

public sealed class Income : Entity
{
    public Guid UserId { get; private set; }
    public string Name { get; private set; }
    public bool Recurrent { get; private set; }
    public float Value { get; private set; }
    public bool Active { get; private set; }

    public Income(string name, float value, bool active, bool recurrent, string userId)
    {
        ValidateDomain(name, value, active, recurrent, userId);
    }

    public void Update(string name, float value, bool active, bool recurrent)
    {
        ValidateDomain(name, value, active, recurrent, this.UserId.ToString(), this.RegisteredAt);
    }

    private void ValidateDomain(string name, float value, bool active, bool recurrent, string userId, DateTime? registeredAt = null)
    {
        Guid validGuid;
        DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid name. Name is required");
        DomainExceptionValidation.When((value < 0), "Invalid value. Value cannot be less than 0");
        DomainExceptionValidation.When(!Guid.TryParse(userId, out validGuid), "Invalid user.");

        Name = name;
        Value = value;
        Active = active;
        Recurrent = recurrent;
        RegisteredAt = registeredAt == null ? DateTime.UtcNow : registeredAt.Value;
        ModifiedAt = DateTime.UtcNow;
        UserId = validGuid;
    }
}
