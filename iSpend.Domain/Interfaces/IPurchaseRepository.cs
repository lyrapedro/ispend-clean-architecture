using iSpend.Domain.Entities;

namespace iSpend.Domain.Interfaces;

public interface IPurchaseRepository
{
    Task<IEnumerable<Purchase>> GetPurchases(string userId);
    Task<IEnumerable<Purchase>> GetPurchasesFromCreditCard(int creditCardId);
    Task<Purchase> GetById(int id);
    Task<IEnumerable<Purchase>> GetByName(string userId, string name);
    Task<Purchase> Create(Purchase purchase);
    Task<Purchase> Update(Purchase purchase);
    Task<Purchase> Remove(Purchase purchase);
}
