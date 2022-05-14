namespace iSpend.Domain.Entities;

public class Category : Entity
{
    public string UserId { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }

    public ICollection<Purchase> Purchases { get; set; }
}
