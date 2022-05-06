using iSpend.Domain.Entities;

namespace iSpend.Domain.Interfaces;

public interface IPurchaseRepository
{
    Task<IEnumerable<Purchase>> GetPurchases(int creditCardId);
    Task<Purchase> GetPurchaseById(int? id);
    Task<Purchase> GetPurchaseCreditCard(int? id);
    Task<Purchase> Create(Purchase purchase);
    Task<Purchase> Update(Purchase purchase);
    Task<Purchase> Remove(Purchase purchase);
}
