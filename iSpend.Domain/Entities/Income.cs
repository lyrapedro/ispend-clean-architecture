using iSpend.Domain.Validation;
using System.ComponentModel;

namespace iSpend.Domain.Entities;

public sealed class Income : Entity
{
    public string UserId { get; private set; }
    public int? CategoryId { get; set; }
    public Category? Category { get; set; }
    public string Name { get; private set; }
    public bool Recurrent { get; private set; }
    public decimal Value { get; private set; }
    public int? NumberOfRecurrences { get; private set; }
    public IncomeType? Type { get; private set; }
    public int Payday { get; private set; }
    public bool Paid { get; private set; }

    public ICollection<IncomeNode> IncomeNodes { get; private set; }

    public enum IncomeType
    {
        [Description("Daily")]
        Daily = 0,
        [Description("Weekly")]
        Weekly = 1,
        [Description("Monthly")]
        Monthly = 2,
        [Description("Yearly")]
        Yearly = 3
    }

    public Income(string userId, int? categoryId, string name, bool recurrent, decimal value, int payday)
    {
        ValidateDomain(userId, categoryId, name, recurrent, value, payday);
    }

    public void Update(string userId, int? categoryId, string name, bool recurrent, decimal value, int payday)
    {
        ValidateDomain(userId, categoryId, name, recurrent, value, payday);
    }

    private void ValidateDomain(string userId, int? categoryId, string name, bool recurrent, decimal value, int payday)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(name),
            "Invalid name. Name is required");

        DomainExceptionValidation.When(string.IsNullOrEmpty(userId),
            "Invalid user.");

        DomainExceptionValidation.When(categoryId < 0,
            "Invalid category.");

        DomainExceptionValidation.When(value < 0,
            "Invalid value.");

        DomainExceptionValidation.When(payday <= 0 || payday > 31,
            "Invalid billing day. Must be a day of month");

        UserId = userId;
        Name = name;
        CategoryId = categoryId;
        Recurrent = recurrent;
        Value = value;
        Payday = payday;
        RegisteredAt = RegisteredAt > DateTime.MinValue ? RegisteredAt : DateTime.Now;
        ModifiedAt = DateTime.Now;
    }
}

public sealed class IncomeNode
{
    public int Id { get; private set; }
    public int IncomeId { get; private set; }
    public Income Income { get; private set; }
    public bool Paid { get; private set; }
    public DateOnly ReferenceDate { get; private set; }

    public IncomeNode(int incomeId, DateOnly referenceDate)
    {
        ValidateDomain(incomeId, referenceDate);
    }

    private void ValidateDomain(int incomeId, DateOnly referenceDate)
    {
        DomainExceptionValidation.When(incomeId < 0,
            "Invalid income.");

        IncomeId = incomeId;
        ReferenceDate = referenceDate;
    }
}
