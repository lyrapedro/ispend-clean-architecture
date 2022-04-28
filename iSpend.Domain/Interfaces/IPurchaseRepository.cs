using iSpend.Domain.Entities;

namespace iSpend.Domain.Interfaces;

public interface IPurchaseRepository
{
    Task<IEnumerable<Purchase>> GetPurchasesAsync(int creditCardId);
    Task<Purchase> GetPurchaseByIdAsync(int? id);
    Task<Purchase> GetPurchaseCreditCardAsync(int? id);
    Task<Purchase> CreateAsync(Purchase purchase);
    Task<Purchase> UpdateAsync(Purchase purchase);
    Task<Purchase> RemoveAsync(Purchase purchase);
}
