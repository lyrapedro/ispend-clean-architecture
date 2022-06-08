using iSpend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iSpend.Application.Utils
{
    public class ServicesUtils
    {
        public List<Installment> GenerateListOfInstallmentsForPurchase(int numberOfInstallments, CreditCard creditCard, Purchase purchase)
        {
            var newInstallmentsList = new List<Installment>();
            var valueByInstallment = purchase.Price / numberOfInstallments;
            int expirationDay;
            int expirationMonth;
            int expirationYear = purchase.PurchasedAt.Year;

            bool purchasedAfterInvoiceClosing = purchase.PurchasedAt.Day >= creditCard.ClosingDay ? true : false;

            if (purchasedAfterInvoiceClosing)
            {
                expirationDay = creditCard.ExpirationDay;
                expirationMonth = purchase.PurchasedAt.Month + 1;
                if (expirationMonth > 12)
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
                if (expirationMonth > 12)
                {
                    expirationMonth = 1;
                    expirationYear += 1;
                }
            }

            return newInstallmentsList;
        }
    }
}
