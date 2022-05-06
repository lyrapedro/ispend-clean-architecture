using iSpend.Domain.Validation;

namespace iSpend.Domain.Entities;

public sealed class Income : Entity
{
    public Guid UserId { get; private set; }
    public string Name { get; private set; }
    public bool Recurrent { get; private set; }
    public decimal Value { get; private set; }
    public bool Active { get; private set; }
    public int Payday { get; set; }

    public Income(string name, decimal value, int payday, bool active, bool recurrent, string userId)
    {
        ValidateDomain(name, value, payday, active, recurrent, userId);
    }

    public void Update(string name, decimal value, int payday, bool active, bool recurrent)
    {
        ValidateDomain(name, value, payday, active, recurrent, this.UserId.ToString(), this.RegisteredAt);
    }

    private void ValidateDomain(string name, decimal value, int payday, bool active, bool recurrent, string userId, DateTime? registeredAt = null)
    {
        Guid validGuid;
        DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid name. Name is required");
        DomainExceptionValidation.When((value < 0), "Invalid value. Value cannot be less than 0");
        DomainExceptionValidation.When((payday < 0 || payday > 31), "Invalid payday");
        DomainExceptionValidation.When(!Guid.TryParse(userId, out validGuid), "Invalid user.");

        Name = name;
        Value = value;
        Active = active;
        Recurrent = recurrent;
        Payday = payday;
        RegisteredAt = registeredAt == null ? DateTime.UtcNow : registeredAt.Value;
        ModifiedAt = DateTime.UtcNow;
        UserId = validGuid;
    }
}
