using iSpend.Domain.Validation;

namespace iSpend.Domain.Entities;

public sealed class Income
{
    public int Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Name { get; private set; }
    public bool Recurrent { get; private set; }
    public float Value { get; private set; }
    public bool Active { get; private set; }
    public DateTime RegisteredAt { get; private set; }

    public Income(string name, float value, bool active, bool recurrent, string userId)
    {
        ValidateDomain(name, value, active, recurrent, userId);
    }

    private void ValidateDomain(string name, float value, bool active, bool recurrent, string userId)
    {
        Guid validGuid;
        DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid name. Name is required");
        DomainExceptionValidation.When((value < 0), "Invalid value. Value cannot be less than 0");
        DomainExceptionValidation.When(!Guid.TryParse(userId, out validGuid), "Invalid user.");

        Name = name;
        Value = value;
        Active = active;
        Recurrent = recurrent;
        RegisteredAt = DateTime.UtcNow;
        UserId = validGuid;
    }
}
