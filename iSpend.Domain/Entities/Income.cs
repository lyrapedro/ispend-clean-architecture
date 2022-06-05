using iSpend.Domain.Validation;

namespace iSpend.Domain.Entities;

public sealed class Income : Entity
{
    public string UserId { get; private set; }
    public int? CategoryId { get; private set; }
    public Category? Category { get; private set; }
    public string Name { get; private set; }
    public bool Recurrent { get; private set; }
    public decimal Value { get; private set; }
    public int Payday { get; set; }

    public Income(string userId, int? categoryId, string name, bool recurrent, decimal value, int payDay)
    {
        ValidateDomain(userId, categoryId, name, recurrent, value, payDay);
    }

    public void Update(string userId, int? categoryId, string name, bool recurrent, decimal value, int payDay)
    {
        ValidateDomain(userId, categoryId, name, recurrent, value, payDay);
    }

    private void ValidateDomain(string userId, int? categoryId, string name, bool recurrent, decimal value, int payDay)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(name),
            "Invalid name. Name is required");

        DomainExceptionValidation.When(string.IsNullOrEmpty(userId),
            "Invalid user.");

        DomainExceptionValidation.When(categoryId < 0,
            "Invalid category.");

        DomainExceptionValidation.When(value < 0,
            "Invalid value.");

        DomainExceptionValidation.When(payDay <= 0 || payDay > 31,
            "Invalid billing day. Must be a day of month");

        UserId = userId;
        Name = name;
        CategoryId = categoryId;
        Recurrent = recurrent;
        Value = value;
        Payday = payDay;
        RegisteredAt = DateTime.Now;
        ModifiedAt = DateTime.Now;
    }
}
