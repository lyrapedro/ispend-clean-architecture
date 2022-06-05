using iSpend.Domain.Validation;

namespace iSpend.Domain.Entities;

public sealed class Expense : Entity
{
    public string UserId { get; private set; }
    public int? CategoryId { get; private set; }
    public Category? Category { get; private set; }
    public string Name { get; private set; }
    public decimal Value { get; private set; }
    public bool Recurrent { get; private set; }
    public int BillingDay { get; set; }

    public Expense(string userId, int? categoryId, string name, decimal value, bool recurrent, int billingDay)
    {
        ValidateDomain(userId, categoryId, name, value, recurrent, billingDay);
    }

    public void Update(string userId, int? categoryId, string name, decimal value, bool recurrent, int billingDay)
    {
        ValidateDomain(userId, categoryId, name, value, recurrent, billingDay);
    }

    private void ValidateDomain(string userId, int? categoryId, string name, decimal value, bool recurrent, int billingDay)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(name),
            "Invalid name. Name is required");

        DomainExceptionValidation.When(string.IsNullOrEmpty(userId),
            "Invalid user.");

        DomainExceptionValidation.When(categoryId < 0,
            "Invalid category.");

        DomainExceptionValidation.When(value < 0,
            "Invalid value.");

        DomainExceptionValidation.When(billingDay <= 0 || billingDay > 31,
            "Invalid billing day. Must be a day of month");

        UserId = userId;
        Name = name;
        CategoryId = categoryId;
        Value = value;
        Recurrent = recurrent;
        BillingDay = billingDay;
        RegisteredAt = DateTime.Now;
        ModifiedAt = DateTime.Now;
    }
}
