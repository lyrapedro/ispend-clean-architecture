using iSpend.Domain.Validation;
using System.Text.RegularExpressions;

namespace iSpend.Domain.Entities;

public class Category : Entity
{
    public string? UserId { get; private set; }
    public string Name { get; private set; }
    public string Color { get; private set; }

    public ICollection<Purchase> Purchases { get; private set; }
    public ICollection<Income> Incomes { get; private set; }
    public ICollection<Expense> Expenses { get; private set; }

    public Category(string name, string color)
    {
        ValidateDomain(name, color);
    }

    public Category(int id, string name, string color)
    {
        DomainExceptionValidation.When(id < 0, "Invalid Id value.");
        Id = id;
        ValidateDomain(name, color);
    }

    public void Update(string name, string color)
    {
        ValidateDomain(name, color);
    }

    private void ValidateDomain(string name, string color)
    {
        var hexRegex = new Regex(@"^#(?:[0-9a-fA-F]{3}){1,2}$");

        DomainExceptionValidation.When(string.IsNullOrEmpty(name),
            "Invalid name. Name is required");

        DomainExceptionValidation.When(string.IsNullOrEmpty(color),
            "Invalid color.Color is required");

        DomainExceptionValidation.When(!hexRegex.Match(color).Success,
            "Invalid color.Color must be in Hex format");

        DomainExceptionValidation.When(name.Length < 3 || name.Length > 30,
           "Invalid name, too short, minimum 3 characters and max 30");

        Name = name;
        Color = color;
        RegisteredAt = DateTime.Now;
        ModifiedAt = DateTime.Now;
    }
}
