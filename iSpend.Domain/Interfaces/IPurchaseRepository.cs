using iSpend.Domain.Entities;

namespace iSpend.Domain.Interfaces;

public interface IPurchaseRepository
{
    Task<IEnumerable<Purchase>> GetPurchasesAsync();
    Task<Purchase> GetPurchaseByIdAsync(int? id);
    Task<Purchase> CreateAsync(Purchase purchase);
    Task<Purchase> UpdateAsync(Purchase purchase);
    Task<Purchase> RemoveAsync(Purchase purchase);
}
