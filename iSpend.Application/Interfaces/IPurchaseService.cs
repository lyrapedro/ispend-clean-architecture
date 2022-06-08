using iSpend.Application.DTOs;

namespace iSpend.Application.Interfaces;

public interface IPurchaseService
{
    Task<IEnumerable<PurchaseDTO>> GetPurchases(string userId);
    Task<IEnumerable<PurchaseDTO>> GetPurchasesFromCreditCard(int creditCardId);
    Task<PurchaseDTO> GetById(int id);
    Task<IEnumerable<PurchaseDTO>> GetByName(string userId, string name);
    Task Add(PurchaseDTO purchaseDTO);
    Task Update(PurchaseDTO purchaseDTO);
    Task Remove(PurchaseDTO purchaseDTO);
}
