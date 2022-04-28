﻿using iSpend.Domain.Validation;

namespace iSpend.Domain.Entities;

public sealed class Purchase
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public float Price { get; private set; }
    public DateTime PurchasedAt { get; private set; }

    public Purchase(string name, float price, string purchasedAt, int creditCardId)
    {
        ValidateDomain(name, price, purchasedAt, creditCardId);
    }

    public int CreditCardId { get; set; }
    public CreditCard CreditCard { get; set; }
    public ICollection<Installment> Installments { get; set; }

    private void ValidateDomain(string name, float price, string purchasedAt, int creditCardId)
    {
        DateTime validDate;
        DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid name. Name is required");
        DomainExceptionValidation.When((price < 0), "Invalid price. Price cannot be less than 0");
        DomainExceptionValidation.When((creditCardId < 0), "Invalid credit card.");
        DomainExceptionValidation.When(!DateTime.TryParse(purchasedAt, out validDate), "Invalid price. Price cannot be under then 0");

        Name = name;
        Price = price;
        PurchasedAt = validDate;
        CreditCardId = creditCardId;
    }
}
