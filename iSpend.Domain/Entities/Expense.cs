using iSpend.Domain.Validation;

namespace iSpend.Domain.Entities;

public sealed class Expense : Entity
{
    public Guid UserId { get; private set; }
    public string Name { get; private set; }
    public decimal Value { get; private set; }
    public bool Recurrent { get; private set; }
    public bool Active { get; private set; }
    public int BillingDay { get; set; }
    public int PaidMonths { get; private set; }

    public Expense(string name, decimal value, bool active, bool recurrent, int billingDay, int paidMonths, string userId)
    {
        ValidateDomain(name, value, active, recurrent, billingDay, paidMonths, userId);
    }

    public void Update(string name, decimal value, bool active, bool recurrent, int billingDay, int paidMonths)
    {
        ValidateDomain(name, value, active, recurrent, billingDay, paidMonths, this.UserId.ToString(), this.RegisteredAt);
    }

    private void ValidateDomain(string name, decimal value, bool active, bool recurrent, int billingDay, int paidMonths, string userId, DateTime? registeredAt = null)
    {
        Guid validGuid;
        DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid name. Name is required");
        DomainExceptionValidation.When((value < 0), "Invalid value. Value cannot be less than 0");
        DomainExceptionValidation.When((paidMonths < 0), "Invalid number of paid months.");
        DomainExceptionValidation.When((billingDay < 0 || billingDay > 31), "Invalid billing day");
        DomainExceptionValidation.When(!Guid.TryParse(userId, out validGuid), "Invalid user.");

        Name = name;
        Value = value;
        Active = active;
        Recurrent = recurrent;
        BillingDay = billingDay;
        PaidMonths = paidMonths;
        RegisteredAt = registeredAt == null ? DateTime.UtcNow : registeredAt.Value;
        ModifiedAt = DateTime.UtcNow;
        UserId = validGuid;
    }
}
