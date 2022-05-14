namespace iSpend.Domain.Entities;

public class Category : Entity
{
    public string UserId { get; private set; }
    public string Name { get; private set; }
    public string Color { get; private set; }

    public ICollection<Purchase> Purchases { get; private set; }
    public ICollection<Income> Incomes { get; private set; }
    public ICollection<Expense> Expenses { get; private set; }
}
