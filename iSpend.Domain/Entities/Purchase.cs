using iSpend.Domain.Validation;

namespace iSpend.Domain.Entities;

public sealed class Purchase : Entity
{
    public int CreditCardId { get; set; }
    public CreditCard CreditCard { get; set; }
    public int? CategoryId { get; set; }
    public Category? Category { get; set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public int? NumberOfInstallments { get; private set; }
    public bool? Paid { get; private set; }
    public DateTime PurchasedAt { get; private set; }
    public ICollection<Purchase> Purchases { get; set; }

    public Purchase(int creditCardId, int? categoryId, string name, decimal price, int? numberOfInstallments, bool? paid, DateTime purchasedAt)
    {
        ValidateDomain(creditCardId, categoryId, name, price, numberOfInstallments, paid, purchasedAt);
    }

    public void Update(int creditCardId, int? categoryId, string name, decimal price, int? numberOfInstallments, bool? paid, DateTime purchasedAt)
    {
        ValidateDomain(creditCardId, categoryId, name, price, numberOfInstallments, paid, purchasedAt);
    }

    private void ValidateDomain(int creditCardId, int? categoryId, string name, decimal price, int? numberOfInstallments, bool? paid, DateTime purchasedAt)
    {
        DomainExceptionValidation.When(price < 0,
            "Invalid price.");

        DomainExceptionValidation.When(creditCardId < 0,
            "Invalid credit card");

        DomainExceptionValidation.When(string.IsNullOrEmpty(name),
            "Invalid name");

        DomainExceptionValidation.When(purchasedAt < DateTime.MinValue,
            "Invalid date");

        CreditCardId = creditCardId;
        CategoryId = categoryId;
        Name = name;
        Price = price;
        NumberOfInstallments = numberOfInstallments;
        Paid = paid;
        PurchasedAt = purchasedAt;
        RegisteredAt = RegisteredAt > DateTime.MinValue ? RegisteredAt : DateTime.Now;
        ModifiedAt = DateTime.Now;
    }
}
