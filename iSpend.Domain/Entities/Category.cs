using iSpend.Domain.Validation;

namespace iSpend.Domain.Entities;

public class Category : Entity
{
    public string Name { get; set; }
    public string Color { get; set; }

    public ICollection<Purchase> Purchases { get; set; }

    public Category(string name, string color)
    {
        ValidateDomain(name, color);
    }

    public void Update(string name, string color)
    {
        ValidateDomain(name, color, this.RegisteredAt);
    }

    private void ValidateDomain(string name, string color, DateTime? registeredAt = null)
    {
        DateTime validDate;
        DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid name. Name is required");

        if (string.IsNullOrEmpty(color))
            Color = "#A27430";

        Name = name;
        RegisteredAt = registeredAt == null ? DateTime.UtcNow : registeredAt.Value;
        ModifiedAt = DateTime.UtcNow;

    }
}
