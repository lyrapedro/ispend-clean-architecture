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
    public ICollection<Installment> Installments { get; set; }

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

public static class PurchaseExtensions
{
    public static List<Installment> GenerateListOfInstallments(this Purchase purchase, CreditCard creditCard)
    {
        //AQUI FALTA TRATAMENTO PRA QUANDO O MÊS NÃO TEM O DIA x
        var newInstallmentsList = new List<Installment>();
        var numberOfInstallments = purchase.NumberOfInstallments.GetValueOrDefault();
        var valueByInstallment = Decimal.Round(purchase.Price / numberOfInstallments, 2);
        int expirationDay;
        int expirationMonth;
        int expirationYear = purchase.PurchasedAt.Year;

        bool purchasedAfterInvoiceClosing = purchase.PurchasedAt.Day >= creditCard.ClosingDay ? true : false;

        if (purchasedAfterInvoiceClosing)
        {
            //post to the next invoice
            expirationDay = creditCard.ExpirationDay;
            expirationMonth = purchase.PurchasedAt.Month + 1;
            int lastMonthOfTheYear = 12;
            //if the next invoice is in the following year, adjust the expiration date
            if (expirationMonth > lastMonthOfTheYear)
            {
                expirationMonth = 1;
                expirationYear += 1;
            }
        }
        else
        {
            expirationDay = creditCard.ExpirationDay;
            expirationMonth = purchase.PurchasedAt.Month;
        }

        for (var i = 1; i <= purchase.NumberOfInstallments; i++)
        {
            var installmentExpiresDate = new DateTime(expirationYear, expirationMonth, expirationDay);
            newInstallmentsList.Add(new Installment(purchase.Id, i, valueByInstallment, false, installmentExpiresDate));
            expirationMonth += 1;
            int lastMonthOfTheYear = 12;
            if (expirationMonth > lastMonthOfTheYear)
            {
                expirationMonth = 1;
                expirationYear += 1;
            }
        }

        return newInstallmentsList;
    }


}
